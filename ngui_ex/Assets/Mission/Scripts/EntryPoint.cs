using UnityEngine;

class EntryPoint : MonoBehaviour {

	public GAME_STATE m_startState = GAME_STATE.IntroState;

    void Start() {
		//GuiMgr.GetInst ().PushPnl (PANEL_TYPE.Intro);
		GameStateMgr.GetInst ().ForwardState (m_startState);//GAME_STATE.IntroState);
    }

}
