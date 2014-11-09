using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class PanelInitilizer : MonoBehaviour {

	public LayerType layerType;
	public string folder;
	public GameObject prefab;

	void Start () {}

	public void Init () {
		VerticalGridView grid = this.GetComponent <VerticalGridView> ();

		Dictionary<string, Texture> dict = TileManager.Instance.GetFolder (folder);

		foreach(Texture t in dict.Values)
		{
			GameObject go = GameObject.Instantiate (prefab) as GameObject;
			go.name = t.name;
			
			UITexture uit = go.GetComponent<UITexture> ();
			uit.mainTexture = t;

			TileHandler handler = go.GetComponent<TileHandler>();
			Tile tile = new Tile ();
			tile.typeId = int.Parse (t.name);
			tile.layerType = this.layerType;
			handler.Setup (tile);
			
			grid.AddChild (go);
			go.transform.localScale = Vector3.one;

			//Add to ToolboxHandler
			ToolboxHandler.Instance.AddTileHandler (handler);
		}
	}
}
