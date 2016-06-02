using UnityEngine;

public class StageEntrancePanel : MonoBehaviour {

	public void ClickForwardToChapterMapBtn() {
		GameStateMgr.GetInst ().BackwardState ();
	}

	public void ClickForwardToPartyEditBtn() {
		GameStateMgr.GetInst ().ForwardState (GAME_STATE.PartyEditState);
	}

}