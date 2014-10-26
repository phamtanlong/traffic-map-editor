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

		Texture[] texes = Resources.LoadAll<Texture>(folder);
		Array.Sort <Texture> (texes, CompareTextureName);

		foreach(Texture t in texes)
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

	private static int CompareTextureName(Texture x, Texture y) {
		int xx = int.Parse (x.name);
		int yy = int.Parse (y.name);

		return xx - yy;
	}

}
