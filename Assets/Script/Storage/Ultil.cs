using UnityEngine;
using System;
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

	public static void InitDefaultValue (Tile tile) {

		//---------------- ROAD -------------------

		if (tile.layerType == LayerType.Road) {
			//Velocity
			Ultil.GetString (TileKey.MIN_VEL, "0", tile.properties);
			Ultil.GetString (TileKey.MAX_VEL, "40", tile.properties);
			
			//Huong Re
			Ultil.GetString (TileKey.RE_TRAI, "true", tile.properties);
			Ultil.GetString (TileKey.RE_PHAI, "true", tile.properties);
			Ultil.GetString (TileKey.RE_THANG, "true", tile.properties);
			
			foreach (VihicleType val in Enum.GetValues(typeof(VihicleType)))
			{
				string name = Enum.GetName(typeof(VihicleType), val);
				Ultil.GetString (TileKey.DI + name, "true", tile.properties);
				Ultil.GetString (TileKey.DUNG + name, "true", tile.properties);
			}

			switch (tile.typeId) {

			case TileID.ROAD_DOWN:
				Ultil.GetString (TileKey.LE_TRAI, "true", tile.properties);
				break;
				
			case TileID.ROAD_LEFT:
				Ultil.GetString (TileKey.LE_TREN, "true", tile.properties);
				break;
				
			case TileID.ROAD_RIGHT:
				Ultil.GetString (TileKey.LE_DUOI, "true", tile.properties);
				break;
				
			case TileID.ROAD_UP:
				Ultil.GetString (TileKey.LE_PHAI, "true", tile.properties);
				break;

			}
		}

		//------------------ AUTO_CAR --------------------
		if (tile.typeId == TileID.AUTOCAR || tile.typeId == TileID.AUTOBIKE) {
			//Direction
			Ultil.GetString (TileKey.AUTOCAR_DIR, "UP", tile.properties);
		}
	}
}






