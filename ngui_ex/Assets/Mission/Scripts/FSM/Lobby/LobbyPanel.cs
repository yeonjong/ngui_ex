using UnityEngine;

public class LobbyPanel : PanelBase {

    public void ClickBackwardToIntroBtn() {
		GameStateMgr.GetInst ().BackwardState ();
    }

    public void ClickForwardToChapterMapBtn() {
		GameStateMgr.GetInst().ForwardState(GAME_STATE.ChapterMapState);
    }

}
