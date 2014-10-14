using UnityEngine;
using System.Collections;

public class CurrentLayer : MonoBehaviour {
	
	private static LayerHandler _instance;
	
	public static LayerHandler Instance {
		get {
			return _instance;
		}
		set {
			_instance = value;
		}
	}
}
