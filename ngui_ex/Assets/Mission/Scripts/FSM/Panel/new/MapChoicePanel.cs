using UnityEngine;
using System.Collections;

public class MapChoicePanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
		case "btn_select":
			//TODO: select logic
			Debug.Log ("select logic");

			GuiMgr.GetInst ().PopPnl ();
			break;
		case "btn_cancel":
			GuiMgr.GetInst ().PopPnl ();
			break;
		}
	}

}
