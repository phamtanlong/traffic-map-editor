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
	public Tile SelectedTile = null;

	public UIInput inpInfo;
	public RoadInspector roadInspector;
	public SignInspector signInspector;
	public ViewInspector viewInspector;

	void Start () {}
	void Update () {}

	public void SetSelectedTile (Tile t) {
		SelectedTile = t;

		switch (t.layerType) {
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
		switch (SelectedTile.layerType) {
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
