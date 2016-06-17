using UnityEngine;

public class LobbyPanel : MonoBehaviour {

    public void ClickForwardToChapterMapBtn() {
		GameStateMgr.GetInst ().ForwardState (GAME_STATE.OtherState);
		GuiMgr.GetInst ().PushPnl (PANEL_TYPE.ChapterMap);
    }

	public void OnClickAreanaBtn() {
		GameStateMgr.GetInst ().ForwardState (GAME_STATE.OtherState);
		GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaEntrance);
	}

	public void OnClickStrongestAreanaBtn() {
		GameStateMgr.GetInst ().ForwardState (GAME_STATE.OtherState);
		GuiMgr.GetInst ().PushPnl (PANEL_TYPE.StrongestAreanaEntrance);
	}

	public void OnClickShamBattleBtn() {
		GameStateMgr.GetInst ().ForwardState (GAME_STATE.OtherState);
		GuiMgr.GetInst ().PushPnl (PANEL_TYPE.ShamBattleEntrance);
	}

}
