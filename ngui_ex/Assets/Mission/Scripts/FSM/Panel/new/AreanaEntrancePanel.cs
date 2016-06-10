using UnityEngine;
using System.Collections;

public class AreanaEntrancePanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		switch (btnName) {
		case "btn_back":
			GameStateMgr.GetInst ().Backward ();
			GuiMgr.GetInst ().PopPnl ();
			break;
		case "btn_other_user_party_info":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.OtherUserPartyInfo, false);
			break;
		case "btn_attack_party_edit":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AttackPartyEdit);
			break;
		case "btn_defense_party_edit":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.DefensePartyEdit);
			break;
		case "btn_reset":
			// TODO: reset
			Debug.Log ("todo");
			break;
		case "btn_ranking":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaRanking);
			break;
		case "btn_record":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaRecord);
			break;
		case "btn_market":
			// TODO: market
			Debug.Log ("todo");
			break;
		case "btn_reward":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaReward);
			break;
		case "btn_help":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaHelp);
			break;
		case "btn_cumulative":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaCumulative);
			break;
		}
	}


}
