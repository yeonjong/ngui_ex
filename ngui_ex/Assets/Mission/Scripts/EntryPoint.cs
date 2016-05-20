using UnityEngine;

class EntryPoint : MonoBehaviour {

    void Start() {
        GameStateMgr.GetInst().ForwardState(GAME_STATE.IntroState);
    }

}
