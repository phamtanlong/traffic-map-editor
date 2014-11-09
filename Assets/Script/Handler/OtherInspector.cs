using UnityEngine;
using System.Collections;

public class OtherInspector : IInspector {
	
	public LightInspector lightInspector;

	private IInspector currentInspector;

	void Start () {
		lightInspector.gameObject.SetActive (false);
	}

	void Update () {}
	
	public override void Save () {
		if (currentInspector != null) {
			currentInspector.Save ();
		}
	}
	
	public override void Init (GridTileHandler t) {
		if (t.tile.layerType != LayerType.Other) {
			Debug.Log ("Wrong tile");
			return;
		}
		this.gridtile = t;

		if (currentInspector != null) {
			currentInspector.gameObject.SetActive (false);
			currentInspector = null;
		}

		switch (gridtile.tile.typeId) {
		case 301:
			currentInspector = lightInspector;
			break;
		}

		if (currentInspector != null) {
			currentInspector.gameObject.SetActive (true);
			currentInspector.Init (gridtile);
		}
	}
}
