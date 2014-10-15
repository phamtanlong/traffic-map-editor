using UnityEngine;
using System.Collections;
using System;

public class DragableObject1 : MonoBehaviour {

	public Vector2 step=Vector2.one;
	public KeyCode keyToDisable=KeyCode.Space;

	private bool isDeleted = false;
	private Texture originColorDelete;
	private Texture originColorClick;
	private Vector2 offset = Vector2.zero;
	private bool isDragFirst = false;

	void Start () {}
	void Update () {}

	public void OnPress (bool isPress)
	{
		if (keyToDisable != KeyCode.None && Input.GetKey (keyToDisable)==true) {
			return;
		}

		if (isPress == true) {
			isDragFirst = true;
		} else {
			isDragFirst = false;
			offset = Vector2.zero;
		}

		//Move in Step
		if (isPress == false) {
			Vector3 pos = transform.localPosition;

			int dx = Mathf.RoundToInt (pos.x / step.x);
			int dy = Mathf.RoundToInt (pos.y / step.y);

			pos.x = step.x * dx;
			pos.y = step.y * dy;

			transform.localPosition = pos;
		}
	}

	public void OnDrag (Vector2 v)
	{
		if (keyToDisable != KeyCode.None && Input.GetKey (keyToDisable)==true) {
			return;
		}

		//Debug.Log ("on drag: " + v);
		if (isDragFirst) {
			isDragFirst = false;
			Vector3 curPos = transform.position; //world
			Vector2 touchPos = UICamera.current.camera.ScreenToWorldPoint (UICamera.lastTouchPosition); //world
			offset = new Vector2 (touchPos.x - curPos.x, touchPos.y - curPos.y); //world
		}

		Vector2 vec = UICamera.lastTouchPosition;
		Vector3 newpos= UICamera.current.camera.ScreenToWorldPoint(new Vector3(vec.x,vec.y,0));
		transform.position = new Vector3 (newpos.x-offset.x, newpos.y-offset.y, newpos.z);
	}
}
