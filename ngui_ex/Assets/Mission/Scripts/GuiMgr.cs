using UnityEngine;
using System.Text;
using System.Collections;

public class GuiMgr : MonoBehaviour {

    private static GuiMgr inst;

    private GameObject pnl_intro;
	private GameObject pnl_patch;

	private GameObject pnl_common_top_bar;
    private GameObject pnl_lobby;
	private GameObject pnl_chapter_map;
	private GameObject pnl_stage_entrance;
	private GameObject pnl_party_edit;
	private GameObject pnl_formation_edit;
    
	private GameObject pnl_battle;

    private PatchPanel patchPanel;

    void Awake() {
        if (!inst) { inst = this; }
    }

    private GuiMgr() { }

    public static GuiMgr GetInst() {
        return inst;
    }

	public void ShowFormationEditUI() {
		if (pnl_formation_edit == null)
			pnl_formation_edit = GameObjectPoolMgr.GetInst ().Load ("pnl_formation_edit", Vector3.zero, Quaternion.identity);

		pnl_formation_edit.SetActive (true);
	}

	public void HideFormationEditUI() {
		if (pnl_formation_edit != null)
			pnl_formation_edit.SetActive(false);
	}

	public void ShowPartyEditUI() {
		if (pnl_party_edit == null)
			pnl_party_edit = GameObjectPoolMgr.GetInst ().Load ("pnl_party_edit", Vector3.zero, Quaternion.identity);

		pnl_party_edit.SetActive (true);
	}

	public void HidePartyEditUI() {
		if (pnl_party_edit != null)
			pnl_party_edit.SetActive(false);
	}

	
	public void ShowStageEntranceUI() {
		ShowCommonTopBarUI ();

		if (pnl_stage_entrance == null)
			pnl_stage_entrance = GameObjectPoolMgr.GetInst ().Load ("pnl_stage_entrance", Vector3.zero, Quaternion.identity);
		
		pnl_stage_entrance.SetActive (true);
	}

	public void HideStageEntranceUI() {
		if (pnl_stage_entrance != null)
			pnl_stage_entrance.SetActive(false);
	}

	public void ShowChapterMapUI() {
		ShowCommonTopBarUI ();

		if (pnl_chapter_map == null)
			pnl_chapter_map = GameObjectPoolMgr.GetInst ().Load ("pnl_chapter_map", Vector3.zero, Quaternion.identity);

		pnl_chapter_map.SetActive (true);
	}

	public void HideChapterMapUI() {
		if (pnl_chapter_map != null)
			pnl_chapter_map.SetActive(false);
	}

    public void ShowPatchUI()
    {
        if (pnl_patch == null)
            pnl_patch = GameObjectPoolMgr.GetInst().Load("pnl_patch", Vector3.zero, Quaternion.identity);

        pnl_patch.SetActive(true);
        patchPanel = pnl_patch.GetComponent<PatchPanel>();
    }


    public void HidePatchUI() {
        if (pnl_patch != null) {
            pnl_patch.SetActive(false);
            patchPanel = null;
        }
    }

    public void ShowBattleUI()
    {
		HideCommonTopBarUI ();

        if (pnl_battle == null)
            pnl_battle = GameObjectPoolMgr.GetInst().Load("pnl_battle", Vector3.zero, Quaternion.identity);

        pnl_battle.SetActive(true);
    }

    public void HideBattleUI() {
        if (pnl_battle != null)
            pnl_battle.SetActive(false);
    }

    public void ShowLobbyUI() {
		ShowCommonTopBarUI ();

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

	private void ShowCommonTopBarUI () {
		if (pnl_common_top_bar == null)
			pnl_common_top_bar = GameObjectPoolMgr.GetInst ().Load ("pnl_common_top_bar", Vector3.zero, Quaternion.identity);
	
		pnl_common_top_bar.SetActive (true);
	}

	private void HideCommonTopBarUI() {
		if (pnl_common_top_bar != null)
			pnl_common_top_bar.SetActive (false);
	}

    private void ShowWelcomSubPanel(string userID) {
		HideCommonTopBarUI ();

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
        GameStateMgr.GetInst().ForwardState(GAME_STATE.PatchState);
    }

    public void FailLogin(string log) {
        Debug.Log(log);
        pnl_intro.GetComponent<IntroPanel>().InitLoginInputFields();
    }
    
    public void TryLogin(string id, string pw) {
        LoginChecker loginChecker = new LoginChecker();
        loginChecker.CheckLoginInfomation(id, pw);
    }

    public void OnPatchProgressChanged(float percent) {
        patchPanel.SetPatchProgress(percent);
    }

    public void OnPatchCompleted() {
        patchPanel.ShowLobbyBtn();
    }

	public void OnSelectFormation() {
		pnl_party_edit.GetComponent<PartyEditPanel> ().CheckFormation ();
	}
	
}
