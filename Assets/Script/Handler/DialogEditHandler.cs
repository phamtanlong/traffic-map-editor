using UnityEngine;
using System.Collections;

public class DialogEditHandler : MonoBehaviour {

	public UIInput inpWidth;
	public UIInput inpHeight;
	public UIInput inpName;

	void OnEnable() {
		inpWidth.value = ""+CurrentMap.Instance.width;
		inpHeight.value = ""+CurrentMap.Instance.height;
		inpName.value = ""+CurrentMap.Instance.name;
	}

	public void OnEditMapOK () {
		this.gameObject.SetActive (false);
		Main.Instance.log.text = "Edit OK";

		int w = int.Parse (inpWidth.value);
		int h = int.Parse (inpHeight.value);
		
		if (w % 2 != 0) {
			w += 1;
		}
		if (h % 2 != 0) {
			h += 1;
		}
		
		CurrentMap.Instance.width = w;
		CurrentMap.Instance.height = h;
		CurrentMap.Instance.name = inpName.value;

		Main.Instance.EditMap ();
	}
	
	public void OnEditMapCancel () {
		this.gameObject.SetActive (false);
		Main.Instance.log.text = "Edit Cancel";
	}
}
