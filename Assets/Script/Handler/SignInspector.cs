using UnityEngine;
using System.Collections;

public class SignInspector : IInspector {
	
	public UILabel lbID;
	public UIPopupList popChieu;
	public GameObject objTocDoMax;
	public UIInput inpTocDoMax;
	public GameObject objTocDoMin;
	public UIInput inpTocDoMin;

	private bool isIniting = true;


	void Start () {}

	void Update () {}
	
	public override void Save () {
		if (isIniting == true) {
			return;
		}

		gridtile.tile.properties[TileKey.SIGN_DIR] = popChieu.value;

		//Han che toc do
		if (gridtile.tile.typeId == 103 || gridtile.tile.typeId == 128) {
			gridtile.tile.properties[TileKey.SIGN_MAX_TOCDO] = inpTocDoMax.value;
		} else if (gridtile.tile.typeId == 138 || gridtile.tile.typeId == 139) {
			gridtile.tile.properties[TileKey.SIGN_MIN_TOCDO] = inpTocDoMin.value;
		}
	
	}
	
	public override void Init (GridTileHandler gridtile) {
		if (gridtile.tile.layerType != LayerType.Sign) {
			Debug.Log ("Wrong tile");
			return;
		}
		isIniting = true;
		this.gridtile = gridtile;

		//Disable Alll
		objTocDoMax.SetActive (false);
		objTocDoMin.SetActive (false);

		//----------
		
		//Id
		lbID.text = ""+gridtile.tile.objId;

		//direction
		string dir = Ultil.GetString (TileKey.SIGN_DIR, "UP", gridtile.tile.properties);
		popChieu.value = dir;

		//Han che toc do
		if (gridtile.tile.typeId == 103 || gridtile.tile.typeId == 128) {
			objTocDoMax.SetActive (true);

			string maxTocDo = Ultil.GetString (TileKey.SIGN_MAX_TOCDO, "40", gridtile.tile.properties);
			inpTocDoMax.value = ""+maxTocDo;
		} else if (gridtile.tile.typeId == 138 || gridtile.tile.typeId == 139) { //Toc Do Toi Thieu
			objTocDoMin.SetActive (true);

			string minTocDo = Ultil.GetString (TileKey.SIGN_MIN_TOCDO, "20", gridtile.tile.properties);
			inpTocDoMin.value = ""+minTocDo;
		}

		isIniting = false;
	}

	public void OnValueChange () {
		Save ();
	}
}
