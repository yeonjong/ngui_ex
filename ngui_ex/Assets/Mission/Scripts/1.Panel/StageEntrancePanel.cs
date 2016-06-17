using UnityEngine;

public class StageEntrancePanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		switch (btnName) {
		case "btn_back":
		case "spr_modal":
			GuiMgr.GetInst ().PopPnl ();
			break;
		case "btn_entrance":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.PartyEdit);
			break;
		}
	}

}