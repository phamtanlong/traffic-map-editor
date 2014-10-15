using UnityEngine;
using System.Collections;

public class Ultil {

	private static int layerId = 0;
	private static long objId = 0;

	public static int GetNewLayerId () {
		layerId++;
		return layerId;
	}

	public static long GetNewObjId () {
		objId++;
		return objId;
	}
}
