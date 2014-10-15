using UnityEngine;
using System;
using System.Collections;

public class GridTileHandler : MonoBehaviour {

	public Tile tile;

	void Start () {}
	void Update () {}

	public void OnSelect (bool isSelect) {
		if (isSelect) {
			DrawPanelHandler.Instance.SelectedGridTile = this;
			GetComponent<UITexture>().color = new Color (1.0f,0.5f,0.5f,1);
		} else {
			GetComponent<UITexture>().color = Color.white;
		}
	}

	public void Init (TileHandler ins) {

		this.tile = ins.tile.Copy ();

		//edit
		ins.gameObject.layer = 9;
		
		UITexture tt = ins.GetComponent<UITexture>();
		tt.width = tt.mainTexture.width;
		tt.height = tt.mainTexture.height;
		
		ins.gameObject.AddComponent (typeof (DragableObject1));
		
		//remove
		//Destroy (ins.GetComponent<BoxCollider>());
		Destroy (ins.GetComponent<UIButton>());
		Destroy (ins.GetComponent<UIPlaySound>());
		Destroy (ins.GetComponent<UIDragScrollView>());
		Destroy (ins.GetComponent<TweenColor>());
		Destroy (ins.GetComponent<TileHandler>());
		
		foreach (Transform c in ins.transform) {
			GameObject.Destroy(c.gameObject);
		}
		
		ins.GetComponent<UITexture>().color = Color.white;
	}
}
