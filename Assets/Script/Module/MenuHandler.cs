using UnityEngine;
using System.Collections;
using System.IO;
using Pathfinding.Serialization.JsonFx;

public class MenuHandler : MonoBehaviour {

	public GameObject objMenuPanel;
	public UIPopupList popFile;
	public UIPopupList popEdit;

	public bool isShowMenu;

	void Start () {
		objMenuPanel.SetActive (isShowMenu);
		UniFileBrowser.use.SendWindowCloseMessage(CloseWindowCallback);
	}
	
	public void OnShowHideMenu () {
		isShowMenu = ! isShowMenu;
		objMenuPanel.SetActive (isShowMenu);
	}
	
	public void FileValueChange () {
		switch (popFile.value) {
		case "Open":
			DialogHandler.Instance.dialogBackground.SetActive (true);
			UniFileBrowser.use.OpenFileWindow (OpenFileCallback);
			break;

		case "Save":
			DialogHandler.Instance.dialogBackground.SetActive (true);
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

	private void CloseWindowCallback () {
		DialogHandler.Instance.dialogBackground.SetActive (false);
	}

	private void OpenFileCallback (string pathToFile) {
		Main.Instance.log.text = pathToFile;
		string s = File.ReadAllText (pathToFile);
		Main.Instance.log.text = s;

		Main.Instance.Import (s);
	}

	private void SaveFileCallback (string pathToFile) {
		Main.Instance.log.text = pathToFile;

		string s = Main.Instance.Export ();

		File.WriteAllText (pathToFile, s);
		Main.Instance.log.text = "Write completed!";
	}
}
