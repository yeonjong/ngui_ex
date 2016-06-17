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
	ShamBattleEntrance,					//모의전 준비.
	MapChoice,							//(모의전 )맵 선택.
}

public class GuiMgr : MonoBehaviour {

    private static GuiMgr inst;
	// TODO: please merge this array to m_pnlInstances.
	private static string[] panelNames = new string[] {"pnl_common_top_bar", "pnl_intro", "pnl_patch", "pnl_lobby", "pnl_chapter_map", "pnl_stage_entrance", "pnl_party_edit", "pnl_formation_edit", "pnl_battle",
		"pnl_areana_entrance", "pnl_strongest_areana_entrance", "pnl_other_user_party_info", "pnl_character_info", "pnl_formation_info", "pnl_strongest_other_user_party_info", "pnl_defense_party_edit",
		"pnl_change_party", "pnl_attack_party_edit", "pnl_areana_intro_choreography", "pnl_strongest_areana_intro_choreography", "pnl_areana_battle", "pnl_areana_ending_choreography", "pnl_areana_ranking",
		"pnl_areana_record", "pnl_areana_record_review_check", "pnl_strongest_areana_record_review_check", "pnl_areana_reward", "pnl_item_info", "pnl_areana_help", "pnl_areana_cumulative", "pnl_sham_battle_entrance", "pnl_map_choice"};


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

	public void Backward() {
		if (m_panelStack.Count == 0) {
			GameStateMgr.GetInst ().Backward ();
		} else {
			m_pnlInstances [(int)m_panelStack.Peek ()].GetComponent<PanelBase> ().OnClickXXXBtn("btn_back");
		}
	}

	public bool CheckContainsTargetPanel(PANEL_TYPE target) {
		return m_panelStack.Contains (target);
	}

	private void ShowStack() {
		StringBuilder sb = new StringBuilder ("Stack: ");
		foreach (PANEL_TYPE t in m_panelStack.ToArray()) {
			sb.Append (t);
			sb.Append (" ");
		}
		sb.Append (showDepth);
		sb.Append ("\n");
		Debug.Log (sb.ToString());
	}

	int showDepth = 0;
	public void PushPnl(PANEL_TYPE target, bool hideCurrPnl = true) {
		Debug.Log ("Push " + target);

		if (hideCurrPnl) {
			if (m_panelStack.Count != 0) {
				if (showDepth < 0) {
					Stack<PANEL_TYPE> temp = new Stack<PANEL_TYPE> ();
					for (int i = showDepth; i <= 0; i++) {
						HidePanel (m_panelStack.Peek ());
						temp.Push (m_panelStack.Pop ());
					}
					while (temp.Count != 0) {
						Debug.Log ("hide " + temp.Peek());
						m_panelStack.Push (temp.Pop ());
					}
					showDepth = 0;
				} else {
					HidePanel (m_panelStack.Peek ());
				}
			}

		} else {
			showDepth -= 1;
		}

		m_panelStack.Push (target);
		ShowPanel (target);

		ShowStack ();
	}

	public void PopPnl() {
		if (showDepth < 0)
			showDepth++;
		#if UNITY_EDITOR
		if (m_panelStack.Count != 0) HidePanel (m_panelStack.Pop ());
		if (m_panelStack.Count != 0) ShowPanel (m_panelStack.Peek ());
		ShowStack ();
		#elif
		HidePanel (m_panelStack.Pop ());
		ShowPanel (m_panelStack.Peek ());
		#endif
	}

	public void PopPnl(PANEL_TYPE target, params PANEL_TYPE[] args) {
		if (m_panelStack.Contains (target)) {
			while (!target.Equals (m_panelStack.Peek ())) {
				HidePanel (m_panelStack.Pop ());
			}
		} else {
			Debug.LogError ("don't exist target panel in stack");
			m_panelStack.Clear ();
			m_panelStack.Push (target);
		}
		ShowPanel (m_panelStack.Peek());

		if (args == null) {
			showDepth = 0;
		} else {
			for (int i = 0; i < args.Length; i++) {
				m_panelStack.Push (args [i]);
				ShowPanel (args [i]);
			}
			showDepth = args.Length * -1;
		}

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
		case PANEL_TYPE.AreanaEntrance:
		case PANEL_TYPE.ShamBattleEntrance:
		case PANEL_TYPE.AreanaRecord:
			ShowPanel (PANEL_TYPE.CommonTopBar);
			break;

		case PANEL_TYPE.PartyEdit:
		case PANEL_TYPE.FormationEdit:
			break;

		case PANEL_TYPE.Battle:
		case PANEL_TYPE.AreanaIntroChoreography:
		case PANEL_TYPE.StrongestAreanaIntroChoreography:
		case PANEL_TYPE.AreanaBattle:
		case PANEL_TYPE.DefensePartyEdit:
		case PANEL_TYPE.AttackPartyEdit:
			HidePanel (PANEL_TYPE.CommonTopBar);
			break;

		/* don't check */
		//case PANEL_TYPE.AreanaEntrance:
		//	break;
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
		//case PANEL_TYPE.DefensePartyEdit:
		//	break;
		case PANEL_TYPE.ChangeParty:
			break;
		//case PANEL_TYPE.AttackPartyEdit:
		//	break;
		//case PANEL_TYPE.AreanaIntroChoreography:
		//	break;
		//case PANEL_TYPE.StrongestAreanaIntroChoreography:
		//	break;
		//case PANEL_TYPE.AreanaBattle:
		//	break;
		case PANEL_TYPE.AreanaEndingChoreography:
			break;
		case PANEL_TYPE.AreanaRanking:
			break;
		//case PANEL_TYPE.AreanaRecord:
		//	break;
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
		//case PANEL_TYPE.ShamBattle:
		//	break;
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
	public void OnSelectFormation(int formNum) {
		if (m_panelStack.Contains (PANEL_TYPE.PartyEdit)) {
			m_pnlInstances [(int)PANEL_TYPE.PartyEdit].GetComponent<PanelBase> ().OnClickXXXBtn (formNum.ToString ());
		} else if (m_panelStack.Contains (PANEL_TYPE.AttackPartyEdit)) {
			m_pnlInstances [(int)PANEL_TYPE.AttackPartyEdit].GetComponent<PanelBase> ().OnClickXXXBtn (formNum.ToString ());
		} else if (m_panelStack.Contains (PANEL_TYPE.DefensePartyEdit)) {
			m_pnlInstances [(int)PANEL_TYPE.DefensePartyEdit].GetComponent<PanelBase> ().OnClickXXXBtn (formNum.ToString ());
		}
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
