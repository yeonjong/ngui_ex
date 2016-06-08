using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

//NormalPopup,						//Yes&No, OK 류의 팝업.

public enum PANEL_TYPE {
	CommonTopBar,

	Intro,								//
	Patch,								//
	Lobby,								//

	ChapterMap,							//(Single Mode )Map 선택.
	StageEntrance,						//(Single Mode )Stage 진입.
	PartyEdit,							//(Single Mode )Party 편집.
	FormationEdit,						//(Single Mode )진형 선택.

	Battle,								//

	AreanaEntrance,						//아레나 준비.
	StrongestAreanaEntrance,			//최강전장 준비 (최강전장).
	OtherUserPartyInfo,					//상대 유저 방어 파티 정보.
	CharacterInfo,						//
	FormationInfo,						//
	StrongestOtherUserPartyInfo,		//최강전장 상대 유저 방어 파티 정보 (최강전장).
	DefensePartyEdit,					//방어 파티 편성.
	ChangeParty,						//팀 교체 (최강전장).
	AttackPartyEdit,					//공격 파티 편성.
	AreanaIntroChoreography,			//아레나 시작 연출.
	StrongestAreanaIntroChoreography,	//최강전장 시작 연출 (최강전장).
	AreanaBattle,						//아레나 대전 화면.
	AreanaEndingChoreography,			//아레나 종료 연출.
	AreanaRanking,						//아레나 랭킹.
	AreanaRecord,						//아레나 기록.
	AreanaRecordReviewCheck,			//전투 기록 재생여부 확인.
	StrongestAreanaRecordReviewCheck,	//최강전장 전투 기록 재생여부 확인 (최강전장).
	AreanaReward,						//아레나 보상.
	ItemInfo,							//
	AreanaHelp,							//아레나 도움.
	AreanaCumulative,					//아레나 누적 (최강전장).
	ShamBattle,							//모의전.
	MapChoice,							//(모의전 )맵 선택.
}

public class GuiMgr : MonoBehaviour {

    private static GuiMgr inst;
	// TODO: please merge this array to m_pnlInstances.
	private static string[] panelNames = new string[] {"pnl_common_top_bar", "pnl_intro", "pnl_patch", "pnl_lobby", "pnl_chapter_map", "pnl_stage_entrance", "pnl_party_edit", "pnl_formation_edit", "pnl_battle", "pnl_", "pnl_", "pnl_", "pnl_", "pnl_", "pnl_"};


	private GameObject pnl_common_top_bar;
	private GameObject[] m_pnlInstances;
	private Stack<PANEL_TYPE> m_panelStack;

	private PatchPanel patchPanel; //TODO: please find a way. like action | delegate...
    
	void Awake() {
        if (!inst) { inst = this; }

		m_pnlInstances = new GameObject[Enum.GetValues(typeof(PANEL_TYPE)).Length];
		m_panelStack = new Stack<PANEL_TYPE> ();
    }

    private GuiMgr() { }

    public static GuiMgr GetInst() {
        return inst;
    }

	private void ShowStack() {
		StringBuilder sb = new StringBuilder ("Stack: ");
		foreach (PANEL_TYPE t in m_panelStack.ToArray()) {
			sb.Append (t);
			sb.Append (" ");
		}
		sb.Append ("\n");
		Debug.Log (sb.ToString());
	}

	public void JumpBackPanel(PANEL_TYPE panelType) {
		Debug.Log ("JumpBack " + panelType);

		HidePanel (m_panelStack.Pop ());

		if (m_panelStack.Contains (panelType)) {
			while (!m_panelStack.Peek().Equals(panelType)) {
				HidePanel(m_panelStack.Pop ());
			}
			ShowPanel (m_panelStack.Peek());
		} else {
			m_panelStack.Clear ();
			PushPanel (panelType);
		}

		ShowStack ();
	}

	public void PushPanel(PANEL_TYPE panelType) {
		Debug.Log ("Push " + panelType);

		if (m_panelStack.Count != 0) {
			if (panelType.Equals (PANEL_TYPE.PartyEdit) && m_panelStack.Peek ().Equals (PANEL_TYPE.StageEntrance)) {
				if (m_panelStack.Contains (PANEL_TYPE.ChapterMap)) {
					Stack<PANEL_TYPE> tempStack = new Stack<PANEL_TYPE> ();
					PANEL_TYPE tempType;
					while (!m_panelStack.Peek ().Equals (PANEL_TYPE.ChapterMap)) {
						tempType = m_panelStack.Pop ();
						tempStack.Push (tempType);
						HidePanel (tempType);
					}
					HidePanel (m_panelStack.Peek ());
					while (tempStack.Count != 0) {
						m_panelStack.Push (tempStack.Pop());
					}
				} else {
					m_panelStack.Clear ();
				}
			} else if (panelType.Equals (PANEL_TYPE.StageEntrance) && m_panelStack.Peek ().Equals (PANEL_TYPE.ChapterMap)) {
				
			} else if (panelType.Equals (PANEL_TYPE.FormationEdit) && m_panelStack.Peek().Equals(PANEL_TYPE.PartyEdit)) {
				
			} else {
				HidePanel (m_panelStack.Peek ());
			}
		}

		m_panelStack.Push (panelType);
		ShowPanel (panelType);

		ShowStack ();
	}

	public void PopPanel() {
		Debug.Log ("Pop");

		HidePanel (m_panelStack.Pop ());
		ShowPanel (m_panelStack.Peek ());

		ShowStack ();
	}

	public void ShowPanel(PANEL_TYPE panelType) {
		int panelIndex = (int)panelType;
		if (m_pnlInstances[panelIndex] == null)
			m_pnlInstances[panelIndex] = GameObjectPoolMgr.GetInst ().Load (panelNames[panelIndex], Vector3.zero, Quaternion.identity);

		switch (panelType) {
		case PANEL_TYPE.CommonTopBar:
			break;

		case PANEL_TYPE.Intro:
			string userID = LoginChecker.GetUserID ();
			if (userID == null) {
				m_pnlInstances[(int)PANEL_TYPE.Intro].GetComponent<IntroPanel>().ShowLoginPanel();

			} else {
				HidePanel (PANEL_TYPE.CommonTopBar);

				StringBuilder sb = new StringBuilder("Welcome ");
				sb.Append(userID);
				sb.Append(" !");
				m_pnlInstances[(int)PANEL_TYPE.Intro].GetComponent<IntroPanel>().ShowWelcomePanel(sb.ToString());
			}
			break;

		case PANEL_TYPE.Patch:
			patchPanel = m_pnlInstances[panelIndex].GetComponent<PatchPanel>();
			break;

		case PANEL_TYPE.Lobby:
		case PANEL_TYPE.ChapterMap:
		case PANEL_TYPE.StageEntrance:
			ShowPanel (PANEL_TYPE.CommonTopBar);
			break;

		case PANEL_TYPE.PartyEdit:
		case PANEL_TYPE.FormationEdit:
			break;

		case PANEL_TYPE.Battle:
			HidePanel (PANEL_TYPE.CommonTopBar);
			break;

		/* don't check */
		case PANEL_TYPE.AreanaEntrance:
			ShowPanel (PANEL_TYPE.CommonTopBar);
			break;
		case PANEL_TYPE.StrongestAreanaEntrance:
			break;
		case PANEL_TYPE.OtherUserPartyInfo:
			break;
		case PANEL_TYPE.CharacterInfo:
			break;
		case PANEL_TYPE.FormationInfo:
			break;
		case PANEL_TYPE.StrongestOtherUserPartyInfo:
			break;
		case PANEL_TYPE.DefensePartyEdit:
			break;
		case PANEL_TYPE.ChangeParty:
			break;
		case PANEL_TYPE.AttackPartyEdit:
			break;
		case PANEL_TYPE.AreanaIntroChoreography:
		case PANEL_TYPE.StrongestAreanaIntroChoreography:
			HidePanel (PANEL_TYPE.CommonTopBar);
			break;
		case PANEL_TYPE.AreanaBattle:
			break;
		case PANEL_TYPE.AreanaEndingChoreography:
			break;
		case PANEL_TYPE.AreanaRanking:
			break;
		case PANEL_TYPE.AreanaRecord:
			break;
		case PANEL_TYPE.AreanaRecordReviewCheck:
			break;
		case PANEL_TYPE.StrongestAreanaRecordReviewCheck:
			break;
		case PANEL_TYPE.AreanaReward:
			break;
		case PANEL_TYPE.ItemInfo:
			break;
		case PANEL_TYPE.AreanaHelp:
			break;
		case PANEL_TYPE.AreanaCumulative:
			break;
		case PANEL_TYPE.ShamBattle:
			break;
		case PANEL_TYPE.MapChoice:
			break;
		}

		m_pnlInstances[panelIndex].SetActive (true);
	}

	public void HidePanel(PANEL_TYPE panelType) {
		int panelIndex = (int)panelType;
		if (m_pnlInstances [panelIndex] != null)
			m_pnlInstances [panelIndex].SetActive (false);

		if (panelType.Equals (PANEL_TYPE.Patch)) {
			// TODO: this area should be managed by GameObjectPoolMgr.
			Destroy (m_pnlInstances [panelIndex]);
			m_pnlInstances [panelIndex] = null;
		}
	}

	/* login */
    public void SuccessLogin(string log) {
		Debug.Log (log);
        GameStateMgr.GetInst().ForwardState(GAME_STATE.PatchState);
    }

    public void FailLogin(string log) {
        Debug.Log(log);
		m_pnlInstances[(int)PANEL_TYPE.Intro].GetComponent<IntroPanel>().InitLoginInputFields();
    }
    
    public void TryLogin(string id, string pw) {
        LoginChecker loginChecker = new LoginChecker();
        loginChecker.CheckLoginInfomation(id, pw);
    }

	/* patch */
    public void OnPatchProgressChanged(float percent) {
        patchPanel.SetPatchProgress(percent);
    }

    public void OnPatchCompleted() {
        patchPanel.ShowLobbyBtn();
    }

	/* party edit */
	public void OnSelectFormation() {
		m_pnlInstances[(int)PANEL_TYPE.PartyEdit].GetComponent<PartyEditPanel> ().CheckFormation ();
	}















	/*
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

	public void ShowPartyEditUI() {
		if (pnl_party_edit == null)
			pnl_party_edit = GameObjectPoolMgr.GetInst ().Load ("pnl_party_edit", Vector3.zero, Quaternion.identity);

		pnl_party_edit.SetActive (true);
	}

	public void HidePartyEditUI() {
		if (pnl_party_edit != null)
			pnl_party_edit.SetActive(false);
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

	private void ShowCommonTopBar () {
		if (pnl_common_top_bar == null)
			pnl_common_top_bar = GameObjectPoolMgr.GetInst ().Load ("pnl_common_top_bar", Vector3.zero, Quaternion.identity);
	
		pnl_common_top_bar.SetActive (true);
	}

	private void HideCommonTopBar() {
		if (pnl_common_top_bar != null)
			pnl_common_top_bar.SetActive (false);
	}

    private void ShowWelcomSubPanel(string userID) {
		ShowPanel (PANEL_TYPE.CommonTopBar);

        StringBuilder sb = new StringBuilder("Welcome ");
        sb.Append(userID);
        sb.Append(" !");
		m_pnlInstances[(int)PANEL_TYPE.Intro].GetComponent<IntroPanel>().ShowWelcomePanel(sb.ToString());
    }

    private void ShowLoginSubPanel() {
		m_pnlInstances[(int)PANEL_TYPE.Intro].GetComponent<IntroPanel>().ShowLoginPanel();
    }
	*/
}
