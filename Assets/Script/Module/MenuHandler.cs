using UnityEngine;
using System.Collections;
using System.IO;

public class MenuHandler : MonoBehaviour {

	public GameObject objMenuPanel;
	public UIPopupList popFile;
	public UIPopupList popEdit;

	public bool isShowMenu;

	void Start () {
		objMenuPanel.SetActive (isShowMenu);
	}

	public void OnShowHideMenu () {
		isShowMenu = ! isShowMenu;
		objMenuPanel.SetActive (isShowMenu);
	}

	public void FileValueChange () {
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
			DialogHandler.Instance.OpenDialogNewMap ();
			break;
		}
	}

	public void EditValueChange () {
		switch (popEdit.value) {
		case "Edit":
			DialogHandler.Instance.OpenDialogEditMap ();
			break;
		}
	}

	private void OpenFileCallback (string pathToFile) {
		Main.Instance.log.text = pathToFile;
		string s = File.ReadAllText (pathToFile);
		Main.Instance.log.text = s;
	}

	private void SaveFileCallback (string pathToFile) {
		Main.Instance.log.text = pathToFile;
		File.WriteAllText (pathToFile, "I LOVE YOU, this file!");
		Main.Instance.log.text = "Write completed!";
	}
}
