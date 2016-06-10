using System;

/* imsi */
using UnityEngine;

public enum GAME_STATE {
	NullState,

	IntroState,
	PatchState,
	LobbyState,

	OtherState,
}

public class GameStateMgr {

	private static GameStateMgr inst;
	private static GameStateBase[] gameStates;
	private static GAME_STATE currGameState;
	public GAME_STATE Curr { get { return currGameState; } }

	static GameStateMgr() {
		inst = new GameStateMgr();

		gameStates = new GameStateBase[Enum.GetValues(typeof(GAME_STATE)).Length];
		gameStates[(int)GAME_STATE.IntroState] = new IntroState();
		gameStates[(int)GAME_STATE.PatchState] = new PatchState();
		gameStates[(int)GAME_STATE.LobbyState] = new LobbyState();

		currGameState = GAME_STATE.NullState;
		//prevGameState = GAME_STATE.NullState;
	}

	private GameStateMgr() { }

	public static GameStateMgr GetInst() {
		return inst;
	}

	/*
	public bool BackwardState() {
		if (prevGameState.Equals (GAME_STATE.NullState)) {
			Debug.Log ("exit popup");
			return true;
		}

		gameStates [(int)currGameState].OnLeave (prevGameState);
		currGameState = prevGameState;
		return true;
	}
	*/
	/*
	public void JumpBackState(GAME_STATE nextGameState) {
		switch (currGameState) {
		case GAME_STATE.BattleState:
		case GAME_STATE.AreanaBattleState:
			gameStates [(int)currGameState].OnLeave (nextGameState);
			currGameState = nextGameState;
			break;
		default:
			Debug.Log ("don't implemented jumpback form the " + currGameState);
			break;
		}
	}
	*/

	public void Backward() {
		switch (currGameState) {
		case GAME_STATE.IntroState:
		case GAME_STATE.LobbyState:
			gameStates [(int)currGameState].OnLeave (GAME_STATE.NullState);
			break;
		case GAME_STATE.PatchState:
			gameStates [(int)currGameState].OnLeave (GAME_STATE.IntroState);
			currGameState = GAME_STATE.IntroState;
			break;
		default:
			currGameState = GAME_STATE.LobbyState;
			//Debug.LogError ("");
			break;
		}
	}

	public void ForwardState(GAME_STATE nextGameState) {
		if (nextGameState.Equals (GAME_STATE.NullState) || nextGameState.Equals(GAME_STATE.OtherState)) {
			currGameState = nextGameState;
			return;
		}

		if (nextGameState.Equals (GAME_STATE.PatchState)) {
			AssetBundleMgr.GetInst().CheckIsPatched(); // after patch check, continue to OnCompletePatchCheck() function.
			return;
		}

		gameStates [(int)nextGameState].OnEnter (currGameState);
		currGameState = nextGameState;
	}

	public void OnCompletePatchCheck(bool alreadyPatched) {
		GAME_STATE nextGameState;
		if (alreadyPatched) {
			nextGameState = GAME_STATE.LobbyState;
		} else {
			nextGameState = GAME_STATE.PatchState;
		}
		gameStates [(int)nextGameState].OnEnter (currGameState);
		currGameState = nextGameState;
	}

}
