using UnityEngine;

public class ChapterMapPanel : MonoBehaviour {
	
	public void ClickForwardToStageEntranceBtn() {
		GuiMgr.GetInst ().PushPanel (PANEL_TYPE.StageEntrance);
		//GameStateMgr.GetInst ().ForwardState (GAME_STATE.StageEntranceState);
	}

}