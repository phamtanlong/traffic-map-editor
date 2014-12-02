using UnityEngine;
using System.Collections;

public class AutoCarInspector : IInspector {
	
	public UILabel lbID;
	public UIPopupList popChieu;

	private bool isIniting = true;

	void Start () {}

	void Update () {}
	
	public override void Save () {
		if (isIniting == true) {
			return;
		}

		gridtile.tile.properties[TileKey.AUTOCAR_DIR] = popChieu.value;
	}
	
	public override void Init (GridTileHandler gridtile) {
		if (gridtile.tile.layerType != LayerType.Other) {
			Debug.Log ("Wrong tile");
			return;
		}
		isIniting = true;
		this.gridtile = gridtile;

		//Id
		lbID.text = ""+gridtile.tile.objId;

		//direction
		string dir = Ultil.GetString (TileKey.AUTOCAR_DIR, "UP", gridtile.tile.properties);
		popChieu.value = dir;

		isIniting = false;
	}

	public void OnValueChange () {
		Save ();

		if (this.gridtile != null) {
			switch (popChieu.value) {
			case "UP":
				gridtile.transform.localEulerAngles = new Vector3(0, 0, 0);
				break;

			case "DOWN":
				gridtile.transform.localEulerAngles = new Vector3(0, 0, 180);
				break;

			case "LEFT":
				gridtile.transform.localEulerAngles = new Vector3(0, 0, 90);
				break;

			case "RIGHT":
				gridtile.transform.localEulerAngles = new Vector3(0, 0, -90);
				break;
			}
		}
	}
}
