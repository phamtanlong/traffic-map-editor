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

	private Layer selectedLayer;
	public Layer GetSelectedLayer () {
		return selectedLayer;
	}
	public void SetSelectedLayer (Layer l) {
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

	public Layer NewLayer (Layer.Param p) {
		GameObject ins = GameObject.Instantiate (prefabLayer) as GameObject;
		Layer l = ins.GetComponent <Layer> ();
		l.Setup (p);

		l.transform.parent = parent.transform;
		l.transform.localScale = Vector3.one;

		parent.AddChild (l.transform);
		parent.Reposition ();

		SetSelectedLayer (l);

		return l;
	}

	#region Event Handler
	
	public void OnNewLayer () {
		Main.Instance.NewLayer ();
	}

	public void OnRemoveLayer () {
		if (selectedLayer != null) {
			Main.listLayer.Remove (selectedLayer);
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
