using UnityEngine;
using System.Text;
//using System.Collections;

public class GuiMgr : MonoBehaviour {

    private static GuiMgr inst;

    private GameObject panelsRoot;

    private GameObject pnl_intro;
    private GameObject pnl_lobby;
    private GameObject pnl_battle;

    void Awake() {
        if (!inst) { inst = this; }

        panelsRoot = transform.FindChild("Panels").gameObject;

        //pnl_intro = panelsRoot.AddChild(Resources.Load("pnl_intro") as GameObject);

        //pnl_lobby = panelsRoot.AddChild(Resources.Load("pnl_lobby") as GameObject);
        //pnl_lobby.SetActive(false);

        //pnl_battle = panelsRoot.AddChild(Resources.Load("pnl_battle") as GameObject);
        //pnl_battle.SetActive(false);

        // tODO:
        //pnl_intro = GameObjectPoolMgr.inst.Load("pnl_intro", Vector3.zero, Quaternion.identity);
        //pnl_intro.transform.parent = ...;


    }

    private GuiMgr() { }

    public static GuiMgr GetInst() {
        return inst;
    }

    /*
	void Start () {
        // TODO: pnl_login 을 생성해서 Panels의 자식으로 생성하자.
        // a-z, 0-9, 특수문자 // 중 2가지 이상으로 구성되는지 확인.
        MakeLoginPanel();
    }
    */

    public void ShowBattleUI()
    {
        pnl_battle.SetActive(true);
    }

    public void HideBattleUI()
    {
        pnl_battle.SetActive(false);
    }

    public void ShowLobbyUI() {
        pnl_lobby.SetActive(true);
    }

    public void HideLobbyUI() {
        pnl_lobby.SetActive(false);
    }

    public void ShowIntroUI() {
        pnl_intro.SetActive(true);

        string userID = LoginChecker.GetUserID();
        if (userID == null) {
            ShowLoginSubPanel();
        } else {
            ShowWelcomSubPanel(userID);
        }
    }

    public void HideIntroUI() {
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

    private void SuccessLogin(string log) {
        Debug.Log(log);
        pnl_intro.SetActive(false);
        GameStateMgr.getInst().ForwardState(GAME_STATE.LobbyState);
    }

    private void FailLogin(string log) {
        Debug.Log(log);
        pnl_intro.GetComponent<IntroPanel>().InitLoginInputFields();
    }
    
    public void TryLogin(string id, string pw) {
        LoginChecker loginChecker = new LoginChecker();
        string log = null;
        bool isSuccess = loginChecker.CheckLoginInfomation(id, pw, ref log);

        if (isSuccess) SuccessLogin(log);
        else FailLogin(log);
    }
	
}
