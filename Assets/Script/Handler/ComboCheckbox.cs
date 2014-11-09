using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ComboCheckboxData {
	public string title = "";
	public bool check1 = false;
	public bool check2 = false;

	public ComboCheckboxData () {}

	public ComboCheckboxData (string t, bool b1, bool b2) {
		title = t;
		check1 = b1;
		check2 = b2;
	}
}

public class ComboCheckbox : MonoBehaviour {

	public UIButton button;
	public GameObject viewPanel;
	public UIScrollView scrollView;
	public GameObject prefab;
	public VerticalGridView verticalGrid;

	public List<ComboCheckboxItem> listItems;

	private bool isShow = false;

	public Action onValueChange = null;

	void Start () {
		isShow = false;
		RefreshState ();
	}

	void Update () {}

	public void Init (List<ComboCheckboxData> listData, Action callback) {

		this.onValueChange = callback;

		foreach (Transform t in verticalGrid.transform) {
			GameObject.Destroy (t.gameObject);
		}
		
		listItems = new List<ComboCheckboxItem> ();

		for (int i = 0; i < listData.Count; ++i) {
			GameObject ins = GameObject.Instantiate (prefab) as GameObject;
			ComboCheckboxItem item = ins.GetComponent<ComboCheckboxItem>();
			item.Init (listData[i].title, listData[i].check1, listData[i].check2, this.onValueChange);


			verticalGrid.AddChild (ins);
			ins.transform.localScale = Vector3.one;

			listItems.Add (item);
		}

		scrollView.ResetPosition ();
		scrollView.Scroll (0.01f);
	}

	private void RefreshState () {
		viewPanel.SetActive (isShow);
		
		scrollView.ResetPosition ();
		scrollView.Scroll (0.01f);
	}

	public void OnButtonClick () {
		isShow = ! isShow;
		RefreshState ();
	}
}
