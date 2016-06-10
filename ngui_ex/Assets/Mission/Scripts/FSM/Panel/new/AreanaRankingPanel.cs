using UnityEngine;
using System.Collections;

public class AreanaRankingPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
			GuiMgr.GetInst ().PopPnl ();
			break;

		case "btn_other_user_party_info":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.OtherUserPartyInfo, false);
			break;
		}
	}

}
