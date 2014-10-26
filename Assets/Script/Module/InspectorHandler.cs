using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InspectorHandler : MonoBehaviour {

	public static bool IsFocus {
		get {
			return Instance.inpInfo.isSelected;
		}
	}
	public static InspectorHandler Instance = null;
	void Awake () {
		Instance = this;
	}

	[HideInInspector]
	public GridTileHandler SelectedTile = null;

	public UIInput inpInfo;
	public RoadInspector roadInspector;
	public SignInspector signInspector;
	public ViewInspector viewInspector;

	void Start () {
		roadInspector.gameObject.SetActive (false);
		signInspector.gameObject.SetActive (false);
		viewInspector.gameObject.SetActive (false);
	}
	void Update () {}

	public void SetSelectedTile (GridTileHandler t) {
		SelectedTile = t;

		roadInspector.gameObject.SetActive (false);
		signInspector.gameObject.SetActive (false);
		viewInspector.gameObject.SetActive (false);

		switch (t.tile.layerType) {
		case LayerType.Road:
			roadInspector.Init (t);
			roadInspector.gameObject.SetActive (true);
			break;

		case LayerType.Sign:
			signInspector.Init (t);
			signInspector.gameObject.SetActive (true);
			break;

		case LayerType.View:
			viewInspector.Init (t);
			viewInspector.gameObject.SetActive (true);
			break;

		}
	}

	public void OnSave () {
		switch (SelectedTile.tile.layerType) {
		case LayerType.Road:
			roadInspector.Save ();
			break;

		case LayerType.Sign:
			signInspector.Save ();
			break;

		case LayerType.View:
			viewInspector.Save ();
			break;
		}
	}
}
