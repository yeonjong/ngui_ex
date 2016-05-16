using UnityEngine;

public class BattlePanel : MonoBehaviour {

    public void ClickForwardToLobbyBtn() {
        GameStateMgr.GetInst().ForwardState(GAME_STATE.LobbyState);
    }
}
