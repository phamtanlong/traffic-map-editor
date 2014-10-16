using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;

public class Main : MonoBehaviour {

	public static Main Instance;
	public UILabel log;

	void Awake () {
		Instance = this;
	}

	public void Import (string s) {

		Main.Instance.Reset ();

		Dictionary<string, object>  total = JsonReader.Deserialize <Dictionary<string, object>> (s);

		//Info
		Dictionary<string, object> info = total["info"] as Dictionary<string, object>;
		if (info != null) {
			Global.currentMap.name = (string) info["name"];
			Global.currentMap.width = (int) info["width"];
			Global.currentMap.height = (int) info["height"];

			Main.Instance.EditMap ();
		}


		//Layer
		Dictionary<string, object> layer = total["layer"] as Dictionary<string, object>;
		if (layer != null) {
			foreach (KeyValuePair<string, object> p in layer) {
				//New Layer
				int layerId = int.Parse (p.Key);
				ExplorerHandler.Instance.NewLayer (layerId);

				//Add Object
				Dictionary<string, object> tile = p.Value as Dictionary<string, object>;
				if (tile != null) {
					foreach (object o in tile.Values) {
						Tile t = JsonReader.Deserialize<Tile> (JsonWriter.Serialize (o));

						Vector2 v = new Vector2 (t.x, t.y);
						TileHandler tilehandler = ToolboxHandler.Instance.GetTileHandler (t.typeId);

						ToolboxHandler.Instance.SelectedTile = tilehandler;
						Global.currentTile = tilehandler.tile;
						DrawPanelHandler.Instance.AddNewObject (v, t.objId, tilehandler, layerId);
					}
				} else {
					Debug.LogError ("Null tile dictionary");
				}
			}
		} else {
			Debug.Log ("Null layer dictionary");
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
		}


	}

	public string Export () {
		Dictionary<string, object> total = new Dictionary<string, object> ();
		

		//Map Info
		Dictionary<string,object> info = new Dictionary<string, object> ();
		info["name"] = Global.currentMap.name;
		info["width"] = Global.currentMap.width;
		info["height"] = Global.currentMap.height;
		info["tile"] = GameConst.TILE_WIDTH;
		//-------------------------
		total["info"] = info;


		//Layers
		Dictionary<int, Dictionary<int, Tile>> layer = new Dictionary<int, Dictionary<int, Tile>> ();		
		foreach (KeyValuePair<int, Dictionary<int, GridTileHandler>> p in DrawPanelHandler.Instance.dictTiles) {
			Dictionary<int, Tile> tile = new Dictionary<int, Tile> ();
			foreach (GridTileHandler g in p.Value.Values) {
				g.tile.x = g.transform.localPosition.x;
				g.tile.y = g.transform.localPosition.y;
				tile[g.tile.objId] = g.tile;
			}
			layer[p.Key] = tile;
		}
		//-------------------------
		total["layer"] = layer; 


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
		ExplorerHandler.Instance.OnNewLayer ();
	}

	public void EditMap () {
		DrawPanelHandler.Instance.SetSize (Global.currentMap.width, Global.currentMap.height);
	}

	public void NewLayer (LayerHandler l) {
		DrawPanelHandler.Instance.NewLayer (l.layer.id);
	}

	public void RemoveLayer (LayerHandler selectedLayer) {
		DrawPanelHandler.Instance.RemoveLayer (selectedLayer.layer.id);
	}

	public void SetSelectedLayer () {
		DrawPanelHandler.Instance.SelectLayer ();
	}
}
