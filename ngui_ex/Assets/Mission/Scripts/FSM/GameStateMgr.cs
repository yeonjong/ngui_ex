using System;

/* imsi */
using UnityEngine;

public enum GAME_STATE {
	nullState,
    IntroState,
    PatchState,
    LobbyState,
	ChapterMapState,
	StageEntranceState, // if don't need this, delete state.
	PartyEditState,
	FormationEditState, // if don't need this, delete state.
    BattleState,
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
		gameStates[(int)GAME_STATE.StageEntranceState] = new StageEntranceState();
		gameStates[(int)GAME_STATE.PartyEditState] = new PartyEditState();
		gameStates[(int)GAME_STATE.FormationEditState] = new FormationEditState();
        gameStates[(int)GAME_STATE.BattleState] = new BattleState();

        currentGameState = GAME_STATE.nullState;
    }

    private GameStateMgr() { }

    public static GameStateMgr GetInst() {
        return inst;
    }

	public bool BackwardState() {
		if (currentGameState.Equals (GAME_STATE.IntroState)) {
			Debug.Log ("TODO: Active Exit Popup panel");
			// TODO: Active Exit Popup panel.
			// 두번 연속 눌렸을때 확인해야됨.
			return true;
		}
			
		switch (currentGameState) {
		case GAME_STATE.PatchState:
			gameStates [(int)currentGameState].OnLeave (currentGameState);
			currentGameState = GAME_STATE.IntroState;
			gameStates [(int)currentGameState].OnEnter (currentGameState);
			break;
		case GAME_STATE.LobbyState:
			gameStates [(int)currentGameState].OnLeave (currentGameState);
			currentGameState = GAME_STATE.IntroState;
			gameStates [(int)currentGameState].OnEnter (currentGameState);
			break;
		case GAME_STATE.ChapterMapState:
			gameStates [(int)currentGameState].OnLeave (currentGameState);
			currentGameState = GAME_STATE.LobbyState;
			gameStates [(int)currentGameState].OnEnter (currentGameState);
			break;
		case GAME_STATE.StageEntranceState:
			gameStates [(int)currentGameState].OnLeave (currentGameState);
			currentGameState = GAME_STATE.ChapterMapState;
			break;
		case GAME_STATE.PartyEditState:
			gameStates [(int)currentGameState].OnLeave (currentGameState);
			currentGameState = GAME_STATE.ChapterMapState;
			gameStates [(int)currentGameState].OnEnter (currentGameState);
			break;
		case GAME_STATE.FormationEditState:
			gameStates [(int)currentGameState].OnLeave (currentGameState);
			currentGameState = GAME_STATE.PartyEditState;
			break;
		case GAME_STATE.BattleState:
			gameStates [(int)currentGameState].OnLeave (currentGameState);
			currentGameState = GAME_STATE.ChapterMapState;
			gameStates [(int)currentGameState].OnEnter (currentGameState);
			break;
		}

		return true;
	}

    public void ForwardState(GAME_STATE nextGameState) { //nextGameState don't need...

		switch (currentGameState) {
		case GAME_STATE.nullState:
			currentGameState = GAME_STATE.IntroState;
			gameStates [(int)currentGameState].OnEnter (currentGameState);
			break;
		case GAME_STATE.IntroState:
			AssetBundleMgr.GetInst().CheckIsPatched(); // after patch check, continue to OnCompletePatchCheck() function.
			break;
		case GAME_STATE.PatchState:
			gameStates [(int)currentGameState].OnLeave (currentGameState);
			currentGameState = GAME_STATE.LobbyState;
			gameStates [(int)currentGameState].OnEnter (currentGameState);
			break;
		case GAME_STATE.LobbyState:
			gameStates [(int)currentGameState].OnLeave (currentGameState);
			currentGameState = GAME_STATE.ChapterMapState;
			gameStates [(int)currentGameState].OnEnter (currentGameState);
			break;
		case GAME_STATE.ChapterMapState:
			currentGameState = GAME_STATE.StageEntranceState;
			gameStates [(int)currentGameState].OnEnter (currentGameState);
			break;
		case GAME_STATE.StageEntranceState:
			gameStates [(int)GAME_STATE.ChapterMapState].OnLeave (GAME_STATE.ChapterMapState);
			gameStates [(int)currentGameState].OnLeave (currentGameState);
			currentGameState = GAME_STATE.PartyEditState;
			gameStates [(int)currentGameState].OnEnter (currentGameState);
			break;
		case GAME_STATE.PartyEditState:
			if (nextGameState.Equals (GAME_STATE.FormationEditState)) {
			} else if (nextGameState.Equals (GAME_STATE.BattleState)) {
				gameStates [(int)currentGameState].OnLeave (currentGameState);
			}
			currentGameState = nextGameState;
			gameStates [(int)currentGameState].OnEnter (currentGameState);
			break;
		case GAME_STATE.FormationEditState:
			break;
		case GAME_STATE.BattleState:
			if (nextGameState.Equals (GAME_STATE.LobbyState)) {
				gameStates [(int)currentGameState].OnLeave (currentGameState);
				currentGameState = nextGameState;
			} else if (nextGameState.Equals (GAME_STATE.ChapterMapState)) {
				gameStates [(int)currentGameState].OnLeave (currentGameState);
				currentGameState = nextGameState;
			} else if (nextGameState.Equals (GAME_STATE.StageEntranceState)) {
				gameStates [(int)currentGameState].OnLeave (currentGameState);
				currentGameState = nextGameState;
				gameStates [(int)GAME_STATE.ChapterMapState].OnEnter (GAME_STATE.ChapterMapState);
			}
			gameStates [(int)currentGameState].OnEnter (currentGameState);
			break;
		}


		/*
        if (!gameState.Equals(currentGameState))
            gameStates[(int)currentGameState].OnLeave(gameState);
        currentGameState = gameState;
        gameStates[(int)gameState].OnEnter(gameState);
		*/
	}

	public void OnCompletePatchCheck(bool alreadyPatched) {
		gameStates [(int)currentGameState].OnLeave (currentGameState);
		if (alreadyPatched) {
			currentGameState = GAME_STATE.LobbyState;
		} else {
			currentGameState = GAME_STATE.PatchState;
		}
		gameStates [(int)currentGameState].OnEnter (currentGameState);
	}

}
