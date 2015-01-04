using UnityEngine;
using System.Collections;

public class DialogEditHandler : MonoBehaviour {

	public UIInput inpWidth;
	public UIInput inpHeight;
	public UIInput inpName;
	public UIInput inpHour;

	void OnEnable() {
		int w = Global.currentMap.width;
		int h = Global.currentMap.height;

		w = (int)(w * GameConst.TILE_WIDTH / GameConst.MET_TO_PIXEL);
		h = (int)(h * GameConst.TILE_HEIGHT / GameConst.MET_TO_PIXEL);

		inpWidth.value = ""+w;
		inpHeight.value = ""+h;
		inpName.value = ""+Global.currentMap.name;
		inpHour.value = ""+Global.currentMap.simulateTime;
	}

	public void OnEditMapOK () {
		this.gameObject.SetActive (false);
		Main.Instance.log.text = "Edit OK";

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
		Global.currentMap.simulateTime = int.Parse (inpHour.value);

		Main.Instance.EditMap ();
	}
	
	public void OnEditMapCancel () {
		this.gameObject.SetActive (false);
		Main.Instance.log.text = "Edit Cancel";
	}
}
