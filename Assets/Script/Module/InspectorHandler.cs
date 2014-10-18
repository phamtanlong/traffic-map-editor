using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InspectorHandler : MonoBehaviour {

	public static bool IsFocus {
		get {
			return Instance.inpInfo.isSelected;
		}
	}
	public static InspectorHandler Instance = null;
	void Awake () {
		Instance = this;
	}

	[HideInInspector]
	public Tile SelectedTile = null;

	public UIInput inpInfo;

	void Start () {}
	void Update () {}

	public void SetSelectedTile (Tile t) {
		SelectedTile = t;
		string s = "";
		foreach (KeyValuePair<string, string> p in SelectedTile.properties) {
			s += p.Key + "=" + p.Value + "\n";
		}
		inpInfo.value = s;
	}

	public void OnSave () {
		string s = inpInfo.value;

		Dictionary<string, string> dict = new Dictionary<string, string> ();
		bool isOK = true;

		string[] ss = s.Split ('\n');
		for (int i = 0; i < ss.Length; ++i) {
			string[] sss = ss[i].Split ('=');
			if (sss.Length == 2 && sss[0].Length > 0 && sss[1].Length > 0) {
				dict[sss[0]] = sss[1];
			} else {
				isOK = false;
				DialogHandler.Instance.ShowDialogMessage ("Error", "Format is not valid :(");
			}
		}

		if (isOK) {
			SelectedTile.properties = dict;
		}
	}
}
