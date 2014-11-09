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

	public static void ResetObjId (int defaultId = 0) {
		objId = defaultId;
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
	
	public static int CompareTextureName(Texture x, Texture y) {
		int xx = int.Parse (x.name);
		int yy = int.Parse (y.name);
		
		return xx - yy;
	}

	public static string GetFolderTile (string tileName) {
		int v = int.Parse (tileName);

		if (v <= 100) {
			return "1";
		} else if (v <= 200) {
			return "100";
		} else if (v <= 300) {
			return "200";
		} else if (v <= 400) {
			return "300";
		}

		Debug.LogError ("Wrong Tile Name");

		return "";
	}

}






