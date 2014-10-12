using UnityEngine;
using System.Collections;

public class CurrentTile  {

	private static TileHandler _instance;

	public static TileHandler Instance {
		get {
			return _instance;
		}
		set {
			_instance = value;
		}
	}
}
