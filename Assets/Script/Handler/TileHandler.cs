using UnityEngine;
using System.Collections;

public class TileHandler : MonoBehaviour {

	public Tile tile;

	public bool isSelected = false;
	public UISprite sprSelect;
	public UILabel lblId;
	
	public void Setup (Tile tile) {
		this.tile = tile;

		lblId.text = ""+tile.id;
	}

	public void OnClick () {
		if (CurrentTile.Instance != null) {
			CurrentTile.Instance.Select (false);
		}

		CurrentTile.Instance = this;
		CurrentTile.Instance.Select (true);
	}

	public void Select (bool select) {
		isSelected = select;
		sprSelect.gameObject.SetActive (isSelected);
	}
}
