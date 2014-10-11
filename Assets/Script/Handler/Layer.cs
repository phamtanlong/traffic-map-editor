using UnityEngine;
using System.Collections;

public class Layer : MonoBehaviour {

	public class Param {
		public string name;
		public int id;

		public Param Copy () {
			Param p = new Param ();
			p.name = this.name;
			p.id = this.id;
			return p;
		}
	}

	public UILabel lblName;
	public UISprite sprSelect;

	private Param param;

	public void Setup (Param _param) {
		this.param = _param.Copy ();

		lblName.text = param.name;
	}

	public void OnSelect (bool s) {
		sprSelect.gameObject.SetActive (s);
		ExplorerHandler.Instance.selectedLayer = this;
	}
}
