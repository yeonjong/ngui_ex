using System;

public enum GAME_STATE {
    IntroState,
    LobbyState,
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
        gameStates[(int)GAME_STATE.LobbyState] = new LobbyState();
        gameStates[(int)GAME_STATE.BattleState] = new BattleState();

        currentGameState = GAME_STATE.IntroState;
    }

    private GameStateMgr() { }

    public static GameStateMgr GetInst() {
        return inst;
    }

    public void ForwardState(GAME_STATE gameState) {
        if (!gameState.Equals(currentGameState))
            gameStates[(int)currentGameState].OnLeave(gameState);
        currentGameState = gameState;
        gameStates[(int)gameState].OnEnter(gameState);
    }

}
