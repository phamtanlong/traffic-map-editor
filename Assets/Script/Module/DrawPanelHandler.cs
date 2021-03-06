﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;

public class DrawPanelHandler : MonoBehaviour {
	const float COPY_X = 64;
	const float COPY_Y = 64;

	public static DrawPanelHandler Instance;

	public GridTileHandler SelectedGridTile = null;

	public GridTileHandler CopiedGridTile = null;

	public Camera objcamera;
	public KeyCode keyToDrag = KeyCode.Space;

	public UISprite grid;
	public GameObject parentLayer;
	public GameObject tempParent;
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
			if (InspectorHandler.Instance.IsFocus == false) {
				if (SelectedGridTile != null) {
					dictLayers[Global.currentLayer.id].dictTiles.Remove (SelectedGridTile.tile.objId);

					GameObject.Destroy (SelectedGridTile.gameObject);
					SelectedGridTile = null;
				}
			}
		}
		
		if (InspectorHandler.Instance.IsFocus == false) {
			if (Input.GetKeyUp (KeyCode.C)) {
				if (SelectedGridTile != null) {
					CopiedGridTile = SelectedGridTile;
					Main.Instance.log.text = "Copy: " + CopiedGridTile.gameObject.name;
				}
			}

			if (Input.GetKeyUp (KeyCode.V)) {
				if (CopiedGridTile != null && Global.currentLayer.type == CopiedGridTile.tile.layerType) {
					//AddNewObject (Vector2 pos, int newTileId, TileHandler tileHandler, Tile tile, int layerId)

					Tile tile = CopiedGridTile.tile.Copy ();
					Vector3 oldpos = CopiedGridTile.transform.localPosition;
					Vector3 newpos = new Vector3 (oldpos.x+COPY_X, oldpos.y+COPY_Y, oldpos.z);
					int newTileId = Ultil.GetNewObjId ();
					//TileHandler tilehandler = ToolboxHandler.Instance.GetTileHandler (tile.typeId);

					GridTileHandler gt = AddNewObject (newpos, newTileId, tile.Copy (), Global.currentLayer.id);
					gt.tile = tile;
					gt.tile.objId = newTileId;

					UITexture tt = gt.GetComponent <UITexture> ();
					tt.width = (int)gt.tile.w;
					tt.height = (int)gt.tile.h;

					Debug.Log (CopiedGridTile.tile.w);
					Debug.Log (tile.w);
					Debug.Log (gt.tile.w);
					Debug.Log (tt.width);

					BoxCollider box = gt.GetComponent <BoxCollider> ();
					box.size = new Vector3 (gt.tile.w, gt.tile.h, 0);

					if (gt != null) {
						Main.Instance.log.text = "Paste: " + gt.gameObject.name;
					}
				}
			}
		}
	}

	public void SetSize (int w, int h) {
		grid.width = GameConst.TILE_WIDTH * w;
		grid.height = GameConst.TILE_HEIGHT * h;

		//parentLayer.transform.parent = grid.transform;
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
		ins.transform.parent = parentLayer.transform;
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
		InspectorHandler.Instance.SetSelectedTile (DrawPanelHandler.Instance.SelectedGridTile);
	}

	#region SCROLL
	
	void OnScroll (float delta) {
		delta *= -1;
		
		float s = objcamera.orthographicSize;
		s = s + delta * scaleSpeed;
		
		if (s < minScale) {
			s = minScale;
		}
		
		if (s > maxScale) {
			s = maxScale;
		}
		
		Main.Instance.log.text = "Scale " + s;
		objcamera.orthographicSize = s;
		
		if (s <= 1.0f) {
			grid.spriteName = "tile128";
		} else if (s <= 2.0f) {
			grid.spriteName = "tile256";
		} else {
			grid.spriteName = "tile512";
		}
	}

	#endregion

	#region DRAW

	void OnClick () {
		bool isDrag = Input.GetKey (keyToDrag);
		
		if (isDrag == false && Global.currentLayer != null && Global.currentTile != null) {
			Vector2 vec = UICamera.lastWorldPosition;
			Vector3 vec2 = transform.InverseTransformPoint (vec.x, vec.y, 0);
			Vector2 v = new Vector2 (vec2.x, vec2.y);

			int newTileId = Ultil.GetNewObjId ();
			Tile t = Global.currentTile.Copy();
			Ultil.InitDefaultValue (t);

			AddNewObject (v, newTileId, t, Global.currentLayer.id);
		}
	}

	public GridTileHandler AddNewObject (Vector2 pos, int newTileId, Tile tile, int layerId) {
		if (layerId > 0) {
			
			GridLayerHandler d = null;
			dictLayers.TryGetValue (layerId, out d);

			if (tile.layerType != d.layer.type) {
				Main.Instance.log.text = "Choose layer-type correctly!";
				return null;
			}
			
			if (d == null) { //no layer
				return null;
			}

			GameObject ins = new GameObject ();
			UITexture tt = ins.AddComponent <UITexture>();
			tt.mainTexture = TileManager.Instance.GetTexture (tile.typeId+"");

			GridTileHandler gt = ins.AddComponent <GridTileHandler>();
			gt.Init (tile, newTileId);
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

			gt.tile.x = gt.transform.localPosition.x;
			gt.tile.y = gt.transform.localPosition.y;
			gt.tile.w = tt.width;
			gt.tile.h = tt.height;


			dictLayers[layerId].dictTiles[newTileId] = gt;

			return gt;
		}

		return null;
	}

	#endregion

}
