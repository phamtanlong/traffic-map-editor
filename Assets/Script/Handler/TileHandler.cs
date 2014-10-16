using UnityEngine;
using System.Collections;

public class TileHandler : MonoBehaviour {

	public Tile tile;

	public UISprite sprSelect;
	public UILabel lblId;
	
	private bool isSelected = false;

	void Start () {}
	void Update () {}

	public void Setup (Tile tile) {
		this.tile = tile;

		lblId.text = ""+tile.typeId;
	}

	public void OnClick () {
		if (ToolboxHandler.Instance.SelectedTile != null) {
			ToolboxHandler.Instance.SelectedTile.Select (false);
		}

		ToolboxHandler.Instance.SelectedTile = this;
		ToolboxHandler.Instance.SelectedTile.Select (true);

		Global.currentTile = this.tile;
	}

	public void Select (bool select) {
		isSelected = select;
		if (sprSelect != null) {
			sprSelect.gameObject.SetActive (isSelected);
		}
	}
}
