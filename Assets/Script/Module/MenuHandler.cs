using UnityEngine;
using System;
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

	private static bool _fileInited = false;
	public void FileValueChange () {
		if (_fileInited == false) {
			_fileInited = true;
			return;
		}

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
			DialogHandler.Instance.ShowDialogMessageYesNo ("Warning", 
			                                               "Do you want to SAVE before quiting?", 
			                                               this.CallbackQuitSave, 
			                                               this.CallbackQuitNotSave);
			break;

		case "New":
			DialogHandler.Instance.OpenDialogNewMap ();
			break;
		}
	}

	public void CallbackQuitSave () {Debug.Log ("quit save");
		DialogHandler.Instance.dialogBackground.SetActive (true);
#if UNITY_WEBPLAYER
		SaveFile2 ();
#else
		SaveTemp ();
		UniFileBrowser.use.SaveFileWindow (SaveFileCallbackAndQuit);
#endif

	}

	public void CallbackQuitNotSave () {Debug.Log ("quit not save");
		Application.Quit ();
	}

	private static bool _editInited = false;
	public void EditValueChange () {
		if (_editInited == false) {
			_editInited = true;
			return;
		}

		switch (popEdit.value) {
		case "Edit":
			DialogHandler.Instance.OpenDialogEditMap ();
			break;
		}
	}

	public void OnHelp () {
		DialogHandler.Instance.OpenDialogHelp ();
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

		bool isOK = Main.Instance.Import (s);

		if (isOK == false) {
			Main.Instance.log.text = "Invalid file! Please use and JSON file made by Map Editor";
			Debug.LogError ("Wrong Type Of File");
		}
	}

	private void SaveFileCallback (string pathToFile) {
		Main.Instance.log.text = pathToFile;

		string s = Main.Instance.Export ();
		try {
			File.WriteAllText (pathToFile, s);
			Main.Instance.log.text = "Write completed!";
		} catch (Exception e) {
			Main.Instance.log.text = "Can not save file!";
			DialogHandler.Instance.ShowDialogMessageOK ("Error", "Can not save file\nPlease:\n- Save with new file's name.\n- Save to another place.", null); 
		}
	}
	
	private void SaveFileCallbackAndQuit (string pathToFile) {
		Main.Instance.log.text = pathToFile;
		
		string s = Main.Instance.Export ();
		File.WriteAllText (pathToFile, s);
		
		Main.Instance.log.text = "Write completed!";

		Application.Quit ();
	}

	private void SaveTemp () {
#if ! UNITY_EDITOR
		string s = Main.Instance.Export ();
		string log = Application.dataPath 
				+ "/save_" 
				+ Global.currentMap.name 
				+ "_" + System.DateTime.Now.Day + "th"
				+ "-" + System.DateTime.Now.Month
				+ "_" +System.DateTime.Now.Hour 
				+ "h" + System.DateTime.Now.Minute + "m.json";

		File.WriteAllText (log, s);
		
		Main.Instance.log.text = log;
#endif
	}

#endif
}
