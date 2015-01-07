using UnityEngine;
using System;
using System.Collections;

public class DialogMessageHandler : MonoBehaviour {

	public UILabel lbTitle;
	public UILabel lbMessage;

	public UIButton btnOk;
	public UIButton btnYes;
	public UIButton btnNo;

	public Action onOk;
	public Action onYes;
	public Action onNo;

	void Start () {}
	void Update () {}

	public void ShowDialogOK (string title, string message, Action callback) {

		lbTitle.text = title;
		lbMessage.text = message;
		this.onOk = callback;

		btnOk.gameObject.SetActive (true);
		btnYes.gameObject.SetActive (false);
		btnNo.gameObject.SetActive (false);

		this.gameObject.SetActive (true);
	}

	public void ShowDialogYesNo (string title, string message, Action callbackYes, Action callbackNo) {
		
		lbTitle.text = title;
		lbMessage.text = message;
		this.onYes = callbackYes;
		this.onNo = callbackNo;
		
		btnOk.gameObject.SetActive (false);
		btnYes.gameObject.SetActive (true);
		btnNo.gameObject.SetActive (true);

		this.gameObject.SetActive (true);
	}

	public void OnOK () {
		this.gameObject.SetActive (false);
		if (onOk != null) {
			onOk ();
		}
	}

	public void OnYes () {
		this.gameObject.SetActive (false);
		if (onYes != null) {
			onYes ();
		}
	}
	
	public void OnNo () {
		this.gameObject.SetActive (false);
		if (onNo != null) {
			onNo ();
		}
	}

}
