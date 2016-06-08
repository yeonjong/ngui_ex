using System;

/* imsi */
using UnityEngine;

public enum GAME_STATE {
	NullState,

	IntroState,
	PatchState,
	LobbyState,

	ChapterMapState,
	PartyEditState,
	BattleState,

	AreanaEntranceState,
	DefensePartyEditState,
	AttackPartyEditState,
	AreanaBattleState,

	StrongestAreanaEntranceState,
	StrongestAreanaBattleState,

	ShamBattleState,
}

public class GameStateMgr {

	private static GameStateMgr inst;
	private static GameStateBase[] gameStates;
	private static GAME_STATE currentGameState;

	static GameStateMgr() {
		inst = new GameStateMgr();

		gameStates = new GameStateBase[Enum.GetValues(typeof(GAME_STATE)).Length];
		gameStates[(int)GAME_STATE.IntroState] = new IntroState();
		gameStates[(int)GAME_STATE.PatchState] = new PatchState();
		gameStates[(int)GAME_STATE.LobbyState] = new LobbyState();

		gameStates[(int)GAME_STATE.ChapterMapState] = new ChapterMapState();
		gameStates[(int)GAME_STATE.PartyEditState] = new PartyEditState();
		gameStates[(int)GAME_STATE.BattleState] = new BattleState();

		gameStates[(int)GAME_STATE.AreanaEntranceState] = new AreanaEntranceState();
		gameStates[(int)GAME_STATE.DefensePartyEditState] = new DefensePartyEditState();
		gameStates[(int)GAME_STATE.AttackPartyEditState] = new AttackPartyEditState();
		gameStates[(int)GAME_STATE.AreanaBattleState] = new AreanaBattleState();

		gameStates[(int)GAME_STATE.StrongestAreanaEntranceState] = new StrongestAreanaEntranceState();
		gameStates[(int)GAME_STATE.StrongestAreanaBattleState] = new StrongestAreanaBattleState();

		gameStates[(int)GAME_STATE.ShamBattleState] = new ShamBattleState();

		currentGameState = GAME_STATE.NullState;
	}

	private GameStateMgr() { }

	public static GameStateMgr GetInst() {
		return inst;
	}

	public bool BackwardState() {
		GAME_STATE backwardState;

		switch (currentGameState) {
		case GAME_STATE.IntroState:
		case GAME_STATE.PatchState:
		case GAME_STATE.LobbyState:
			backwardState = GAME_STATE.IntroState;
			break;
		case GAME_STATE.ChapterMapState:
			backwardState = GAME_STATE.LobbyState;
			break;
		case GAME_STATE.PartyEditState:
		case GAME_STATE.BattleState:
			backwardState = GAME_STATE.ChapterMapState;
			break;
		case GAME_STATE.AreanaEntranceState:
			backwardState = GAME_STATE.LobbyState;
			break;
		case GAME_STATE.DefensePartyEditState:
		case GAME_STATE.AttackPartyEditState:
		case GAME_STATE.AreanaBattleState:
			backwardState = GAME_STATE.AreanaEntranceState;
			break;
		case GAME_STATE.StrongestAreanaEntranceState:
			backwardState = GAME_STATE.LobbyState;
			break;
		case GAME_STATE.StrongestAreanaBattleState:
			backwardState = GAME_STATE.StrongestAreanaEntranceState;
			break;
		case GAME_STATE.ShamBattleState:
			backwardState = GAME_STATE.LobbyState;
			break;
		default:
			backwardState = GAME_STATE.IntroState;
			break;
		}

		gameStates [(int)currentGameState].OnLeave (backwardState);
		currentGameState = backwardState;

		return true;
	}

	public void ForwardState(GAME_STATE nextGameState) { //nextGameState don't need...

		if (nextGameState.Equals (GAME_STATE.NullState)) {
			Debug.Log ("test mode: don't forward state");
			return;
		}

		switch (currentGameState) {
		case GAME_STATE.NullState:
			gameStates [(int)nextGameState].OnEnter (currentGameState);
			currentGameState = nextGameState;
			break;
		case GAME_STATE.IntroState:
			AssetBundleMgr.GetInst().CheckIsPatched(); // after patch check, continue to OnCompletePatchCheck() function.
			break;
		case GAME_STATE.PatchState:
		case GAME_STATE.LobbyState:
		case GAME_STATE.ChapterMapState:
		case GAME_STATE.PartyEditState:
			gameStates [(int)nextGameState].OnEnter (currentGameState);
			currentGameState = nextGameState;
			break;
		case GAME_STATE.BattleState:
			if (nextGameState.Equals (GAME_STATE.LobbyState)) {
				gameStates [(int)currentGameState].OnLeave (nextGameState);
				currentGameState = nextGameState;
			} else if (nextGameState.Equals (GAME_STATE.ChapterMapState)) {
				gameStates [(int)currentGameState].OnLeave (nextGameState);
				currentGameState = nextGameState;
			} else {
				Debug.LogError ("...");
			}
			break;
		case GAME_STATE.AreanaEntranceState:
		case GAME_STATE.DefensePartyEditState:
		case GAME_STATE.AttackPartyEditState:
		case GAME_STATE.AreanaBattleState:
		case GAME_STATE.StrongestAreanaEntranceState:
		case GAME_STATE.StrongestAreanaBattleState:
		case GAME_STATE.ShamBattleState:
		default:
			gameStates [(int)nextGameState].OnEnter (currentGameState);
			currentGameState = nextGameState;
			break;

		}
	}

	public void OnCompletePatchCheck(bool alreadyPatched) {
		//gameStates [(int)currentGameState].OnLeave (currentGameState);
		GAME_STATE nextGameState;
		if (alreadyPatched) {
			nextGameState = GAME_STATE.LobbyState;
		} else {
			nextGameState = GAME_STATE.PatchState;
		}
		gameStates [(int)nextGameState].OnEnter (currentGameState);
		currentGameState = nextGameState;
	}

}
