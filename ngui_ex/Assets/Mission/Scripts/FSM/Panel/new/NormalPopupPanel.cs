using UnityEngine;
using System.Collections;

public class NormalPopupPanel : MonoBehaviour {

	public void OnClickXXXBtn() {
		GameStateMgr.GetInst ().ForwardState (GAME_STATE.IntroState);
		GameStateMgr.GetInst ().BackwardState ();
		GuiMgr.GetInst ().PushPanel (PANEL_TYPE.Intro);
		GuiMgr.GetInst ().PopPanel ();
	}

}
