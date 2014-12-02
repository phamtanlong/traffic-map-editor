using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public struct TileKey {
	public static string MIN_VEL = "MIN_VEL";
	public static string MAX_VEL = "MAX_VEL";
	public static string RE_TRAI = "RE_TRAI";
	public static string RE_PHAI = "RE_PHAI";
	public static string RE_THANG = "RE_THANG";
	public static string RE_QUAY_DAU = "QUAY_DAU";
	public static string LOAI_XE = "LOAI_XE";
	public static string DI = "DI_";
	public static string DUNG = "DUNG_";
	public static string LE_TRAI = "LE_TRAI";
	public static string LE_PHAI = "LE_PHAI";
	public static string LE_TREN = "LE_TREN";
	public static string LE_DUOI = "LE_DUOI";
	//
	public static string PCACH_TRAI = "PCACH_TRAI";
	public static string PCACH_PHAI = "PCACH_PHAI";
	public static string PCACH_TREN = "PCACH_TREN";
	public static string PCACH_DUOI = "PCACH_DUOI";

	//Light

	public static string LIGHT_GROUP_ID = "LIGHT_GROUP_ID";
	public static string LIGHT_HUONG = "LIGHT_HUONG";
	public static string LIGHT_LAN_DUONG = "LIGHT_LAN_DUONG";

	//Sign

	public static string SIGN_DIR = "SIGN_DIR";
	public static string SIGN_MAX_TOCDO = "SIGN_MAX_TOCDO";
	public static string SIGN_MIN_TOCDO = "SIGN_MIN_TOCDO";

	//Auto Car
	public static string AUTOCAR_DIR = "AUTOCAR_DIR";
}



[System.Serializable]
public class Tile {

	public int objId;
	public int typeId;
	public LayerType layerType;
	public float x;
	public float y;
	public float w;
	public float h;
	public Dictionary<string, string> properties;

	public Tile () {
		properties = new Dictionary<string, string> ();
	}

	public Tile Copy () {
		Tile t = new Tile ();
		t.objId = this.objId;
		t.typeId = this.typeId;
		t.layerType = this.layerType;
		t.x = x;
		t.y = y;
		t.w = w;
		t.h = h;
	
		foreach (KeyValuePair<string, string> p in this.properties) {
			t.properties[p.Key] = p.Value;
		}

		return t;
	}
}
