using UnityEngine;
using System.Collections;

public class run : MonoBehaviour {

	public GameObject prefab;

	// Use this for initialization
	void Start () {
		VerticalGridView grid = this.GetComponent <VerticalGridView> ();

		for (int i = 0; i < 10; ++i) {
			GameObject go = GameObject.Instantiate (prefab) as GameObject;
			go.name = ""+i;
			grid.AddChild (go);
			go.transform.localScale = Vector3.one;
		}
	}
}
