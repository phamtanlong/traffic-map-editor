using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RoadInspector : IInspector {

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
		Debug.Log ("save");
		//Coi
		tile.properties[TileKey.COI] = "" + cbCoi.value;

		//Chieu
		tile.properties[TileKey.CHIEU] = popChieu.value;

		//Velocity
		tile.properties[TileKey.MIN_VEL] = inpTocDoMin.value;
		tile.properties[TileKey.MAX_VEL] = inpTocDoMax.value;

		//Huong Re
		tile.properties[TileKey.RE_TRAI] = ""+cbReTrai.value;
		tile.properties[TileKey.RE_PHAI] = ""+cbRePhai.value;
		tile.properties[TileKey.RE_THANG] = ""+cbReThang.value;

		//Loai Xe
		for (int i = 0; i < cbLoaiXe.listItems.Count; ++i) {
			ComboCheckboxItem item = cbLoaiXe.listItems[i];

			tile.properties[TileKey.DI + item.lbTitle.text] = ""+item.Check1;
			tile.properties[TileKey.DUNG + item.lbTitle.text] = ""+item.Check2;
		}
	}

	public override void Init (Tile tile) {
		if (tile.layerType != LayerType.Road) {
			Debug.Log ("Wrong tile");
			return;
		}

		this.tile = tile;

		//Coi
		bool isCoi = Boolean.Parse (Ultil.GetString (TileKey.COI, "true", tile.properties));
		cbCoi.value = isCoi;

		//Chieu
		string dir = Ultil.GetString (TileKey.CHIEU, "UP", tile.properties);
		popChieu.value = dir;

		//Velocity
		int minVel = int.Parse (Ultil.GetString (TileKey.MIN_VEL, "0", tile.properties));
		int maxVel = int.Parse (Ultil.GetString (TileKey.MAX_VEL, "40", tile.properties));
		inpTocDoMin.value = ""+minVel;
		inpTocDoMax.value = ""+maxVel;

		//Huong Re
		bool isReTrai = Boolean.Parse (Ultil.GetString (TileKey.RE_TRAI, "true", tile.properties));
		bool isRePhai = Boolean.Parse (Ultil.GetString (TileKey.RE_PHAI, "true", tile.properties));
		bool isReThang = Boolean.Parse (Ultil.GetString (TileKey.RE_THANG, "true", tile.properties));
		cbReTrai.value = isReTrai;
		cbRePhai.value = isRePhai;
		cbReThang.value = isReThang;
		
		List<ComboCheckboxData> listData = new List<ComboCheckboxData> ();
		foreach (VihicleType val in Enum.GetValues(typeof(VihicleType)))
		{
			string name = Enum.GetName(typeof(VihicleType), val);
			bool isDi = Boolean.Parse (Ultil.GetString (TileKey.DI + name, "true", tile.properties));
			bool isDung = Boolean.Parse (Ultil.GetString (TileKey.DUNG + name, "true", tile.properties));

			listData.Add (new ComboCheckboxData (name, isDi, isDung));
		}

		cbLoaiXe.Init (listData);
	}
}
