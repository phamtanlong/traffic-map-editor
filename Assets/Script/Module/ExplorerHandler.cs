using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplorerHandler : MonoBehaviour {

	public static ExplorerHandler Instance;
	void Awake () {
		Instance = this;
	}

	void Start () {
		OnNewLayer ();
	}
	
	public static List<LayerHandler> listLayer = new List<LayerHandler> ();

	public bool isShow = true;
	public GameObject fullObj;
	public UIScrollView scrollView;
	public UIGrid parent;
	public static LayerHandler SelectedLayer = null;

	public void Reset () {
		for (int i = 0; i < listLayer.Count; ++i) {
			GameObject.Destroy (listLayer[i].gameObject);
		}
		listLayer.Clear ();

		SelectedLayer = null;
		OnNewLayer ();
	}

	public void SetSelectedLayer (LayerHandler l) {
		for (int i = 0; i < listLayer.Count; ++i) {
			listLayer[i].Select (false);
		}

		ExplorerHandler.SelectedLayer = l;
		if (l != null) {
			Global.currentLayer = l.layer;
		} else {
			Global.currentLayer = null;
		}

		if (ExplorerHandler.SelectedLayer != null) {
			ExplorerHandler.SelectedLayer.Select (true);
		}

		Main.Instance.SetSelectedLayer ();
	}

	public GameObject prefabLayer;

	public void OnShowHide () {
		isShow = ! isShow;
		fullObj.SetActive (isShow);
	}

	#region Event Handler
	
	public void OnNewLayer () {
		int id = Ultil.GetNewLayerId ();
		string name = "layer " + id;
		
		Layer p = new Layer ();
		p.name = name;
		p.id = id;

		GameObject ins = GameObject.Instantiate (prefabLayer) as GameObject;
		LayerHandler l = ins.GetComponent <LayerHandler> ();
		l.Setup (p);
		l.transform.parent = parent.transform;
		l.transform.localScale = Vector3.one;
		
		parent.AddChild (l.transform);
		parent.Reposition ();
		
		SetSelectedLayer (l);

		//
		listLayer.Add (l);
		Main.Instance.NewLayer (l);
	}

	public void OnRemoveLayer () {
		if (ExplorerHandler.SelectedLayer != null) {

			LayerHandler l = ExplorerHandler.SelectedLayer;
			
			listLayer.Remove (l);
			Main.Instance.RemoveLayer (l);

			parent.RemoveChild (l.transform);
			GameObject.Destroy (l.gameObject);

			if (listLayer.Count > 0) {
				SetSelectedLayer (listLayer[listLayer.Count-1]);
			} else {
				SetSelectedLayer (null);
			}
		}
	}

	#endregion
}
