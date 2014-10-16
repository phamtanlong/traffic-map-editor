using UnityEngine;
using System.Collections;

public class Ultil {

	private static int layerId = 0;
	private static int objId = 0;

	public static void ResetLayerId () {
		layerId = 0;
	}
	public static int GetNewLayerId () {
		layerId++;
		return layerId;
	}

	public static void ResetObjId () {
		objId = 0;
	}
	public static int GetNewObjId () {
		objId++;
		return objId;
	}
}
