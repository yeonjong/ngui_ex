public class BattleState : GameStateBase {

    public override void OnEnter(GAME_STATE prev_gs) {
        base.OnEnter(prev_gs);
        GuiMgr.GetInst().ShowBattleUI();
    }

    public override void OnLeave(GAME_STATE next_gs) {
        base.OnLeave(next_gs);
        GuiMgr.GetInst().HideBattleUI();
    }

}
