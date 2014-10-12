using UnityEngine;
using System.Collections;
using System;

public class DragableObject : MonoBehaviour {

	public Vector2 step;
	public KeyCode keyToDrag=KeyCode.None;

	public event Action<GameObject> onDelete;
	public event Action<GameObject> onClick;
	private bool isDeleted = false;
	private Texture originColorDelete;
	private Texture originColorClick;
	private Vector2 offset = Vector2.zero;
	private bool isDragFirst = false;

	void Start ()
	{
	}

	void Update () 
	{
	}

	public void OnPress (bool isPress)
	{
		if (keyToDrag != KeyCode.None && Input.GetKey (keyToDrag)==false) {
			return;
		}

		if (isPress == true) {
			isDragFirst = true;
		} else {
			isDragFirst = false;
			offset = Vector2.zero;
		}

		//remove when release
		if (isPress == false && isDeleted == true)
		{
			if (onDelete != null)
			{
				onDelete (this.gameObject);
			}
		}

		//Click
		if (isPress == false)
		{
			if (onClick != null)
			{
				onClick (this.gameObject);
			}
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
		if (keyToDrag != KeyCode.None && Input.GetKey (keyToDrag)==false) {
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

//	void OnTriggerEnter(Collider other) {
//		if (other.gameObject.name == "btnDelete") {
//			if (isDeleted == false)
//			{
//				isDeleted = true;
//				originColorDelete = gameObject.GetComponent<MeshRenderer>().materials[0].mainTexture;
//				//change color here
//			}
//		}
//	}
//
//	void OnTriggerExit(Collider other) {
//		if (other.gameObject.name == "btnDelete") {
//			if (isDeleted == true)
//			{
//				isDeleted = false;
//				gameObject.GetComponent<MeshRenderer>().materials[0].mainTexture = originColorDelete;
//			}
//		}
//	}

}
