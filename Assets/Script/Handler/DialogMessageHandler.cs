using UnityEngine;
using System;
using System.Collections;

public class DialogMessageHandler : MonoBehaviour {

	public UILabel lbTitle;
	public UILabel lbMessage;
	private Action<object> callback;
	private object data;

	void Start () {}
	void Update () {}

	public void Show (string title, string message, Action<object> callback = null, object data = null) {

		lbTitle.text = title;
		lbMessage.text = message;
		this.callback = callback;
		this.data = null;

		this.gameObject.SetActive (true);
	}

	public void OnOK () {
		this.gameObject.SetActive (false);

		if (callback != null) {
			callback (data);
		}
	}
}
