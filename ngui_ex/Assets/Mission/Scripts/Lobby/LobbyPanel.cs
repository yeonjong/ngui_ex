using UnityEngine;

public class LobbyPanel : MonoBehaviour {

    public void ClickForwardToIntroBtn() {
        GameStateMgr.getInst().ForwardState(GAME_STATE.IntroState);
    }

    public void ClickForwardToBattleBtn() {
        GameStateMgr.getInst().ForwardState(GAME_STATE.BattleState);
    }

}
