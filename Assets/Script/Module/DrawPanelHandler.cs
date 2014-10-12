using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawPanelHandler : MonoBehaviour {

	public static DrawPanelHandler Instance;

	public UISprite grid;
	public List<GameObject> listLayers = new List<GameObject> ();

	void Awake () {
		Instance = this;
	}

	void Start () {
		SetSize (CurrentMap.Instance.width, CurrentMap.Instance.height);
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
		GameObject layer = new GameObject (""+id);
		layer.transform.parent = grid.transform;
		layer.transform.localScale = Vector3.one;

		listLayers.Add (layer);
	}

	public void RemoveLayer (int id) {
		for (int i = 0; i < listLayers.Count; ++i) {
			if (listLayers[i].name == "" + id) {
				GameObject.Destroy (listLayers[i].gameObject);
				listLayers.RemoveAt (i);
				break;
			}
		}
	}
}
