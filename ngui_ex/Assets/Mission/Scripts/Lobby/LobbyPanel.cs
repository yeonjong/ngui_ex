using UnityEngine;

public class LobbyPanel : MonoBehaviour {

    public void ClickForwardToIntroBtn() {
        GameStateMgr.GetInst().ForwardState(GAME_STATE.IntroState);
    }

    public void ClickForwardToBattleBtn() {
        GameStateMgr.GetInst().ForwardState(GAME_STATE.BattleState);
    }

}
