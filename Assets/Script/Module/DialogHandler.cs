using UnityEngine;
using System.Collections;

public class DialogHandler : MonoBehaviour {

	public static DialogHandler Instance;

	public GameObject dialogNewMap;
	public GameObject dialogEditMap;

	void Awake () {
		Instance = this;
	}

	public void OpenDialogNewMap () {
		dialogNewMap.SetActive (true);
	}

	public void OpenDialogEditMap () {
		dialogEditMap.SetActive (true);
	}
}
