using UnityEngine;

public class ChapterMapPanel : PanelBase {

	/*
	public void ClickForwardToStageEntranceBtn() {
		GuiMgr.GetInst ().PushPnl (PANEL_TYPE.StageEntrance, false);
	}
	*/	
	public override void OnClickXXXBtn(string btnName) {
		switch (btnName) {
		case "btn_back":
			GameStateMgr.GetInst ().Backward ();
			GuiMgr.GetInst ().PopPnl ();
			break;
		case "btn_dungeon":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.StageEntrance, false);
			break;
		}
	}

}