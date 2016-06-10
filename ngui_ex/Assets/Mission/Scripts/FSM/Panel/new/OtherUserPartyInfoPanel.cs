using UnityEngine;
using System.Collections;

public class OtherUserPartyInfoPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		switch (btnName) {
		case "btn_back":
		case "spr_modal":
			GuiMgr.GetInst ().PopPnl ();
			break;
		case "btn_ok":
			//TODO: save info
			Debug.Log ("todo");

			GuiMgr.GetInst ().PopPnl ();
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
