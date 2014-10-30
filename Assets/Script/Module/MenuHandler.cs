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
#if !UNITY_WEBPLAYER
		UniFileBrowser.use.SendWindowCloseMessage(CloseWindowCallback);
#endif
	}
	
	public void OnShowHideMenu () {
		isShowMenu = ! isShowMenu;
		objMenuPanel.SetActive (isShowMenu);
	}
	
	public void FileValueChange () {
		switch (popFile.value) {
		case "Open":
			DialogHandler.Instance.dialogBackground.SetActive (true);
#if UNITY_WEBPLAYER
			Application.ExternalCall( "ImportJSON", "The game says hello!" );
#else
			UniFileBrowser.use.OpenFileWindow (OpenFileCallback);
#endif
			break;

		case "Save":
			DialogHandler.Instance.dialogBackground.SetActive (true);
#if UNITY_WEBPLAYER
			SaveFile2 ();
#else
			UniFileBrowser.use.SaveFileWindow (SaveFileCallback);
			SaveTemp ();
#endif
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

#if UNITY_WEBPLAYER

	public void OpenFileCallback2 (string s) {
		DialogHandler.Instance.dialogBackground.SetActive (false);
		Main.Instance.Import (s);
	}

	public void SaveFile2 () {
		DialogHandler.Instance.dialogBackground.SetActive (false);
		string s = Main.Instance.Export ();
		Application.ExternalCall( "ExportJSON", s );
	}

#else

	private void OpenFileCallback (string pathToFile) {
		Main.Instance.log.text = pathToFile;
		string s = File.ReadAllText (pathToFile);
		Main.Instance.log.text = pathToFile;
		Main.Instance.Import (s);
	}

	private void SaveFileCallback (string pathToFile) {
		Main.Instance.log.text = pathToFile;

		string s = Main.Instance.Export ();
		File.WriteAllText (pathToFile, s);

		Main.Instance.log.text = "Write completed!";
	}

	private void SaveTemp () {
#if ! UNITY_EDITOR
		string s = Main.Instance.Export ();
		string log = Application.dataPath + "/save_" + Global.currentMap.name + "_" + System.DateTime.Now.Hour + "h_" + System.DateTime.Now.Minute + "m.json";
		File.WriteAllText (log, s);
		
		Main.Instance.log.text = log;
#endif
	}

#endif
}
