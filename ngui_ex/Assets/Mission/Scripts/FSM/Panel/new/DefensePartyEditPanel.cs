using UnityEngine;
using System.Collections;

public class DefensePartyEditPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
		case "btn_x":
			GuiMgr.GetInst ().PopPnl ();
			break;
		case "btn_formation_edit":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.FormationEdit, false);
			break;
		case "btn_save":
			// TODO: save logic
			Debug.Log ("todo");
			GuiMgr.GetInst ().PopPnl ();
			break;
		}
	}

}
