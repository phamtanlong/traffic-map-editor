using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public enum GridViewDirection {
//	Vertical,
//	Horizontal
//}

public class VerticalGridView : MonoBehaviour {

//	public GridViewDirection direction;
	public int column=0;
	//public int row=0;
	public int tileWidth;
	public int tileHeight;
	public Vector2 padding;

	private List<List<GameObject>> listTiles = new List<List<GameObject>> ();

	void Start () {}
	void Update () {}

	public void AddChild (GameObject widget) 
	{
		int row = listTiles.Count;
		if (row == 0) { //brand new
			listTiles.Add (new List<GameObject> ());
			row = 1;
		}
		int col = listTiles[row-1].Count;
		if (col == column) { //full row
			listTiles.Add (new List<GameObject> ());
			row++;
			col = 0;
		}

		Vector3 v = Vector3.zero;
		v.x = tileWidth * col + col * padding.x;
		v.y = - tileHeight * (row-1) - (row-1) * padding.y;

		widget.transform.parent = this.transform;
		widget.transform.localPosition = v;

		listTiles[row-1].Add (widget);
	}
}
