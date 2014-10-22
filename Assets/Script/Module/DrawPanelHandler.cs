using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;

public class DrawPanelHandler : MonoBehaviour {

	public static DrawPanelHandler Instance;

	public GridTileHandler SelectedGridTile = null;

	public Camera camera;
	public KeyCode keyToDrag = KeyCode.Space;

	public UISprite grid;
	public GameObject prefabGridLayer;
	public float scaleSpeed;
	public float minScale;
	public float maxScale;

	[HideInInspector]
	public Dictionary<int, GridLayerHandler> dictLayers = new Dictionary<int, GridLayerHandler> ();

	void Awake () {
		Instance = this;
	}

	void Start () {
		SetSize (Global.currentMap.width, Global.currentMap.height);
	}

	void Update () {
		if (Input.GetKeyUp (KeyCode.Backspace) || Input.GetKeyUp (KeyCode.Delete)) 
		{
			if (InspectorHandler.IsFocus == false) {
				if (SelectedGridTile != null) {
					dictLayers[Global.currentLayer.id].dictTiles.Remove (SelectedGridTile.tile.objId);

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
		foreach (GridLayerHandler go in dictLayers.Values) {
			GameObject.Destroy (go.gameObject);
		}
		dictLayers.Clear ();

		//setsize
		SetSize (w, h);
	}

	public void NewLayer (Layer l) {
		GameObject ins = GameObject.Instantiate (prefabGridLayer) as GameObject;
		ins.name = ""+l.id;
		ins.transform.parent = grid.transform;
		ins.transform.localScale = Vector3.one;
		ins.transform.localPosition = Vector3.zero;
		ins.gameObject.GetComponent<UIWidget>().depth = l.id;
		GridLayerHandler layer = ins.GetComponent <GridLayerHandler> ();
		layer.layer = l.Copy ();
		dictLayers[l.id] = layer;
	}

	public void RemoveLayer (int layerId) {
		GridLayerHandler layer = null;
		dictLayers.TryGetValue (layerId, out layer);
		dictLayers.Remove (layerId);
		GameObject.Destroy (layer.gameObject);
	}

	public void OnSelectedLayerChange () {
		foreach (GridLayerHandler go in dictLayers.Values) {
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

		float s = camera.orthographicSize;
		s = s + delta * scaleSpeed;

		if (s < 0.2f) {
			s = 0.2f;
		}

		if (s > 4.0f) {
			s = 4.0f;
		}

		Main.Instance.log.text = "Scale " + s;
		camera.orthographicSize = s;

		if (s <= 1.0f) {
			grid.spriteName = "tile128";
		} else if (s <= 2.0f) {
			grid.spriteName = "tile256";
		} else {
			grid.spriteName = "tile512";
		}
	}

	void OnClick () {
		bool isDrag = Input.GetKey (keyToDrag);
		
		if (isDrag == false && Global.currentLayer != null) {
			Vector2 vec = UICamera.lastWorldPosition;
			Vector3 vec2 = transform.InverseTransformPoint (vec.x, vec.y, 0);
			Vector2 v = new Vector2 (vec2.x, vec2.y);

			int newTileId = Ultil.GetNewObjId ();
			AddNewObject (v, newTileId, ToolboxHandler.Instance.SelectedTile, Global.currentTile, Global.currentLayer.id);
		}
	}

	public void AddNewObject (Vector2 pos, int newTileId, TileHandler tileHandler, Tile tile, int layerId) {
		if (tileHandler != null && layerId > 0) {
			
			GridLayerHandler d = null;
			dictLayers.TryGetValue (layerId, out d);

			if (tile.layerType != d.layer.type) {
				Main.Instance.log.text = "Choose layer-type correctly!";
				return;
			}
			
			if (d == null) { //no layer
				return;
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
			gt.gameObject.GetComponent <EventTransfer>().onPress = this.GetComponent<DragableCamera>().OnPress;
			gt.gameObject.GetComponent <EventTransfer>().onDrag = this.GetComponent<DragableCamera>().OnDrag;

			GridLayerHandler l = gameObject.GetComponent<DrawPanelHandler>().dictLayers[layerId];
			
			gt.transform.parent = l.transform;
			gt.transform.localPosition = pos;
			gt.transform.localScale = Vector2.one;

			dictLayers[layerId].dictTiles[newTileId] = gt;
		}
	}

	#endregion

}
