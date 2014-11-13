using UnityEngine;
using System.Collections;

public class DialogNewHandler : MonoBehaviour {
	
	public UIInput inpWidth;
	public UIInput inpHeight;
	public UIInput inpName;
	public UIInput inHour;

	public void OnCreateNewMap () {
		this.gameObject.SetActive (false);
		Main.Instance.log.text = "Create";

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
		Global.currentMap.simulateTime = int.Parse (inHour.value);

		Main.Instance.NewMap ();
	}
	
	public void OnCancelNewMap () {
		this.gameObject.SetActive (false);
		Main.Instance.log.text = "Cancel";
	}

}
