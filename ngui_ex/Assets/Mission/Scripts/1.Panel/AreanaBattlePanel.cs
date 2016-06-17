using UnityEngine;
using System.Collections;

public class AreanaBattlePanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
		case "btn_imsi_back":
			if (GuiMgr.GetInst ().CheckContainsTargetPanel (PANEL_TYPE.AreanaRecordReviewCheck)) {
				GuiMgr.GetInst ().PopPnl (PANEL_TYPE.AreanaRecord, PANEL_TYPE.AreanaRecordReviewCheck);
			}
			else if (GuiMgr.GetInst ().CheckContainsTargetPanel (PANEL_TYPE.AreanaEntrance))
				GuiMgr.GetInst ().PopPnl (PANEL_TYPE.AreanaEntrance);
			else if (GuiMgr.GetInst ().CheckContainsTargetPanel (PANEL_TYPE.ShamBattleEntrance))
				GuiMgr.GetInst ().PopPnl (PANEL_TYPE.ShamBattleEntrance);
			else
				Debug.LogError ("this case not implemented");
			break;
		case "btn_imsi_win":
			GlobalApp.Inst.isWin = true;
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaEndingChoreography);
			break;
		case "btn_imsi_lose":
			GlobalApp.Inst.isWin = false;
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaEndingChoreography);
			break;
		}
	}

}
