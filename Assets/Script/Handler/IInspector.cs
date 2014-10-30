using UnityEngine;
using System.Collections;

public enum VihicleType {
	XeMay,
	MoTo,
	Oto,
	XeDap,
	XeKhach,
	XeTai,
	Romooc,
	XeLam,
	XichLo,
	ThoSo,
}

public enum MyDirection {
	UP,
	DOWN,
	LEFT,
	RIGHT
}

public struct TileKey {
	public static string COI = "COI";
	public static string CHIEU = "CHIEU";
	public static string MIN_VEL = "MIN_VEL";
	public static string MAX_VEL = "MAX_VEL";
	public static string RE_TRAI = "RE_TRAI";
	public static string RE_PHAI = "RE_PHAI";
	public static string RE_THANG = "RE_THANG";
	public static string LOAI_XE = "LOAI_XE";
	public static string DI = "DI_";
	public static string DUNG = "DUNG_";

	//Light

	public static string LIGHT_GROUP_ID = "LIGHT_GROUP_ID";
	public static string LIGHT_HUONG = "LIGHT_HUONG";
	public static string LIGHT_LAN_DUONG = "LIGHT_LAN_DUONG";

	//Sign

	public static string SIGN_DIR = "SIGN_DIR";

}

public class IInspector : MonoBehaviour {

	public GridTileHandler gridtile;

	void Start () {}

	void Update () {}

	public virtual void Save () {}

	public virtual void Init (GridTileHandler gridtile) {}
}
