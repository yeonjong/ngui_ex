public class AreanaBattleState : GameStateBase {

	public override void OnEnter(GAME_STATE prev_gs) {
		base.OnEnter(prev_gs);

		GuiMgr.GetInst ().PushPanel (PANEL_TYPE.AreanaIntroChoreography);
	}

	public override void OnLeave(GAME_STATE next_gs) {
		base.OnLeave(next_gs);

		GuiMgr.GetInst ().JumpBackPanel (PANEL_TYPE.AreanaEntrance);
	}

}