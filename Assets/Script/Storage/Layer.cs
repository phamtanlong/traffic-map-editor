using UnityEngine;
using System.Collections;

public enum LayerType {
	Road,
	Sign,
	View
}

[System.Serializable]
public class Layer {

	public LayerType type;
	public string name;
	public int id;

	public Layer () {
		type = LayerType.Road;
		name = "";
		id = 0;
	}

	public Layer Copy () {
		Layer p = new Layer ();
		p.type = this.type;
		p.name = this.name;
		p.id = this.id;
		return p;
	}
}
