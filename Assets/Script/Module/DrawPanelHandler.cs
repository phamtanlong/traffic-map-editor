using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;

public class DrawPanelHandler : MonoBehaviour {

	public static DrawPanelHandler Instance;

	public GridTileHandler SelectedGridTile = null;

	public KeyCode keyToDrag = KeyCode.Space;

	public UISprite grid;
	public UISprite grid32;
	public UISprite grid16;
	public UISprite grid8;
	public GameObject prefabGridLayer;
	public float scaleSpeed;
	public float minScale;
	public float maxScale;

	[HideInInspector]
	public Dictionary<int, Dictionary<int, GridTileHandler>> dictTiles; //layerId,position,tile
	[HideInInspector]
	public Dictionary<int, GameObject> dictLayers = new Dictionary<int, GameObject> ();

	void Awake () {
		Instance = this;
	}

	void Start () {
		SetSize (Global.currentMap.width, Global.currentMap.height);
		dictTiles = new Dictionary<int, Dictionary<int, GridTileHandler>> ();
		grid16.gameObject.SetActive (false);
		grid8.gameObject.SetActive (false);
	}

	void Update () {
		if (Input.GetKeyUp (KeyCode.Backspace) || Input.GetKeyUp (KeyCode.Delete)) 
		{
			if (InspectorHandler.IsFocus == false) {
				if (SelectedGridTile != null) {
					dictTiles[Global.currentLayer.id].Remove (SelectedGridTile.tile.objId);

					GameObject.Destroy (SelectedGridTile.gameObject);
					SelectedGridTile = null;
				}
			}
		}
	}

	public void SetSize (int w, int h) {
		grid.width = GameConst.TILE_WIDTH * w;
		grid.height = GameConst.TILE_HEIGHT * h;
	}

	public void Reset (int w, int h) {
		//clear all
		int children = grid.transform.childCount;

		dictTiles.Clear ();
		foreach (GameObject go in dictLayers.Values) {
			GameObject.Destroy (go);
		}
		dictLayers.Clear ();

		//setsize
		SetSize (w, h);
	}

	public void NewLayer (int id) {
		GameObject layer = GameObject.Instantiate (prefabGridLayer) as GameObject;
		layer.name = ""+id;
		layer.transform.parent = grid.transform;
		layer.transform.localScale = Vector3.one;

		layer.GetComponent<UIWidget>().depth = id;

		dictLayers[id] = layer;
	}

	public void RemoveLayer (int layerId) {

		//remove data
		dictTiles.Remove (layerId);

		//remove layer
		GameObject layer = null;
		dictLayers.TryGetValue (layerId, out layer);
		dictLayers.Remove (layerId);
		GameObject.Destroy (layer.gameObject);
	}

	public void SelectLayer () {
		foreach (GameObject go in dictLayers.Values) {
			if (go.name == ""+Global.currentLayer.id) {
				UISprite wi = go.GetComponent<UISprite>();
				wi.color = Color.white;

				//enable boxcollider

				GridTileHandler[] grids = wi.GetComponentsInChildren <GridTileHandler> ();
				for (int i = 0; i < grids.Length; ++i) {
					grids[i].gameObject.GetComponent <BoxCollider>().enabled = true;
				}

			} else {
				UISprite wi = go.GetComponent<UISprite>();
				wi.color = new Color (1,1,1,0.7f);

				//disable boxcollider
				
				GridTileHandler[] grids = wi.GetComponentsInChildren <GridTileHandler> ();
				for (int i = 0; i < grids.Length; ++i) {
					grids[i].gameObject.GetComponent <BoxCollider>().enabled = false;
				}
			}
		}
	}

	public void OnChangeSelectedTile () {
		InspectorHandler.Instance.SetSelectedTile (DrawPanelHandler.Instance.SelectedGridTile.tile);
	}

	#region DRAW

	void OnScroll (float delta) {
		Vector3 s = grid.transform.localScale;
		s.x = s.x + delta * scaleSpeed;
		s.y = s.y + delta * scaleSpeed;

		if (s.x >= 0.2f && s.x <= 12.0f) {
			grid.transform.localScale = s;

			if (s.x < 4.0f) {
				grid32.gameObject.SetActive (true);
				grid16.gameObject.SetActive (false);
				grid8.gameObject.SetActive (false);

			} else if (s.x < 8.0f) {
				grid32.gameObject.SetActive (false);
				grid16.gameObject.SetActive (true);
				grid8.gameObject.SetActive (false);

				grid16.width = grid.width * 2;
				grid16.height = grid.height * 2;
			} else {
				grid32.gameObject.SetActive (false);
				grid16.gameObject.SetActive (false);
				grid8.gameObject.SetActive (true);
				
				grid8.width = grid.width * 4;
				grid8.height = grid.height * 4;
			}
		}
	}

	void OnClick () {
		bool isDrag = Input.GetKey (keyToDrag);
		
		if (isDrag == false) {
			Vector2 vec = UICamera.lastWorldPosition;
			Vector3 vec2 = transform.InverseTransformPoint (vec.x, vec.y, 0);
			Vector2 v = new Vector2 (vec2.x, vec2.y);

			int newTileId = Ultil.GetNewObjId ();
			AddNewObject (v, newTileId, ToolboxHandler.Instance.SelectedTile, new Tile (), Global.currentLayer.id);
		}
	}

	public void AddNewObject (Vector2 pos, int newTileId, TileHandler tileHandler, Tile tile, int layerId) {
		if (tileHandler != null && layerId > 0) {
			
			Dictionary<int, GridTileHandler> d = null;
			dictTiles.TryGetValue (layerId, out d);
			
			if (d == null) { //new layer if it null
				d = new Dictionary<int, GridTileHandler> ();
				dictTiles[layerId] = d;
			}

			TileHandler ins = GameObject.Instantiate (tileHandler) as TileHandler;
			ins.gameObject.AddComponent (typeof (GridTileHandler));

			GridTileHandler gt = ins.GetComponent<GridTileHandler>();
			gt.Init (ins, newTileId);
			gt.tile = tile.Copy ();
			gt.tile.objId = newTileId;
			//gt.tile.layerId = layerId;

			//--------------------------------------------------
			gt.name = ""+newTileId;
			gt.gameObject.AddComponent (typeof (EventTransfer));
			gt.gameObject.GetComponent <EventTransfer>().onScroll = this.OnScroll;
			gt.gameObject.GetComponent <EventTransfer>().onPress = this.GetComponent<DragableObject>().OnPress;
			gt.gameObject.GetComponent <EventTransfer>().onDrag = this.GetComponent<DragableObject>().OnDrag;

			GameObject l = gameObject.GetComponent<DrawPanelHandler>().dictLayers[layerId];
			
			gt.transform.parent = l.transform;
			gt.transform.localPosition = pos;
			gt.transform.localScale = Vector2.one;

			dictTiles[layerId][newTileId] = gt;
		}
	}

	#endregion

}
