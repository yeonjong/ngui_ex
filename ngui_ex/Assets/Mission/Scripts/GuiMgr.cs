using UnityEngine;
using System.Text;
//using System.Collections;

public class GuiMgr : MonoBehaviour {

    private static GuiMgr inst;

    private GameObject pnl_intro;
    private GameObject pnl_lobby;
    private GameObject pnl_battle;

    void Awake() {
        if (!inst) { inst = this; }
    }
    
    private GuiMgr() { }

    public static GuiMgr GetInst() {
        return inst;
    }

    public void ShowBattleUI()
    {
        if (pnl_battle == null)
            pnl_battle = GameObjectPoolMgr.GetInst().Load("pnl_battle", Vector3.zero, Quaternion.identity);

        pnl_battle.SetActive(true);
    }

    public void HideBattleUI()
    {
        if (pnl_battle != null)
            pnl_battle.SetActive(false);
    }

    public void ShowLobbyUI() {
        if (pnl_lobby == null)
            pnl_lobby = GameObjectPoolMgr.GetInst().Load("pnl_lobby", Vector3.zero, Quaternion.identity);

        pnl_lobby.SetActive(true);
    }

    public void HideLobbyUI() {
        if (pnl_lobby != null)
            pnl_lobby.SetActive(false);
    }

    public void ShowIntroUI() {
        if (pnl_intro == null)
            pnl_intro = GameObjectPoolMgr.GetInst().Load("pnl_intro", Vector3.zero, Quaternion.identity);
        
        pnl_intro.SetActive(true);

        string userID = LoginChecker.GetUserID();
        if (userID == null) {
            ShowLoginSubPanel();
        } else {
            ShowWelcomSubPanel(userID);
        }
    }

    public void HideIntroUI() {
        if (pnl_intro != null)
            pnl_intro.SetActive(false);
    }

    private void ShowWelcomSubPanel(string userID) {
        StringBuilder sb = new StringBuilder("Welcome ");
        sb.Append(userID);
        sb.Append(" !");
        pnl_intro.GetComponent<IntroPanel>().ShowWelcomePanel(sb.ToString());
    }

    private void ShowLoginSubPanel() {
        pnl_intro.GetComponent<IntroPanel>().ShowLoginPanel();
    }

    public void SuccessLogin(string log) {
        Debug.Log(log);
        pnl_intro.SetActive(false);
        GameStateMgr.GetInst().ForwardState(GAME_STATE.LobbyState);
    }

    public void FailLogin(string log) {
        Debug.Log(log);
        pnl_intro.GetComponent<IntroPanel>().InitLoginInputFields();
    }
    
    public void TryLogin(string id, string pw) {
        LoginChecker loginChecker = new LoginChecker();
        loginChecker.CheckLoginInfomation(id, pw);
    }
	
}
