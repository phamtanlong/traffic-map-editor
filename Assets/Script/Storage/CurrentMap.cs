using UnityEngine;
using System.Collections;

public class CurrentMap {

	private CurrentMap _instance = null;

	public CurrentMap Instance {
		get {
			if (_instance == null) {
				_instance = new CurrentMap ();
			}

			return _instance;
		}
	}

	public string name;
	public int width;
	public int height;

	private CurrentMap () {
		name = "";
		width = 0;
		height = 0;
	}
}
