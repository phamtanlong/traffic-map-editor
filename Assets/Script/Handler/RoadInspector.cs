using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RoadInspector : IInspector {
	
	public const int STEP_RESIZE = 64;

	public UILabel lbID;
	public UIPopupList popChieu;
	public UIToggle cbCoi;
	public UIInput inpTocDoMin;
	public UIInput inpTocDoMax;
	public UIToggle cbReTrai;
	public UIToggle cbRePhai;
	public UIToggle cbReThang;
	//public UIToggle cbReLui;
	public ComboCheckbox cbLoaiXe;
	public UIInput inpDetail;

	//public Tile tile;

	void Start () {}

	void Update () {}

	public override void Save () {

		//Coi
		gridtile.tile.properties[TileKey.COI] = "" + cbCoi.value;

		//Chieu
		gridtile.tile.properties[TileKey.CHIEU] = popChieu.value;

		//Velocity
		gridtile.tile.properties[TileKey.MIN_VEL] = inpTocDoMin.value;
		gridtile.tile.properties[TileKey.MAX_VEL] = inpTocDoMax.value;

		//Huong Re
		gridtile.tile.properties[TileKey.RE_TRAI] = ""+cbReTrai.value;
		gridtile.tile.properties[TileKey.RE_PHAI] = ""+cbRePhai.value;
		gridtile.tile.properties[TileKey.RE_THANG] = ""+cbReThang.value;

		//Loai Xe
		for (int i = 0; i < cbLoaiXe.listItems.Count; ++i) {
			ComboCheckboxItem item = cbLoaiXe.listItems[i];

			gridtile.tile.properties[TileKey.DI + item.lbTitle.text] = ""+item.Check1;
			gridtile.tile.properties[TileKey.DUNG + item.lbTitle.text] = ""+item.Check2;
		}
	}

	public override void Init (GridTileHandler gridtile) {
		if (gridtile.tile.layerType != LayerType.Road) {
			Debug.Log ("Wrong tile");
			return;
		}

		this.gridtile = gridtile;

		//Id
		lbID.text = ""+gridtile.tile.objId;

		//Coi
		bool isCoi = Boolean.Parse (Ultil.GetString (TileKey.COI, "true", gridtile.tile.properties));
		cbCoi.value = isCoi;

		//Chieu
		string dir = Ultil.GetString (TileKey.CHIEU, "UP", gridtile.tile.properties);
		popChieu.value = dir;

		//Velocity
		int minVel = int.Parse (Ultil.GetString (TileKey.MIN_VEL, "0", gridtile.tile.properties));
		int maxVel = int.Parse (Ultil.GetString (TileKey.MAX_VEL, "40", gridtile.tile.properties));
		inpTocDoMin.value = ""+minVel;
		inpTocDoMax.value = ""+maxVel;

		//Huong Re
		bool isReTrai = Boolean.Parse (Ultil.GetString (TileKey.RE_TRAI, "true", gridtile.tile.properties));
		bool isRePhai = Boolean.Parse (Ultil.GetString (TileKey.RE_PHAI, "true", gridtile.tile.properties));
		bool isReThang = Boolean.Parse (Ultil.GetString (TileKey.RE_THANG, "true", gridtile.tile.properties));
		cbReTrai.value = isReTrai;
		cbRePhai.value = isRePhai;
		cbReThang.value = isReThang;
		
		List<ComboCheckboxData> listData = new List<ComboCheckboxData> ();
		foreach (VihicleType val in Enum.GetValues(typeof(VihicleType)))
		{
			string name = Enum.GetName(typeof(VihicleType), val);
			bool isDi = Boolean.Parse (Ultil.GetString (TileKey.DI + name, "true", gridtile.tile.properties));
			bool isDung = Boolean.Parse (Ultil.GetString (TileKey.DUNG + name, "true", gridtile.tile.properties));

			listData.Add (new ComboCheckboxData (name, isDi, isDung));
		}

		cbLoaiXe.Init (listData);
	}

	#region EVENT HANDLER

	public void OnResizeLeft () {
		if (gridtile == null) return;

		int step = STEP_RESIZE;
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			step *= -1;
		}

		UITexture tt = gridtile.GetComponent <UITexture> ();
		tt.type = UIBasicSprite.Type.Simple;
		tt.pivot = UIWidget.Pivot.Right;
		tt.width = tt.width + step;
		tt.pivot = UIWidget.Pivot.Center;

		BoxCollider box = gridtile.GetComponent <BoxCollider> ();
		box.size = new Vector3 (tt.width, tt.height, 0);
	}

	public void OnResizeRight () {
		if (gridtile == null) return;
		
		int step = STEP_RESIZE;
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			step *= -1;
		}

		UITexture tt = gridtile.GetComponent <UITexture> ();
		tt.type = UIBasicSprite.Type.Simple;
		tt.pivot = UIWidget.Pivot.Left;
		tt.width = tt.width + step;
		tt.pivot = UIWidget.Pivot.Center;
		
		BoxCollider box = gridtile.GetComponent <BoxCollider> ();
		box.size = new Vector3 (tt.width, tt.height, 0);
	}

	public void OnResizeTop () {
		if (gridtile == null) return;
		
		int step = STEP_RESIZE;
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			step *= -1;
		}

		UITexture tt = gridtile.GetComponent <UITexture> ();
		tt.type = UIBasicSprite.Type.Simple;
		tt.pivot = UIWidget.Pivot.Bottom;
		tt.height = tt.height + step;
		tt.pivot = UIWidget.Pivot.Center;
		
		BoxCollider box = gridtile.GetComponent <BoxCollider> ();
		box.size = new Vector3 (tt.width, tt.height, 0);
	}

	public void OnResizeBottom () {
		if (gridtile == null) return;
		
		int step = STEP_RESIZE;
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			step *= -1;
		}

		UITexture tt = gridtile.GetComponent <UITexture> ();
		tt.type = UIBasicSprite.Type.Simple;
		tt.pivot = UIWidget.Pivot.Top;
		tt.height = tt.height + step;
		tt.pivot = UIWidget.Pivot.Center;
		
		BoxCollider box = gridtile.GetComponent <BoxCollider> ();
		box.size = new Vector3 (tt.width, tt.height, 0);
	}

	#endregion
}
