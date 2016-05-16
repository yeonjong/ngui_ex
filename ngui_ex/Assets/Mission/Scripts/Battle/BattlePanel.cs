using UnityEngine;

public class BattlePanel : MonoBehaviour {

    public void ClickForwardToLobbyBtn() {
        GameStateMgr.getInst().ForwardState(GAME_STATE.LobbyState);
    }
}
