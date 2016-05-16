public class GameStateBase {

    private GAME_STATE prev_gs;
    private GAME_STATE next_gs;

    public virtual void OnEnter(GAME_STATE prev_gs) {
        this.prev_gs = prev_gs;
    }

    public virtual void OnLeave(GAME_STATE next_gs) {
        this.next_gs = next_gs;
    }
}
