using UnityEngine;

public class BattlePanel : MonoBehaviour {

    public void ClickForwardToLobbyBtn() {
        GameStateMgr.GetInst().ForwardState(GAME_STATE.LobbyState);
    }

	public void ClickForwardToChapterMapBtn() {
		GameStateMgr.GetInst().ForwardState(GAME_STATE.ChapterMapState);
	}

	public void ClickForwardToCurrentStageEntranceBtn() {
		GameStateMgr.GetInst().ForwardState(GAME_STATE.StageEntranceState);
	}

	public void ClickForwardToNextStageEntranceBtn() {
		GameStateMgr.GetInst().ForwardState(GAME_STATE.StageEntranceState);
	}
}
