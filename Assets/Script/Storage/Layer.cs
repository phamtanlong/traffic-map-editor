using UnityEngine;
using System.Collections;

public class Layer {

	public string name;
	public int id;

	public Layer () {
		name = "";
		id = 0;
	}

	public Layer Copy () {
		Layer p = new Layer ();
		p.name = this.name;
		p.id = this.id;
		return p;
	}
}
