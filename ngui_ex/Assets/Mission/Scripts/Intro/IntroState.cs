public class IntroState : GameStateBase {

    public override void OnEnter(GAME_STATE prev_gs) {
        base.OnEnter(prev_gs);
        // TODO: pnl_login을 등장시키자.
        GuiMgr.GetInst().ShowIntroUI();
    }

    public override void OnLeave(GAME_STATE next_gs) {
        base.OnLeave(next_gs);
        GuiMgr.GetInst().HideIntroUI();
    }

}
