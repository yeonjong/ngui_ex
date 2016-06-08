using UnityEngine;

public class BattlePanel : MonoBehaviour {

    public void ClickForwardToLobbyBtn() {
        GameStateMgr.GetInst().ForwardState(GAME_STATE.LobbyState);
    }

	public void ClickForwardToChapterMapBtn() {
		GameStateMgr.GetInst ().BackwardState ();
	}

	public void ClickForwardToCurrentStageEntranceBtn() {
		//

		GameStateMgr.GetInst().ForwardState(GAME_STATE.ChapterMapState);
		GuiMgr.GetInst ().PushPanel (PANEL_TYPE.StageEntrance);
	}

	public void ClickForwardToNextStageEntranceBtn() {
		GameStateMgr.GetInst().ForwardState(GAME_STATE.ChapterMapState);
		GuiMgr.GetInst ().PushPanel (PANEL_TYPE.StageEntrance);
	}
}
