using UnityEngine;
using System.Collections;

public class ExplorerHandler : MonoBehaviour {

	public static ExplorerHandler Instance;
	void Awake () {
		Instance = this;
	}

	public bool isShow = true;
	public UIScrollView scrollView;
	public UIGrid parent;
	public Layer selectedLayer;

	public GameObject prefabLayer;

	public void OnShowHide () {
		isShow = ! isShow;
		scrollView.gameObject.SetActive (isShow);
	}

	public Layer NewLayer (Layer.Param p) {
		GameObject ins = GameObject.Instantiate (prefabLayer) as GameObject;
		Layer l = ins.GetComponent <Layer> ();
		l.Setup (p);

		l.transform.parent = parent.transform;
		l.transform.localPosition = Vector3.zero;
		l.transform.localScale = Vector3.one;

		parent.AddChild (l.transform);
		parent.Reposition ();

		//scrollView.Scroll (0.01f);
		//scrollView.ResetPosition ();

		return l;
	}
}
