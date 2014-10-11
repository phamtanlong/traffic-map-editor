using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {

	public static Main Instance;

	public static List<Layer> listLayer = new List<Layer> ();

	void Awake () {
		Instance = this;
	}

	public void NewLayer () {
		int id = Ultil.GetNewLayerId ();
		string name = "layer " + id;

		Layer.Param p = new Layer.Param ();
		p.name = name;
		p.id = id;

		Layer l = ExplorerHandler.Instance.NewLayer (p);
		listLayer.Add (l);
	}
}
