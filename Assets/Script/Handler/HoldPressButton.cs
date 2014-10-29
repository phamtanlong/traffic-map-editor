using UnityEngine;
using System.Collections.Generic;

public class HoldPressButton : MonoBehaviour {
	
	public EventDelegate onClick = null;
	public static float FRAME_NUM = 10;
	float frameNo = FRAME_NUM;

	bool isPress = false;
	int counter = 0;

	void Start () {
		counter = 0;
		frameNo = FRAME_NUM;
	}

	void Update () {
		counter++;

		if (counter >= frameNo) {
			counter = 0;

			if (isPress == true) {
				frameNo -= 0.5f;
				if (onClick != null) {
					onClick.Execute ();
				}
			} else {
				frameNo = FRAME_NUM;
			}
		}
	}

	void OnPress (bool ispress) {
		this.isPress = ispress;
	}
}
