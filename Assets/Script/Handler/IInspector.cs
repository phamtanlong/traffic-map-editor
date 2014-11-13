using UnityEngine;
using System.Collections;

public enum VihicleType {
	MoToA1,
	MoToA2,
	MoToA3,
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

public class IInspector : MonoBehaviour {

	public GridTileHandler gridtile;

	void Start () {}

	void Update () {}

	public virtual void Save () {}

	public virtual void Init (GridTileHandler gridtile) {}
}
