using UnityEngine;

class EntryPoint : MonoBehaviour {

    void Start() {
        // TODO: Intro State으로 진입하자.
        GameStateMgr.GetInst().ForwardState(GAME_STATE.IntroState);
    }

}


