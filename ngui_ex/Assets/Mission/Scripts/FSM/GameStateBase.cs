using UnityEngine;

public class GameStateBase /*: MonoBehaviour*/ {

	private GAME_STATE prev_gs;
    private GAME_STATE next_gs;

	public GAME_STATE Prev { get { return prev_gs; } }
	public GAME_STATE Next { get { return next_gs; } }

	public virtual void OnEnter (GAME_STATE prev_gs) {
		this.prev_gs = prev_gs;
	}

    public virtual void OnLeave(GAME_STATE next_gs) {
        this.next_gs = next_gs;
    }

}