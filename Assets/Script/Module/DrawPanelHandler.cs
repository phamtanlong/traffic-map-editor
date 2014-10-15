using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawPanelHandler : MonoBehaviour {

	public static DrawPanelHandler Instance;

	public GridTileHandler SelectedGridTile = null;

	public KeyCode keyToDrag = KeyCode.Space;
	public KeyCode keyToDelete = KeyCode.E;

	public UISprite grid;
	public UISprite grid32;
	public UISprite grid16;
	public UISprite grid8;
	public GameObject prefabGridLayer;
	public float scaleSpeed;
	public float minScale;
	public float maxScale;

	[HideInInspector]
	public Dictionary<int, Dictionary<Vector2, GridTileHandler>> dictTile; //layerId,position,tile
	[HideInInspector]
	public Dictionary<int, GameObject> dictLayers = new Dictionary<int, GameObject> ();

	void Awake () {
		Instance = this;
	}

	void Start () {
		SetSize (Global.currentMap.width, Global.currentMap.height);
		dictTile = new Dictionary<int, Dictionary<Vector2, GridTileHandler>> ();
		grid16.gameObject.SetActive (false);
		grid8.gameObject.SetActive (false);
	}

	public void SetSize (int w, int h) {
		grid.width = GameConst.TILE_WIDTH * w;
		grid.height = GameConst.TILE_HEIGHT * h;
	}

	public void Reset (int w, int h) {
		//clear all
		int children = grid.transform.childCount;
		for (int i = 0; i < children; ++i) {
			GameObject.Destroy (grid.transform.GetChild(i).gameObject);
		}

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

	public void RemoveLayer (int id) {

		//remove data
		dictTile.Remove (id);

		//remove layer

		GameObject layer = null;
		dictLayers.TryGetValue (id, out layer);
		dictLayers.Remove (id);
		GameObject.Destroy (layer.gameObject);
	}

	public void SelectLayer () {
		foreach (GameObject go in dictLayers.Values) {
			if (go.name == ""+Global.currentLayer.id) {
				UISprite wi = go.GetComponent<UISprite>();
				wi.color = Color.white;
			} else {
				UISprite wi = go.GetComponent<UISprite>();
				wi.color = new Color (1,1,1,0.7f);
			}
		}
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
		PressToDraw ();
	}
	
	void OnDrag (Vector2 v) {
		bool isDrag = Input.GetKey (keyToDrag);
		bool isDelete = Input.GetKey (keyToDelete);
		
		if (isDrag == false) {
			if (isDelete == true) {
				PressToDraw ();
			}
		}

	}
	
	void PressToDraw () {
		bool isDrag = Input.GetKey (keyToDrag);
		bool isDelete = Input.GetKey (keyToDelete);
		
		if (isDrag == false) {
			if (isDelete == false) {
				Vector2 vec = UICamera.lastWorldPosition;
				Vector3 vec2 = transform.InverseTransformPoint (vec.x, vec.y, 0);
				Vector2 v = new Vector2 (vec2.x, vec2.y);
				
				DrawAt (v);
			} else if (isDelete == true) {
				Vector2 vec = UICamera.lastWorldPosition;
				Vector3 vec2 = transform.InverseTransformPoint (vec.x, vec.y, 0);
				Vector2 v = new Vector2 (vec2.x, vec2.y);
				
				EraseAt (v);
			}
		}
	}
	
	void AddToDelete (GameObject t) {
		StartCoroutine (DestroyTile (t));
	}
	
	IEnumerator DestroyTile (GameObject t) {
		GameObject.Destroy (t.gameObject);
		yield return new WaitForEndOfFrame();
	}

	void EraseAt (Vector2 pos) {
		int dx = Mathf.RoundToInt (pos.x / 32);
		int dy = Mathf.RoundToInt (pos.y / 32);
		pos.x = 32 * dx;
		pos.y = 32 * dy;
		
		if (Global.currentLayer != null) {
			Dictionary<Vector2, GridTileHandler> d = null;
			dictTile.TryGetValue (Global.currentLayer.id, out d);
			
			if (d != null) {
				GridTileHandler t = null;
				d.TryGetValue (pos, out t);
				if (t != null) {
					t.gameObject.SetActive (false);
					AddToDelete (t.gameObject);
					//d[pos] = null;
				}
			}
			
			//Delete by ChildName
			DrawPanelHandler draw = GetComponent<DrawPanelHandler>();
			GameObject layer = null;
			draw.dictLayers.TryGetValue (Global.currentLayer.id, out layer);
			if (layer != null) {
				Transform trans = layer.transform.FindChild (pos.x+","+pos.y);
				if (trans != null) {
					AddToDelete (trans.gameObject);
				}
			}
		}
	}
	
	void DrawAt (Vector2 pos) {
		int dx = Mathf.RoundToInt (pos.x / 32);
		int dy = Mathf.RoundToInt (pos.y / 32);
		pos.x = 32 * dx;
		pos.y = 32 * dy;
		
		if (Global.currentTile != null && Global.currentLayer != null) {
			
			Dictionary<Vector2, GridTileHandler> d = null;
			dictTile.TryGetValue (Global.currentLayer.id, out d);
			
			if (d == null) {
				d = new Dictionary<Vector2, GridTileHandler> ();
				dictTile[Global.currentLayer.id] = d;
			}
			
			TileHandler ins = GameObject.Instantiate (ToolboxHandler.Instance.SelectedTile) as TileHandler;
			ins.gameObject.AddComponent (typeof (GridTileHandler));

			GridTileHandler gt = ins.GetComponent<GridTileHandler>();
			gt.Init (ins);

			//--------------------------------------------------
			gt.name = pos.x + "," + pos.y;
			gt.gameObject.AddComponent (typeof (EventTransfer));
			gt.gameObject.GetComponent <EventTransfer>().onScroll = this.OnScroll;
			gt.gameObject.GetComponent <EventTransfer>().onPress = this.GetComponent<DragableObject>().OnPress;
			gt.gameObject.GetComponent <EventTransfer>().onDrag = this.GetComponent<DragableObject>().OnDrag;

			GameObject l = gameObject.GetComponent<DrawPanelHandler>().dictLayers[Global.currentLayer.id];

			//--------------------------------------------------
			//delete obj at this position
			Transform trans = l.transform.FindChild (pos.x+","+pos.y);
			if (trans != null) {
				AddToDelete (trans.gameObject);
			}
			//--------------------------------------------------
			
			gt.transform.parent = l.transform;
			gt.transform.localPosition = pos;
			gt.transform.localScale = Vector2.one;

			dictTile[Global.currentLayer.id][pos] = gt;
		}
	}

	#endregion

}
