using UnityEngine;

public class LobbyPanel : MonoBehaviour {

    public void ClickBackwardToIntroBtn() {
		GameStateMgr.GetInst ().BackwardState ();
    }

    public void ClickForwardToChapterMapBtn() {
		GameStateMgr.GetInst().ForwardState(GAME_STATE.ChapterMapState);
    }

	public void OnClickAreanaBtn() {
		GameStateMgr.GetInst ().ForwardState (GAME_STATE.AreanaEntranceState);
	}

	public void OnClickStrongestAreanaBtn() {
		GameStateMgr.GetInst ().ForwardState (GAME_STATE.StrongestAreanaEntranceState);
	}

	public void OnClickShamBattleBtn() {
		GameStateMgr.GetInst ().ForwardState (GAME_STATE.ShamBattleState);
	}

}
