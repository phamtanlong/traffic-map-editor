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
	public LightInspector lightInspector;
	
	private IInspector currentInspector = null;

	void Start () {
		currentInspector = null;

		roadInspector.gameObject.SetActive (false);
		signInspector.gameObject.SetActive (false);
		viewInspector.gameObject.SetActive (false);
		lightInspector.gameObject.SetActive (false);
	}
	void Update () {}

	public void SetSelectedTile (GridTileHandler t) {
		SelectedTile = t;

		if (currentInspector != null) {
			currentInspector.gameObject.SetActive (false);
		}

		switch (t.tile.layerType) {
		case LayerType.Road:
			currentInspector = roadInspector;
			break;

		case LayerType.Sign:
			currentInspector = signInspector;
			break;

		case LayerType.View:
			currentInspector = viewInspector;
			break;

		case LayerType.Other:
			currentInspector = lightInspector;
			break;
		}
		
		currentInspector.gameObject.SetActive (true);
		currentInspector.Init (t);
	}

	public void OnSave () {
		if (currentInspector != null) {
			currentInspector.Save ();
		}
	}
}
