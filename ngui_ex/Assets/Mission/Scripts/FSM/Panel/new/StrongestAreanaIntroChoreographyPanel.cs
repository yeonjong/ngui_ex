﻿using UnityEngine;
using System.Collections;

public class StrongestAreanaIntroChoreographyPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
		case "spr_modal":
			//GuiMgr.GetInst ().PopPnl ();
			break;
		}
	}

}
