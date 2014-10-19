using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelInitilizer : MonoBehaviour {

	public LayerType layerType;
	public string folder;
	public GameObject prefab;
	
	void Start () {
		VerticalGridView grid = this.GetComponent <VerticalGridView> ();

		Texture[] texes = Resources.LoadAll<Texture>(folder);
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

}
