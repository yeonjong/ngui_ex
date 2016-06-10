using UnityEngine;
using System.Collections;

public class AreanaEndingChoreographyPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
		case "btn_ok":
			if (GuiMgr.GetInst ().CheckContainsTargetPanel (PANEL_TYPE.AreanaRecord))
				GuiMgr.GetInst ().PopPnl (PANEL_TYPE.AreanaRecord, PANEL_TYPE.AreanaRecordReviewCheck);
			else if (GuiMgr.GetInst ().CheckContainsTargetPanel (PANEL_TYPE.AreanaEntrance))
				GuiMgr.GetInst ().PopPnl (PANEL_TYPE.AreanaEntrance);
			else if (GuiMgr.GetInst ().CheckContainsTargetPanel (PANEL_TYPE.ShamBattleEntrance))
				GuiMgr.GetInst ().PopPnl (PANEL_TYPE.ShamBattleEntrance);
			else
				Debug.LogError ("this case not implemented");
			break;
		case "btn_review":
			//TODO: you must give review information.
			Debug.Log("todo logic");
			GuiMgr.GetInst ().PopPnl ();
			break;
		}
	}

}
