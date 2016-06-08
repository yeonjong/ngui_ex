using UnityEngine;

public class FormationEditPanel : MonoBehaviour {
	

	public void ClickSelectFormationBtn(Object formationObject) {
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
		GameData.Inst.SetFormation(formationNumber);
		GuiMgr.GetInst ().OnSelectFormation ();

		ClickForwardToPartyEditBtn ();
	}

	public void ClickForwardToPartyEditBtn() {
		GuiMgr.GetInst ().PopPanel ();
	}

}