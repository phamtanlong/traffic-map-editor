using UnityEngine;
using System.Collections;

public class DialogEditHandler : MonoBehaviour {

	public UIInput inpWidth;
	public UIInput inpHeight;
	public UIInput inpName;

	void OnEnable() {
		inpWidth.value = ""+Global.currentMap.width;
		inpHeight.value = ""+Global.currentMap.height;
		inpName.value = ""+Global.currentMap.name;
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
		
		Global.currentMap.width = w;
		Global.currentMap.height = h;
		Global.currentMap.name = inpName.value;

		Main.Instance.EditMap ();
	}
	
	public void OnEditMapCancel () {
		this.gameObject.SetActive (false);
		Main.Instance.log.text = "Edit Cancel";
	}
}
