using UnityEngine;
using System.Collections;

public class AreanaRewardPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
			GuiMgr.GetInst ().PopPnl ();
			break;
		case "btn_item_info":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.ItemInfo, false);
			break;
		}
	}

}
