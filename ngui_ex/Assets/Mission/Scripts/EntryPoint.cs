using UnityEngine;

class EntryPoint : MonoBehaviour {

	public GAME_STATE m_startState = GAME_STATE.IntroState;

    void Start() {
		GameStateMgr.GetInst ().ForwardState (m_startState);//GAME_STATE.IntroState);
    }

}
