using UnityEngine;
using System;
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

		case "btn_map_0":
		case "btn_map_1":
		case "btn_map_2":
			int wrapIndex = Int32.Parse (btnName.Substring (btnName.Length - 1));
			Debug.Log (wrapIndex);

			//TODO: calculate real index and save selected map;
			Debug.Log("todo");

			GuiMgr.GetInst ().PopPnl ();
			break;
		}
	}

}
