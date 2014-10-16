using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToolboxHandler : MonoBehaviour {

	public static ToolboxHandler Instance;

	[HideInInspector]
	public TileHandler SelectedTile = null;
	[HideInInspector]
	public Dictionary<int,TileHandler> dictTileHandler = new Dictionary<int, TileHandler> ();

	public UIButton[] tabs;
	public GameObject[] scrollViews;

	public int showedTab;
	public int currentTab;

	void Awake () {
		ToolboxHandler.Instance = this;
	}

	// Use this for initialization
	void Start () {
		showedTab = -1;
		currentTab = 0;
		RefreshTabState ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Tab0 () {
		currentTab = 0;
		RefreshTabState ();
	}
	
	public void Tab1 () {
		currentTab = 1;
		RefreshTabState ();
	}
	
	public void Tab2 () {
		currentTab = 2;
		RefreshTabState ();
	}
	
	public void Tab3 () {
		currentTab = 3;
		RefreshTabState ();
	}

	public void RefreshTabState () {
		for (int i = 0; i < scrollViews.Length; ++i) {
			scrollViews[i].gameObject.SetActive (false);
		}

		if (showedTab == currentTab) {
			scrollViews[currentTab].gameObject.SetActive (false);
			showedTab = -1;
		} else {
			scrollViews[currentTab].gameObject.SetActive (true);
			showedTab = currentTab;
		}
	}

	public void AddTileHandler (TileHandler t) {
		dictTileHandler[t.tile.typeId] = t;
	}

	public TileHandler GetTileHandler (int id) {
		return dictTileHandler[id];
	}
}
