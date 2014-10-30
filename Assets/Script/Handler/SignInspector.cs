using UnityEngine;
using System.Collections;

public class SignInspector : IInspector {
	
	public UILabel lbID;
	public UIPopupList popChieu;

	void Start () {}

	void Update () {}
	
	public override void Save () {
		gridtile.tile.properties[TileKey.SIGN_DIR] = popChieu.value;
	}
	
	public override void Init (GridTileHandler gridtile) {
		if (gridtile.tile.layerType != LayerType.Sign) {
			Debug.Log ("Wrong tile");
			return;
		}
		
		this.gridtile = gridtile;
		
		//Id
		lbID.text = ""+gridtile.tile.objId;

		//direction
		string dir = Ultil.GetString (TileKey.SIGN_DIR, "UP", gridtile.tile.properties);
		popChieu.value = dir;
	}
}
