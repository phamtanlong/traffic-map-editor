using UnityEngine;
using System.Collections;
using System;

public class DragableCamera : MonoBehaviour {

	public Camera targetCamera;
	public Vector2 step = Vector2.one;
	public KeyCode keyToDrag = KeyCode.None;

	private bool isDragFirst = false;
	private Vector2 lastTouchPosition = Vector2.zero;
	private Vector3 lastPosition = Vector3.zero;
	
	void Start () {}
	void Update () {}
	
	public void OnPress (bool isPress)
	{
		if (keyToDrag != KeyCode.None && Input.GetKey (keyToDrag)==false) {
			return;
		}
		
		if (isPress == true) {
			isDragFirst = true;
		} else {
			isDragFirst = false;
		}

		//Move in Step
		if (isPress == false) {
			Vector3 pos = targetCamera.transform.localPosition;
			
			int dx = Mathf.RoundToInt (pos.x / step.x);
			int dy = Mathf.RoundToInt (pos.y / step.y);
			
			pos.x = step.x * dx;
			pos.y = step.y * dy;

			targetCamera.transform.localPosition = pos;
		}
	}
	
	public void OnDrag (Vector2 v)
	{
		if (keyToDrag != KeyCode.None && Input.GetKey (keyToDrag)==false) {
			return;
		}
		
		//Debug.Log ("on drag: " + v);
		if (isDragFirst) {
			isDragFirst = false;
			Vector3 curPos = targetCamera.transform.position; //world
			Vector2 touchPos = UICamera.current.camera.ScreenToWorldPoint (UICamera.lastTouchPosition); //world

			lastTouchPosition = touchPos; //save last touch
			lastPosition = curPos;
		}
		
		Vector2 vec = UICamera.lastTouchPosition;
		Vector3 newTouchPosition = UICamera.current.camera.ScreenToWorldPoint(new Vector3(vec.x,vec.y,0));
		
		Vector3 deltaTouch = new Vector3 (lastTouchPosition.x - newTouchPosition.x, lastTouchPosition.y - newTouchPosition.y, 0);
		deltaTouch.x *= Main.Instance.ObjCamera.orthographicSize;
		deltaTouch.y *= Main.Instance.ObjCamera.orthographicSize;
		Vector3 newPos = new Vector3 (lastPosition.x + deltaTouch.x, lastPosition.y + deltaTouch.y, lastPosition.z);
		
		targetCamera.transform.position = newPos;
	}
}
