﻿using UnityEngine;
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

	public void SetSelectedLayer (LayerHandler l) {
		for (int i = 0; i < listLayer.Count; ++i) {
			listLayer[i].Select (false);
		}

		CurrentLayer.Instance = l;

		if (CurrentLayer.Instance != null) {
			CurrentLayer.Instance.Select (true);
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
		if (CurrentLayer.Instance != null) {

			LayerHandler l = CurrentLayer.Instance;
			
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
