using UnityEngine;
using System.Collections;

public class GridHandler : MonoBehaviour {

	public KeyCode keyToEnable=KeyCode.Space;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (keyToEnable) == true) {
			this.GetComponent<BoxCollider>().enabled = true;
		} else {
			this.GetComponent<BoxCollider>().enabled = false;
		}
	}
}
