using UnityEngine;
using System.Collections;

public class DialogHelpHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClose () {
		this.gameObject.SetActive (false);
	}
}
