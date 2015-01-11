using UnityEngine;
using System.Collections;

public class OtherInspector : IInspector {
	
	public LightInspector lightInspector;
	//public AutoCarInspector autoCarInspector;

	private IInspector currentInspector;

	void Start () {
		lightInspector.gameObject.SetActive (false);
		//autoCarInspector.gameObject.SetActive (false);
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

		case 310:
			//currentInspector = autoCarInspector;
			break;
			
		case 311:
			//currentInspector = autoCarInspector;
			break;
		}

		if (currentInspector != null) {
			currentInspector.gameObject.SetActive (true);
			currentInspector.Init (gridtile);
		}
	}
}
