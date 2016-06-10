using UnityEngine;
using System.Collections;

public class AreanaIntroChoreographyPanel : MonoBehaviour {

	public void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_imsi_back":
			if (GuiMgr.GetInst ().CheckContainsTargetPanel (PANEL_TYPE.AreanaEntrance))
				GuiMgr.GetInst ().PopPnl (PANEL_TYPE.AreanaEntrance);
			else if (GuiMgr.GetInst ().CheckContainsTargetPanel (PANEL_TYPE.ShamBattleEntrance))
				GuiMgr.GetInst ().PopPnl (PANEL_TYPE.ShamBattleEntrance);
			else
				Debug.LogError ("this case not implemented");
			break;
		case "btn_imsi_after2seconds":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaBattle);
			break;
		}
	}

}
