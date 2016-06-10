using UnityEngine;
using System.Collections;

public class AreanaRecordReviewCheckPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
		case "spr_modal":
			GuiMgr.GetInst ().PopPnl ();
			break;
		case "btn_review":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaBattle);
			break;
		}
	}

}
