using UnityEngine;
using System;
using System.Collections;

public class EventTransfer : MonoBehaviour {

	public Action<float> onScroll = null;
	public Action<bool> onPress = null;
	public Action<Vector2> onDrag = null;

	public KeyCode keyToTransferDrag = KeyCode.Space;
	public KeyCode keyToTransferPress = KeyCode.Space;

	void Start () {}
	void Update () {}

	void OnScroll (float delta) {
		if (onScroll != null) {
			onScroll (delta);
		}
	}

	void OnPress (bool ispress) {
		if (Input.GetKey (keyToTransferPress) == true) {
			if (onPress != null) {
				onPress (ispress);
			}
		}
	}

	void OnDrag (Vector2 v) {
		if (Input.GetKey (keyToTransferDrag) == true) {
			if (onDrag != null) {
				onDrag (v);
			}
		}
	}
}
