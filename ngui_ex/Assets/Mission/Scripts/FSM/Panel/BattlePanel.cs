using UnityEngine;

public class BattlePanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		switch (btnName) {
		case "btn_back":
		case "btn_forward_to_chapter_map":
			GuiMgr.GetInst ().PopPnl (PANEL_TYPE.ChapterMap);
			break;
		case "btn_forward_to_lobby":
			GameStateMgr.GetInst ().Backward ();
			GuiMgr.GetInst ().PopPnl (PANEL_TYPE.Lobby);
			break;
		case "btn_forward_to_current_stage_entrance":
			GuiMgr.GetInst ().PopPnl (PANEL_TYPE.ChapterMap, PANEL_TYPE.StageEntrance);
			break;
		case "btn_forward_to_next_stage_entrance":
			GuiMgr.GetInst ().PopPnl (PANEL_TYPE.ChapterMap, PANEL_TYPE.StageEntrance);
			break;
		}
	}
}
