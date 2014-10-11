using UnityEngine;
using System.Collections;
using System.IO;

public class MenuHandler : MonoBehaviour {

	public GameObject objMenuPanel;
	public UIPopupList popFile;

	public bool isShowMenu;
	public UILabel message;
	public GameObject objNewMap;

	void Start () {
		objMenuPanel.SetActive (isShowMenu);
	}

	public void OnShowHideMenu () {
		isShowMenu = ! isShowMenu;
		objMenuPanel.SetActive (isShowMenu);
	}

	public void OnCreateNewMap () {
		objNewMap.gameObject.SetActive (false);
		message.text = "Create";
	}

	public void OnCancelNewMap () {
		objNewMap.gameObject.SetActive (false);
		message.text = "Cancel";
	}

	public void FileValueChange () {
		if (popFile.value.Contains ("--") == false) { //seperate mark
			Debug.Log (popFile.value);

			switch (popFile.value) {
			case "Open":
				UniFileBrowser.use.OpenFileWindow (OpenFileCallback);
				break;

			case "Save":
				UniFileBrowser.use.SaveFileWindow (SaveFileCallback);
				break;

			case "Exit":
				Application.Quit ();
				break;

			case "New":
				NewMap ();
				break;
			}
		}
	}

	private void OpenFileCallback (string pathToFile) {
		message.text = pathToFile;
		string s = File.ReadAllText (pathToFile);
		message.text = s;
	}

	private void SaveFileCallback (string pathToFile) {
		message.text = pathToFile;
		File.WriteAllText (pathToFile, "I LOVE YOU, this file!");
		message.text = "Write completed!";
	}

	private void NewMap () {
		message.text = "New File";

		objNewMap.SetActive (true);
	}
}
