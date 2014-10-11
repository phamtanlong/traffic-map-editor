using UnityEngine;
using System.Collections;

public class Ultil {

	private static int id = 0;

	public static int GetNewLayerId () {
		id++;
		return id;
	}
}
