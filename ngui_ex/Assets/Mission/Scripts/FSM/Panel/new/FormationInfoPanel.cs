using UnityEngine;
using System.Collections;

public class FormationInfoPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {

		switch (btnName) {
		case "btn_back":
		case "spr_modal":
		case "btn_x":
			GuiMgr.GetInst ().PopPnl ();
			break;
		}
	}

}
