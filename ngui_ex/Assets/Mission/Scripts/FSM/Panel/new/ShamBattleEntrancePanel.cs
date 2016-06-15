using UnityEngine;
using System;
using System.Text;
using System.Collections;

public class ShamBattleEntrancePanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
			GameStateMgr.GetInst ().Backward ();
			GuiMgr.GetInst ().PopPnl ();
			break;
		case "btn_attack_party_edit":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AttackPartyEdit);
			break;
		case "btn_defense_party_edit":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.DefensePartyEdit);
			break;
		case "btn_map_choice":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.MapChoice, false);
			break;
		case "btn_battle_start":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaIntroChoreography);
			break;

		case "btn_atk_party_formation_info":
			GlobalApp.Inst.SetOtherUser (-1, PANEL_TYPE.ShamBattleEntrance, PARTY_TYPE.AreanaAtk);
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.FormationInfo, false);
			break;
		case "btn_atk_party_character_info0":
		case "btn_atk_party_character_info1":
		case "btn_atk_party_character_info2":
		case "btn_atk_party_character_info3":
		case "btn_atk_party_character_info4":
		case "btn_atk_party_character_info5":
		case "btn_atk_party_character_info6":
		case "btn_atk_party_character_info7":
			GlobalApp.Inst.SetOtherUser (-1, PANEL_TYPE.ShamBattleEntrance, PARTY_TYPE.AreanaAtk);
			GlobalApp.Inst.selectedOtherUserChar = Int32.Parse (btnName.Substring (btnName.Length - 1));
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.CharacterInfo, false);
			break;

		case "btn_def_party_formation_info":
			GlobalApp.Inst.SetOtherUser (-1, PANEL_TYPE.ShamBattleEntrance, PARTY_TYPE.AreanaDef);
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.FormationInfo, false);
			break;
		case "btn_def_party_character_info0":
		case "btn_def_party_character_info1":
		case "btn_def_party_character_info2":
		case "btn_def_party_character_info3":
		case "btn_def_party_character_info4":
		case "btn_def_party_character_info5":
		case "btn_def_party_character_info6":
		case "btn_def_party_character_info7":
			GlobalApp.Inst.SetOtherUser (-1, PANEL_TYPE.ShamBattleEntrance, PARTY_TYPE.AreanaDef);
			GlobalApp.Inst.selectedOtherUserChar = Int32.Parse (btnName.Substring (btnName.Length - 1));
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.CharacterInfo, false);
			break;
		}
	}

	private UILabel[] m_atkPartyLabels;
	private UISprite[] m_atkPartySprites;

	private UILabel[] m_defPartyLabels;
	private UISprite[] m_defPartySprites;

	void Awake() {
		m_atkPartyLabels = new UILabel[2];
		m_atkPartyLabels[0] = transform.FindChild ("atk_party/lbl_party_fighting_power").GetComponent<UILabel> ();
		m_atkPartyLabels[1] = transform.FindChild ("atk_party/lbl_party_cost").GetComponent<UILabel> ();

		m_atkPartySprites = new UISprite[FixedConstantValue.PARTY_MAX_NUM];
		m_atkPartySprites [0] = transform.FindChild ("atk_party/btn_atk_party_character_info0").GetComponent<UISprite> ();
		m_atkPartySprites [1] = transform.FindChild ("atk_party/btn_atk_party_character_info1").GetComponent<UISprite> ();
		m_atkPartySprites [2] = transform.FindChild ("atk_party/btn_atk_party_character_info2").GetComponent<UISprite> ();
		m_atkPartySprites [3] = transform.FindChild ("atk_party/btn_atk_party_character_info3").GetComponent<UISprite> ();
		m_atkPartySprites [4] = transform.FindChild ("atk_party/btn_atk_party_character_info4").GetComponent<UISprite> ();
		m_atkPartySprites [5] = transform.FindChild ("atk_party/btn_atk_party_character_info5").GetComponent<UISprite> ();
		m_atkPartySprites [6] = transform.FindChild ("atk_party/btn_atk_party_character_info6").GetComponent<UISprite> ();
		m_atkPartySprites [7] = transform.FindChild ("atk_party/btn_atk_party_character_info7").GetComponent<UISprite> ();
	
		m_defPartyLabels = new UILabel[2];
		m_defPartyLabels[0] = transform.FindChild ("def_party/lbl_party_fighting_power").GetComponent<UILabel> ();
		m_defPartyLabels[1] = transform.FindChild ("def_party/lbl_party_cost").GetComponent<UILabel> ();

		m_defPartySprites = new UISprite[FixedConstantValue.PARTY_MAX_NUM];
		m_defPartySprites [0] = transform.FindChild ("def_party/btn_def_party_character_info0").GetComponent<UISprite> ();
		m_defPartySprites [1] = transform.FindChild ("def_party/btn_def_party_character_info1").GetComponent<UISprite> ();
		m_defPartySprites [2] = transform.FindChild ("def_party/btn_def_party_character_info2").GetComponent<UISprite> ();
		m_defPartySprites [3] = transform.FindChild ("def_party/btn_def_party_character_info3").GetComponent<UISprite> ();
		m_defPartySprites [4] = transform.FindChild ("def_party/btn_def_party_character_info4").GetComponent<UISprite> ();
		m_defPartySprites [5] = transform.FindChild ("def_party/btn_def_party_character_info5").GetComponent<UISprite> ();
		m_defPartySprites [6] = transform.FindChild ("def_party/btn_def_party_character_info6").GetComponent<UISprite> ();
		m_defPartySprites [7] = transform.FindChild ("def_party/btn_def_party_character_info7").GetComponent<UISprite> ();
	}

	void OnEnable() {

		StringBuilder sb = new StringBuilder ();

		User user = GlobalApp.Inst.userData.m_user;

		/* atk party */
		sb.AppendFormat ("Power {0}", user.GetPartyFightingPower(PARTY_TYPE.ShamAtk));
		m_atkPartyLabels [0].text = sb.ToString ();

		sb.Length = 0;
		CharInfo[] charSet = user.GetCharSet (PARTY_TYPE.ShamAtk);
		int cost = 0;
		for (int i = 0; i < FixedConstantValue.PARTY_MAX_NUM; i++) {
			if (charSet [i] != null) {
				cost += charSet [i].cost;
				m_atkPartySprites [i].spriteName = charSet[i].spriteName;
			}
		}
		sb.AppendFormat ("Cost {0}/{1}", cost, user.m_maxPartyCost);
		m_atkPartyLabels [1].text = sb.ToString ();

		/* def party */
		sb.Length = 0;
		sb.AppendFormat ("Power {0}", user.GetPartyFightingPower(PARTY_TYPE.ShamDef));
		m_defPartyLabels [0].text = sb.ToString ();

		sb.Length = 0;
		charSet = user.GetCharSet (PARTY_TYPE.ShamDef);
		cost = user.GetPartyCost (PARTY_TYPE.ShamDef);

		for (int i = 0; i < FixedConstantValue.PARTY_MAX_NUM; i++) {
			if (charSet [i] != null) {
				cost += charSet [i].cost;
				m_defPartySprites [i].spriteName = charSet[i].spriteName;
			}
		}
		sb.AppendFormat ("Cost {0}/{1}", cost, user.m_maxPartyCost);
		m_defPartyLabels [1].text = sb.ToString ();


	}

}
