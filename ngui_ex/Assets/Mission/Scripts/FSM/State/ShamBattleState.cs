public class ShamBattleState : GameStateBase {

	public override void OnEnter(GAME_STATE prev_gs) {
		base.OnEnter(prev_gs);

		GuiMgr.GetInst ().PushPanel (PANEL_TYPE.ShamBattle);
	}

	public override void OnLeave(GAME_STATE next_gs) {
		base.OnLeave(next_gs);

		GuiMgr.GetInst ().JumpBackPanel (PANEL_TYPE.Lobby);
	}

}