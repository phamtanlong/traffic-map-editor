using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {

	public static Main Instance;
	public UILabel log;

	public static List<LayerHandler> listLayer = new List<LayerHandler> ();

	void Awake () {
		Instance = this;
	}

	public void NewMap () {
		DrawPanelHandler.Instance.Reset (CurrentMap.Instance.width, CurrentMap.Instance.height);
	}

	public void EditMap () {
		DrawPanelHandler.Instance.SetSize (CurrentMap.Instance.width, CurrentMap.Instance.height);
	}

	public void NewLayer (LayerHandler l) {
		listLayer.Add (l);

		DrawPanelHandler.Instance.NewLayer (l.layer.id);
	}

	public void RemoveLayer (LayerHandler selectedLayer) {
		listLayer.Remove (selectedLayer);

		DrawPanelHandler.Instance.RemoveLayer (selectedLayer.layer.id);
	}
}
