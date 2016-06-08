using UnityEngine;
using System.Collections;

public class AreanaEntrancePanel : MonoBehaviour {

	public void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_other_user_party_info":
			GuiMgr.GetInst ().PushPanel (PANEL_TYPE.OtherUserPartyInfo);
			break;
		case "btn_attack_party_edit":
			GameStateMgr.GetInst ().ForwardState (GAME_STATE.AttackPartyEditState);
			break;
		case "btn_defense_party_edit":
			GameStateMgr.GetInst ().ForwardState (GAME_STATE.DefensePartyEditState);
			break;
		case "btn_reset":
			Debug.Log ("don't implement");
			break;
		case "btn_ranking":
			GuiMgr.GetInst ().PushPanel (PANEL_TYPE.AreanaRanking);
			break;
		case "btn_record":
			GuiMgr.GetInst ().PushPanel (PANEL_TYPE.AreanaRecord);
			break;
		case "btn_market":
			Debug.Log ("don't implement");
			break;
		case "btn_reward":
			GuiMgr.GetInst ().PushPanel (PANEL_TYPE.AreanaReward);
			break;
		case "btn_help":
			GuiMgr.GetInst ().PushPanel (PANEL_TYPE.AreanaHelp);
			break;
		case "btn_cumulative":
			GuiMgr.GetInst ().PushPanel (PANEL_TYPE.AreanaCumulative);
			break;
		}
	}


}
