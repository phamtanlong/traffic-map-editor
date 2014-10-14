using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawPanelHandler : MonoBehaviour {

	public static DrawPanelHandler Instance;

	public KeyCode keyToDrag = KeyCode.Space;
	public KeyCode keyToDelete = KeyCode.E;

	public UISprite grid;
	public GameObject prefabGridLayer;
	public float scaleSpeed;
	public float minScale;
	public float maxScale;

	[HideInInspector]
	public Dictionary<int,Dictionary<Vector2,TileHandler>> dictTile; //layerId,position,tile
	[HideInInspector]
	public Dictionary<int,GameObject> dictLayers = new Dictionary<int,GameObject> ();

	void Awake () {
		Instance = this;
	}

	void Start () {
		SetSize (CurrentMap.Instance.width, CurrentMap.Instance.height);
		dictTile = new Dictionary<int, Dictionary<Vector2, TileHandler>> ();
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
			if (go.name == ""+CurrentLayer.Instance.layer.id) {
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

		if (s.x >= minScale && s.x <= maxScale) {
			grid.transform.localScale = s;
		}
	}

	void OnPress (bool isPress) {
		if (isPress == false) { //keyUp
			PressToDraw ();
		}
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
		
		if (CurrentLayer.Instance != null) {
			Dictionary<Vector2,TileHandler> d = null;
			dictTile.TryGetValue (CurrentLayer.Instance.layer.id, out d);
			
			if (d != null) {
				TileHandler t = null;
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
			draw.dictLayers.TryGetValue (CurrentLayer.Instance.layer.id, out layer);
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
		
		if (CurrentTile.Instance != null && CurrentLayer.Instance != null) {
			
			Dictionary<Vector2,TileHandler> d = null;
			dictTile.TryGetValue (CurrentLayer.Instance.layer.id, out d);
			
			if (d == null) {
				d = new Dictionary<Vector2, TileHandler> ();
				dictTile[CurrentLayer.Instance.layer.id] = d;
			}
			
			TileHandler ins = GameObject.Instantiate (CurrentTile.Instance) as TileHandler;
			ins.name = pos.x + "," + pos.y;
			ins.gameObject.layer = 9;
			
			Destroy (ins.GetComponent<BoxCollider>());
			Destroy (ins.GetComponent<UIButton>());
			Destroy (ins.GetComponent<UIPlaySound>());
			Destroy (ins.GetComponent<UIDragScrollView>());
			Destroy (ins.GetComponent<TweenColor>());
			
			foreach (Transform c in ins.transform) {
				GameObject.Destroy(c.gameObject);
			}
			
			ins.GetComponent<UITexture>().color = Color.white;
			
			GameObject l = gameObject.GetComponent<DrawPanelHandler>().dictLayers[CurrentLayer.Instance.layer.id];
			
			//delete obj at this position
			Transform trans = l.transform.FindChild (pos.x+","+pos.y);
			if (trans != null) {
				AddToDelete (trans.gameObject);
			}
			//////////////////////////////

			ins.transform.parent = l.transform;
			ins.transform.localPosition = pos;
			ins.transform.localScale = Vector2.one;
			
			dictTile[CurrentLayer.Instance.layer.id][pos] = ins;

		}
	}

	#endregion

}
