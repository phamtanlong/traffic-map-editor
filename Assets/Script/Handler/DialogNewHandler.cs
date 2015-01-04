using UnityEngine;
using System.Collections;

public class DialogNewHandler : MonoBehaviour {
	
	public UIInput inpWidth;
	public UIInput inpHeight;
	public UIInput inpName;
	public UIInput inHour;

	void Start () {
		inpWidth.value = ""+1000;
		inpHeight.value = ""+1000;
	}

	public void OnCreateNewMap () {
		this.gameObject.SetActive (false);
		Main.Instance.log.text = "Create";

		int w = int.Parse (inpWidth.value);
		int h = int.Parse (inpHeight.value);

		w = (int)(w * GameConst.MET_TO_PIXEL / GameConst.TILE_WIDTH);
		h = (int)(h * GameConst.MET_TO_PIXEL / GameConst.TILE_HEIGHT);

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
