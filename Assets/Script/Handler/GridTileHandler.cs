using UnityEngine;
using System;
using System.Collections;

public class GridTileHandler : MonoBehaviour {

	public static int ROAD_DEPTH = 0;
	public static int SIGN_DEPTH = 700000000;
	public static int VIEW_DEPTH = 1400000000;


	public Tile tile;

	void Start () {}
	void Update () {}

	bool isSelected = false;

	public void Select (bool select) {
		isSelected = select;
		if (select) {
			GetComponent<UITexture>().color = new Color (1.0f,0.5f,0.5f,1);
		} else {
			GetComponent<UITexture>().color = Color.white;
		}
	}

	void OnClick () {
		if (isSelected == false) {
			isSelected = true;
			Select (true);

			if (DrawPanelHandler.Instance.SelectedGridTile != null) {
				DrawPanelHandler.Instance.SelectedGridTile.Select (false);
			}
			DrawPanelHandler.Instance.SelectedGridTile = this;
		}
		
		DrawPanelHandler.Instance.OnChangeSelectedTile ();
	}

	public void Init (TileHandler ins, int newTileId) {

		//edit
		ins.gameObject.layer = 9;
		
		UITexture tt = ins.GetComponent<UITexture>();
		tt.width = tt.mainTexture.width;
		tt.height = tt.mainTexture.height;

		int depth = ROAD_DEPTH;
		switch (ins.tile.layerType) {
		case LayerType.Road:
			depth = ROAD_DEPTH;
			break;
		case LayerType.Sign:
			depth = SIGN_DEPTH;
			break;
		case LayerType.View:
			depth = VIEW_DEPTH;
			break;
		case LayerType.Other:
			depth = SIGN_DEPTH;
			break;
		}
		depth += newTileId;
		tt.depth = depth;

		BoxCollider b = ins.GetComponent<BoxCollider>();
		b.size = new Vector3 (tt.mainTexture.width, tt.mainTexture.height, 1);
		
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
