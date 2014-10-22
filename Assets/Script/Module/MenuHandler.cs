using UnityEngine;
using System.Collections;
using System.IO;
using Pathfinding.Serialization.JsonFx;

public class MenuHandler : MonoBehaviour {

	public GameObject objMenuPanel;
	public UIPopupList popFile;
	public UIPopupList popEdit;
	public UIPopupList popZoom;

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

//	public void ZoomValueChange () {
//		switch (popZoom.value) {
//		case "200%":
//			//DrawPanelHandler.Instance.SetZoom (2.0f);
//			Main.Instance.ObjCamera.orthographicSize = 0.5f;
//			break;
//
//		case "100%":
//			//DrawPanelHandler.Instance.SetZoom (1.0f);
//			Main.Instance.ObjCamera.orthographicSize = 1.0f;
//			break;
//			
//		case "75%":
//			//DrawPanelHandler.Instance.SetZoom (0.75f);
//			Main.Instance.ObjCamera.orthographicSize = 0.5f;
//			break;
//			
//		case "50%":
//			DrawPanelHandler.Instance.SetZoom (0.50f);
//			break;
//			
//		case "25%":
//			DrawPanelHandler.Instance.SetZoom (0.25f);
//			break;
//		}
//	}

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
