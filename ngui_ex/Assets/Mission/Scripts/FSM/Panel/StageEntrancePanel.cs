using UnityEngine;

public class StageEntrancePanel : MonoBehaviour {

	public void ClickForwardToChapterMapBtn() {
		GuiMgr.GetInst ().PopPanel ();
		//GameStateMgr.GetInst ().BackwardState ();
	}

	public void ClickForwardToPartyEditBtn() {
		GameStateMgr.GetInst ().ForwardState (GAME_STATE.PartyEditState);
	}

}