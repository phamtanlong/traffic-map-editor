﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;

public class Main : MonoBehaviour {

	public static Main Instance;
	
	public Camera DialogCamera;
	public Camera UICamera;
	public Camera ObjCamera;

	public UILabel log;

	void Awake () {
		Instance = this;
	}

	public bool Import (string s) {

		Main.Instance.Reset ();

		try {
			ModelMap m = JsonReader.Deserialize <ModelMap> (s) as ModelMap;
		} catch (Exception e) {
			return false;
		}

		Dictionary<string, object>  total = JsonReader.Deserialize <Dictionary<string, object>> (s);
		if (total == null) {
			return false;
		}

		// -----------------------------------
		//Info

		Dictionary<string, object> info = total["info"] as Dictionary<string, object>;
		if (info == null) {
			return false;
		}

		Global.currentMap.name = (string) info["name"];
		Global.currentMap.width = (int) info["width"];
		Global.currentMap.height = (int) info["height"];
		Global.currentMap.simulateTime = (int) info["simulateTime"];
		Main.Instance.EditMap ();

		// -----------------------------------
		//Layers

		int maxId = 0;
		Dictionary<string, object> layers = total["layer"] as Dictionary<string, object>;
		if (layers != null) {

			foreach (KeyValuePair<string, object> p in layers) {
				//p = 1 layer
				Dictionary<string, object> layer = p.Value as Dictionary<string, object>;
				int layerId = int.Parse (layer["id"].ToString ());
				string name = layer["name"].ToString ();
				LayerType layerType = (LayerType) Enum.Parse (typeof (LayerType), layer["type"].ToString ());

				//New Layer
				ExplorerHandler.Instance.NewLayer (layerId, layerType, name);

				//Tiles
				Dictionary<string, object> tiles = layer["tile"] as Dictionary<string, object>;

				//Tile
				foreach (KeyValuePair<string, object> p2 in tiles) {
					Tile t = JsonReader.Deserialize<Tile> (JsonWriter.Serialize (p2.Value));
					
					Vector2 v = new Vector2 (t.x, t.y);

					Global.currentTile = t;
					GridTileHandler gt = DrawPanelHandler.Instance.AddNewObject (v, t.objId, t, layerId);
					gt.tile = t.Copy ();
					
					UITexture tt = gt.GetComponent<UITexture> ();
					tt.width = (int)t.w;
					tt.height = (int)t.h;
			
					BoxCollider box = gt.GetComponent<BoxCollider> ();
					box.size = new Vector3 (tt.width, tt.height, 0);

					if (maxId < t.objId) {
						maxId = t.objId;

						Ultil.ResetObjId (maxId);
					}
				}
			}
		} else {
			return false;
		}

		//State
		Dictionary<string,object> state = total["state"] as Dictionary<string, object>;
		if (state != null) {
			float x = float.Parse (state["x"].ToString());
			float y = float.Parse (state["y"].ToString());

			float scale = float.Parse (state["scale"].ToString());
			float step = float.Parse (state["step"].ToString());
			int current_layer = int.Parse(state["current_layer"].ToString());

			DrawPanelHandler.Instance.grid.transform.localScale = new Vector3 (scale,scale,1);
			DrawPanelHandler.Instance.grid.transform.localPosition = new Vector3 (x,y,0);

			DragableObject1.step = new Vector2 (step, step);
			ToolbarHandler.Instance.inpDragStep.value = ""+step;

			for (int i = 0; i < ExplorerHandler.listLayer.Count; ++i) {
				if (ExplorerHandler.listLayer[i].layer.id == current_layer) {
					ExplorerHandler.Instance.SetSelectedLayer (ExplorerHandler.listLayer[i]);
					break;
				}
			}

			Vector3 campos = new Vector3 ();
			campos.x = float.Parse (state["cam_x"].ToString ());
			campos.y = float.Parse (state["cam_y"].ToString ());
			campos.z = float.Parse (state["cam_z"].ToString ());

			float camera_size = float.Parse (state["cam_size"].ToString ());
			ObjCamera.transform.localPosition = campos;
			ObjCamera.orthographicSize = camera_size;

		} else {
			return false;
		}

		return true;
	}

	public string Export () {
		Dictionary<string, object> total = new Dictionary<string, object> ();
		

		//Map Info
		Dictionary<string, object> info = new Dictionary<string, object> ();

		info["name"] = Global.currentMap.name;
		info["width"] = Global.currentMap.width;
		info["height"] = Global.currentMap.height;
		info["tile"] = GameConst.TILE_WIDTH;
		info["simulateTime"] = Global.currentMap.simulateTime;
		//-------------------------
		total["info"] = info;


		//Layers
		Dictionary<int, object> layers = new Dictionary<int, object> (); //layerId, GridLayerHandler
		foreach (KeyValuePair<int, GridLayerHandler> p in DrawPanelHandler.Instance.dictLayers) {

			//layer in layers
			Dictionary<string, object> layer = new Dictionary<string, object> (); //key, value
			layer["id"] = p.Value.layer.id;
			layer["name"] = p.Value.layer.name;
			layer["type"] = p.Value.layer.type;

			//Tiles in layer
			Dictionary<int, object> tiles = new Dictionary<int, object> ();

			foreach (GridTileHandler g in p.Value.dictTiles.Values) {
				g.tile.x = g.transform.localPosition.x;
				g.tile.y = g.transform.localPosition.y;
				g.tile.w = g.GetComponent<UITexture>().width;
				g.tile.h = g.GetComponent<UITexture>().height;

				g.InitDefaultTileData ();

				tiles[g.tile.objId] = g.tile;
			}

			layer["tile"] = tiles;
			layers[p.Key] = layer;
		}
		//-------------------------
		total["layer"] = layers;


		//State
		Dictionary<string,object> state = new Dictionary<string, object> ();
		//grid
		state["x"] = DrawPanelHandler.Instance.grid.transform.localPosition.x;
		state["y"] = DrawPanelHandler.Instance.grid.transform.localPosition.y;
		state["scale"] = DrawPanelHandler.Instance.grid.transform.localScale.x;
		//step
		state["step"] = DragableObject1.step.x;
		//layer
		state["current_layer"] = ExplorerHandler.SelectedLayer.layer.id;
		//camera
		state["cam_x"] = ObjCamera.transform.localPosition.x;
		state["cam_y"] = ObjCamera.transform.localPosition.y;
		state["cam_z"] = ObjCamera.transform.localPosition.z;
		state["cam_size"] = ObjCamera.orthographicSize;
		//-------------------------
		total["state"] = state;

		
		
		string s = JsonWriter.Serialize (total);
		return s;
	}

	public void Reset () {
		Ultil.ResetLayerId ();
		Ultil.ResetObjId ();
		
		DrawPanelHandler.Instance.Reset (Global.currentMap.width, Global.currentMap.height);
		ExplorerHandler.Instance.Reset ();
	}

	public void NewMap () {
		Reset ();
		ExplorerHandler.Instance.CreateNewLayer (LayerType.Other);
		ExplorerHandler.Instance.CreateNewLayer (LayerType.View);
		ExplorerHandler.Instance.CreateNewLayer (LayerType.Sign);
		ExplorerHandler.Instance.CreateNewLayer (LayerType.Road);
	}

	public void EditMap () {
		DrawPanelHandler.Instance.SetSize (Global.currentMap.width, Global.currentMap.height);
	}

	public void NewLayer (LayerHandler l) {
		DrawPanelHandler.Instance.NewLayer (l.layer);
	}

	public void RemoveLayer (LayerHandler selectedLayer) {
		DrawPanelHandler.Instance.RemoveLayer (selectedLayer.layer.id);
	}

	public void SetSelectedLayer () {
		DrawPanelHandler.Instance.OnSelectedLayerChange ();
		ToolboxHandler.Instance.OnSelectedLayerChange ();
	}
}
