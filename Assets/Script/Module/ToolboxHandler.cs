using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToolboxHandler : MonoBehaviour {

	public static ToolboxHandler Instance;

	[HideInInspector]
	public TileHandler SelectedTile = null;
	[HideInInspector]
	public Dictionary<int,TileHandler> dictTileHandler = new Dictionary<int, TileHandler> ();

	public UIToggle[] tabs;
	public PanelInitilizer[] panels;


	void Awake () {
		ToolboxHandler.Instance = this;
	}


	void Start () {
		for (int i = 0; i < panels.Length; ++i) {
			panels[i].Init ();
		}

		ExplorerHandler.Instance.CreateNewLayer (LayerType.Road);
	}
	void Update () {}

	public void AddTileHandler (TileHandler t) {
		dictTileHandler[t.tile.typeId] = t;
	}

	public TileHandler GetTileHandler (int id) {
		TileHandler t = null;
		dictTileHandler.TryGetValue (id, out t);

		if (t == null) {
			Main.Instance.log.text = "Can not find out tile: " + id;
		}

		return t;
	}

	public void OnSelectedLayerChange () {
		if (Global.currentLayer == null) {
			return;
		}

		switch (Global.currentLayer.type) {
		case LayerType.Road:
			tabs[0].value = true;
			break;

		case LayerType.Sign:
			tabs[1].value = true;
			break;

		case LayerType.View:
			tabs[2].value = true;
			break;
		}
	}
}
