using UnityEngine;
using System.Collections;

public class InspectorHandler : MonoBehaviour {

	public static InspectorHandler Instance = null;
	void Awake () {
		Instance = this;
	}

	[HideInInspector]
	public Tile SelectedTile = null;

	public UIInput inpInfo;

	void Start () {}
	void Update () {}

	public void SetSelectedTile (Tile t) {
		SelectedTile = t;

	}
}
