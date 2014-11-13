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

	public void Init (Tile tile, int newTileId) {

		//edit
		gameObject.layer = 9;
		
		UITexture tt = gameObject.GetComponent<UITexture>();
		tt.width = tt.mainTexture.width;
		tt.height = tt.mainTexture.height;
		tt.color = Color.white;

		int depth = ROAD_DEPTH;
		switch (tile.layerType) {
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

		BoxCollider b = gameObject.AddComponent<BoxCollider>();
		b.size = new Vector3 (tt.mainTexture.width, tt.mainTexture.height, 1);
		
		gameObject.AddComponent <DragableObject1> ();
	}

	
	public void InitDefaultTileData () {
		switch (tile.layerType) {
		case LayerType.Road:
			InitRoadData ();
			break;

		case LayerType.Sign:
			InitSignData ();
			break;

		case LayerType.Other:
			InitOtherData ();
			break;
		}
	}

	private void InitRoadData () {

		//Velocity
		Ultil.GetString (TileKey.MIN_VEL, "0", tile.properties);
		Ultil.GetString (TileKey.MAX_VEL, "40", tile.properties);
		
		//Huong Re
		Ultil.GetString (TileKey.RE_TRAI, "true", tile.properties);
		Ultil.GetString (TileKey.RE_PHAI, "true", tile.properties);
		Ultil.GetString (TileKey.RE_THANG, "true", tile.properties);

		foreach (VihicleType val in Enum.GetValues(typeof(VihicleType)))
		{
			string name = Enum.GetName(typeof(VihicleType), val);
			Ultil.GetString (TileKey.DI + name, "true", tile.properties);
			Ultil.GetString (TileKey.DUNG + name, "true", tile.properties);
		}
	}

	private void InitSignData () {
		Ultil.GetString (TileKey.SIGN_DIR, "UP", tile.properties);
	}

	private void InitOtherData () {
		if (tile.typeId == 301) { //light
			//GroupID
			Ultil.GetString (TileKey.LIGHT_GROUP_ID, "0", tile.properties);
			
			//Huong
			Ultil.GetString (TileKey.LIGHT_HUONG, "UP", tile.properties);
			
			//LanDuong
			Ultil.GetString (TileKey.LIGHT_LAN_DUONG, "0", tile.properties);
		}
	}
}
