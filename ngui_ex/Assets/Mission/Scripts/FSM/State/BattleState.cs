public class BattleState : GameStateBase {

    public override void OnEnter(GAME_STATE prev_gs) {
        base.OnEnter(prev_gs);

		GuiMgr.GetInst ().PushPanel (PANEL_TYPE.Battle);
        //GuiMgr.GetInst().ShowBattleUI();
    }

    public override void OnLeave(GAME_STATE next_gs) {
        base.OnLeave(next_gs);

		if (next_gs.Equals (GAME_STATE.LobbyState)) {
			GuiMgr.GetInst ().JumpBackPanel (PANEL_TYPE.Lobby);
		} else if (next_gs.Equals(GAME_STATE.ChapterMapState)) {
			GuiMgr.GetInst ().JumpBackPanel (PANEL_TYPE.ChapterMap);
		}
        //GuiMgr.GetInst().HideBattleUI();
    }

}
