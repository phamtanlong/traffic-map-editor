using UnityEngine;
using System;
using System.Collections;

public class DialogHandler : MonoBehaviour {

	public static DialogHandler Instance;

	public DialogMessageHandler dialogMessage;
	public GameObject dialogNewMap;
	public GameObject dialogEditMap;
	public GameObject dialogBackground;
	public GameObject dialogHelp;

	void Awake () {
		Instance = this;
	}

	public void ShowDialogMessageOK (string title, string message, Action callback) {
		dialogMessage.ShowDialogOK (title, message, callback);
	}

	public void ShowDialogMessageYesNo (string title, string message, Action callbackYes, Action callbackNo) {
		dialogMessage.ShowDialogYesNo (title, message, callbackYes, callbackNo);
	}

	public void OpenDialogNewMap () {
		dialogNewMap.SetActive (true);
	}

	public void OpenDialogEditMap () {
		dialogEditMap.SetActive (true);
	}

	public void OpenDialogHelp () {
		dialogHelp.SetActive (true);
	}
}
