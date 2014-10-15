using UnityEngine;
using System.Collections;


[System.Serializable]
public class Tile {

	public long objId;
	public int typeId;
	public int layerId;

	public Tile () {
		objId = -1;
		typeId = -1;
		layerId = -1;
	}

	public Tile Copy () {
		Tile t = new Tile ();
		t.objId = this.objId;
		t.typeId = this.typeId;
		t.layerId = this.layerId;

		return t;
	}
}
