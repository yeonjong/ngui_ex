using UnityEngine;
using System.Collections;

public class AttackPartyEditPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
		case "btn_x":
			GuiMgr.GetInst ().PopPnl ();
			break;
		case "btn_start":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaIntroChoreography);
			break;
		case "btn_formation_edit":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.FormationEdit, false);
			break;
		case "btn_formation_info":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.FormationInfo, false);
			break;
		case "btn_character_info":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.CharacterInfo, false);
			break;
		}
	}

}
