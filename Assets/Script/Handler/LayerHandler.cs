using UnityEngine;
using System.Collections;

public class LayerHandler : MonoBehaviour {

	public UILabel lblName;
	public UISprite sprSelect;

	public Layer layer;

	bool isSelected = false;

	public void Setup (Layer l) {
		this.layer = l.Copy ();

		lblName.text = layer.name;
	}

	public void OnClick () {
		isSelected = ! isSelected;
		Select (isSelected);
		ExplorerHandler.Instance.SetSelectedLayer (this);
	}

	public void Select (bool istrue) {
		isSelected = istrue;
		sprSelect.gameObject.SetActive (isSelected);
	}
}
