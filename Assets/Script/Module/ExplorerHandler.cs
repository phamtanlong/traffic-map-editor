using UnityEngine;
using System.Collections;

public class ExplorerHandler : MonoBehaviour {

	public static ExplorerHandler Instance;
	void Awake () {
		Instance = this;
	}

	public bool isShow = true;
	public GameObject fullObj;
	public UIScrollView scrollView;
	public UIGrid parent;

	private LayerHandler selectedLayer;
	public LayerHandler GetSelectedLayer () {
		return selectedLayer;
	}
	public void SetSelectedLayer (LayerHandler l) {
		for (int i = 0; i < Main.listLayer.Count; ++i) {
			Main.listLayer[i].Select (false);
		}

		selectedLayer = l;

		if (selectedLayer != null) {
			selectedLayer.Select (true);
		}
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
		Main.Instance.NewLayer (l);
	}

	public void OnRemoveLayer () {
		if (selectedLayer != null) {

			Main.Instance.RemoveLayer (selectedLayer);
			
			parent.RemoveChild (selectedLayer.transform);
			GameObject.Destroy (selectedLayer.gameObject);
			if (Main.listLayer.Count > 0) {
				SetSelectedLayer (Main.listLayer[Main.listLayer.Count-1]);
			} else {
				SetSelectedLayer (null);
			}
		}
	}

	#endregion
}
