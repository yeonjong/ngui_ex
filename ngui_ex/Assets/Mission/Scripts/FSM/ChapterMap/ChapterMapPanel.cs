using UnityEngine;

public class ChapterMapPanel : MonoBehaviour {
	
	public void ClickForwardToStageEntranceBtn() {
		GameStateMgr.GetInst ().ForwardState (GAME_STATE.StageEntranceState);
	}

}