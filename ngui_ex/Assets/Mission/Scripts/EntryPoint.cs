using UnityEngine;

class EntryPoint : MonoBehaviour {

    void Start() {
        // TODO: Intro State으로 진입하자.
        GameStateMgr.getInst().ForwardState(GAME_STATE.IntroState);
    }

}
