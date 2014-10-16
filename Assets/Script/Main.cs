using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {

	public static Main Instance;
	public UILabel log;

	void Awake () {
		Instance = this;
	}

	public void NewMap () {
		Ultil.ResetLayerId ();
		Ultil.ResetObjId ();

		DrawPanelHandler.Instance.Reset (Global.currentMap.width, Global.currentMap.height);
		ExplorerHandler.Instance.Reset ();
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
