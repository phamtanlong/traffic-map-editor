using UnityEngine;
using System.Collections;

public class Map {
	
	public string name;
	public int width;
	public int height;
	public int simulateTime;
	
	public Map () {
		name = "Map";
		width = 120; //1280
		height = 120; //768
		simulateTime = 8;
	}
}
