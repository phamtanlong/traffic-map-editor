using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public static string GetString (string key, string defaul, Dictionary<string,string> dict) {
		string value = null;
		dict.TryGetValue (key, out value);

		if (string.IsNullOrEmpty (value)) {
			value = defaul;
			dict[key] = value;
		}

		return value;
	}
}
