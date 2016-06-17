using UnityEngine;
using System.Collections;

public class AreanaHelpPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
		case "btn_imsi_back":
			GuiMgr.GetInst ().PopPnl ();
			break;
		}
	}

}
