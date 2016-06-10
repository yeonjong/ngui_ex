using UnityEngine;
using System.Collections;

public class AreanaRecordPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
			GuiMgr.GetInst ().PopPnl ();
			break;

		case "btn_social_share":
			// TODO: social
			Debug.Log("todo");
			break;
		case "btn_record_review_check":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaRecordReviewCheck, false);
			break;
		}
	}

}
