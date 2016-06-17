using UnityEngine;
using System;

public class FormationEditPanel : PanelBase {

	public void ClickSelectFormationBtn(UnityEngine.Object formationObject) {
		int formationNumber;
		switch (formationObject.name) {
		case "btn_formation_0":
			formationNumber = 0;
			break;
		case "btn_formation_1":
			formationNumber = 1;
			break;
		case "btn_formation_2":
			formationNumber = 2;
			break;
		case "btn_formation_3":
			formationNumber = 3;
			break;
		case "btn_formation_4":
			formationNumber = 4;
			break;
		case "btn_formation_5":
			formationNumber = 5;
			break;
		case "btn_formation_6":
			formationNumber = 6;
			break;
		case "btn_formation_7":
			formationNumber = 7;
			break;
		default:
			Debug.LogError ("inject formationObject to button on click.");
			return;
		}
		//GameData.Inst.SetFormation(formationNumber);
		Debug.Log("??");
		Debug.Log (formationNumber);

		GuiMgr.GetInst ().OnSelectFormation (formationNumber);
		GuiMgr.GetInst ().PopPnl ();
	}

	public override void OnClickXXXBtn(string btnName) {
		switch (btnName) {
		case "btn_back":
		case "spr_modal":
			GuiMgr.GetInst ().PopPnl ();
			break;
		}
	}

}