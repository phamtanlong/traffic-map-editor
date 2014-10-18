using UnityEngine;
using System;
using System.Collections;

public class DialogHandler : MonoBehaviour {

	public static DialogHandler Instance;

	public DialogMessageHandler dialogMessage;
	public GameObject dialogNewMap;
	public GameObject dialogEditMap;
	public GameObject dialogBackground;


	void Awake () {
		Instance = this;
	}

	public void ShowDialogMessage (string title, string message, Action<object> callback = null, object data = null) {
		dialogMessage.Show (title, message, callback, data);
	}

	public void OpenDialogNewMap () {
		dialogNewMap.SetActive (true);
	}

	public void OpenDialogEditMap () {
		dialogEditMap.SetActive (true);
	}
}
