using UnityEngine;
using System.Collections;

public class ComboCheckboxItem : MonoBehaviour {
	
	public UILabel lbTitle;
	public UIToggle checkbox1;
	public UIToggle checkbox2;

	void Start () {}
	void Update () {}

	public void Init (string title, bool check1, bool check2) {
		lbTitle.text = title;
		checkbox1.value = check1;
		checkbox2.value = check2;
	}

	public string Title {
		get {
			return lbTitle.text;
		}

		set {
			lbTitle.text = value;
		}
	}

	public bool Check1 {
		get {
			return checkbox1.value;
		}

		set {
			checkbox1.value = value;
		}
	}

	public bool Check2 {
		get {
			return checkbox2.value;
		}

		set {
			checkbox2.value = value;
		}
	}
}
