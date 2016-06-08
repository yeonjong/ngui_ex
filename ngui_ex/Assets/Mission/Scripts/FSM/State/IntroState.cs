using UnityEngine;

public class IntroState : GameStateBase {

    public override void OnEnter(GAME_STATE prev_gs) {
		base.OnEnter(prev_gs);

		GuiMgr.GetInst ().PushPanel (PANEL_TYPE.Intro);
		//GuiMgr.GetInst ().ShowIntroUI ();
	}

    public override void OnLeave(GAME_STATE next_gs) {
		base.OnLeave(next_gs);

		Debug.Log ("TODO: Active Exit Popup panel");
		// TODO: Active Exit Popup panel.
    }

}