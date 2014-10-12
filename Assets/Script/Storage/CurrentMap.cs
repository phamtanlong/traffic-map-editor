using UnityEngine;
using System.Collections;

public class CurrentMap {

	private static Map _instance = null;

	public static Map Instance {
		get {
			if (_instance == null) {
				_instance = new Map ();
			}

			return _instance;
		}
	}
}
