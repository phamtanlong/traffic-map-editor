using UnityEngine;
using System.Collections;
using System;

public class DragableObject1 : MonoBehaviour {

	public static Vector2 step = new Vector2 (32,32) ;//Vector2.one;
	public KeyCode keyToDisable=KeyCode.Space;

	private bool isDeleted = false;
	private Texture originColorDelete;
	private Texture originColorClick;
	private bool isDragFirst = false;
	private Vector2 lastTouchPosition = Vector2.zero;
	private Vector3 lastPosition = Vector3.zero;

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

	public void OnDrag (Vector2 vvv)
	{
		if (keyToDisable != KeyCode.None && Input.GetKey (keyToDisable)==true) {
			return;
		}

		//Debug.Log ("on drag: " + v);
		if (isDragFirst) {
			isDragFirst = false;
			Vector3 curPos = transform.position; //world
			Vector2 touchPos = UICamera.current.camera.ScreenToWorldPoint (UICamera.lastTouchPosition); //world

			lastTouchPosition = touchPos; //save last touch
			lastPosition = curPos;
		}

		Vector2 vec = UICamera.lastTouchPosition;
		Vector3 newTouchPosition = UICamera.current.camera.ScreenToWorldPoint(new Vector3(vec.x,vec.y,0));

		Vector3 deltaTouch = new Vector3 (newTouchPosition.x - lastTouchPosition.x, newTouchPosition.y - lastTouchPosition.y, 0);
		deltaTouch.x *= Main.Instance.ObjCamera.orthographicSize;
		deltaTouch.y *= Main.Instance.ObjCamera.orthographicSize;
		Vector3 newPos = new Vector3 (lastPosition.x + deltaTouch.x, lastPosition.y + deltaTouch.y, lastPosition.z);

		transform.position = newPos;
	}
}
