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
			GlobalApp.Inst.SetCachedParties (shamAtk);
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
			GlobalApp.Inst.SetCachedParties (shamAtk);
			GlobalApp.Inst.SetBtnIndex (Int32.Parse (btnName.Substring (btnName.Length - 1)));
			//charIndex = Int32.Parse (btnName.Substring (btnName.Length - 1));
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.CharacterInfo, false);
			break;

		case "btn_def_party_formation_info":
			GlobalApp.Inst.SetCachedParties (shamDef);
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
			GlobalApp.Inst.SetCachedParties (shamDef);
			GlobalApp.Inst.SetBtnIndex (Int32.Parse (btnName.Substring (btnName.Length - 1)));
			//GlobalApp.Inst.charIndex = Int32.Parse (btnName.Substring (btnName.Length - 1));
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

		m_atkPartySprites = new UISprite[FixedConstantValue.PARTY_MAX_CHAR_NUM];
		m_atkPartySprites [0] = transform.FindChild ("atk_party/btn_atk_party_character_info0/spr_thumbnail").GetComponent<UISprite> ();
		m_atkPartySprites [1] = transform.FindChild ("atk_party/btn_atk_party_character_info1/spr_thumbnail").GetComponent<UISprite> ();
		m_atkPartySprites [2] = transform.FindChild ("atk_party/btn_atk_party_character_info2/spr_thumbnail").GetComponent<UISprite> ();
		m_atkPartySprites [3] = transform.FindChild ("atk_party/btn_atk_party_character_info3/spr_thumbnail").GetComponent<UISprite> ();
		m_atkPartySprites [4] = transform.FindChild ("atk_party/btn_atk_party_character_info4/spr_thumbnail").GetComponent<UISprite> ();
		m_atkPartySprites [5] = transform.FindChild ("atk_party/btn_atk_party_character_info5/spr_thumbnail").GetComponent<UISprite> ();
		m_atkPartySprites [6] = transform.FindChild ("atk_party/btn_atk_party_character_info6/spr_thumbnail").GetComponent<UISprite> ();
		m_atkPartySprites [7] = transform.FindChild ("atk_party/btn_atk_party_character_info7/spr_thumbnail").GetComponent<UISprite> ();
	
		m_defPartyLabels = new UILabel[2];
		m_defPartyLabels[0] = transform.FindChild ("def_party/lbl_party_fighting_power").GetComponent<UILabel> ();
		m_defPartyLabels[1] = transform.FindChild ("def_party/lbl_party_cost").GetComponent<UILabel> ();

		m_defPartySprites = new UISprite[FixedConstantValue.PARTY_MAX_CHAR_NUM];
		m_defPartySprites [0] = transform.FindChild ("def_party/btn_def_party_character_info0/spr_thumbnail").GetComponent<UISprite> ();
		m_defPartySprites [1] = transform.FindChild ("def_party/btn_def_party_character_info1/spr_thumbnail").GetComponent<UISprite> ();
		m_defPartySprites [2] = transform.FindChild ("def_party/btn_def_party_character_info2/spr_thumbnail").GetComponent<UISprite> ();
		m_defPartySprites [3] = transform.FindChild ("def_party/btn_def_party_character_info3/spr_thumbnail").GetComponent<UISprite> ();
		m_defPartySprites [4] = transform.FindChild ("def_party/btn_def_party_character_info4/spr_thumbnail").GetComponent<UISprite> ();
		m_defPartySprites [5] = transform.FindChild ("def_party/btn_def_party_character_info5/spr_thumbnail").GetComponent<UISprite> ();
		m_defPartySprites [6] = transform.FindChild ("def_party/btn_def_party_character_info6/spr_thumbnail").GetComponent<UISprite> ();
		m_defPartySprites [7] = transform.FindChild ("def_party/btn_def_party_character_info7/spr_thumbnail").GetComponent<UISprite> ();
	}

	Party shamAtk;
	Party shamDef;
	void OnEnable() {
		User user = GlobalApp.Inst.userData.m_user;
		shamAtk = user.GetParty (PARTY_TYPE.ShamAtk);
		shamDef = user.GetParty (PARTY_TYPE.ShamAtk);

		/* atk party */
		CharInfo[] charSet = shamAtk.m_charSetList [0];
		int cost = 0;
		int fightingPower = 0;
		for (int i = 0; i < FixedConstantValue.PARTY_MAX_CHAR_NUM; i++) {
			if (charSet [i] != null) {
				cost += charSet [i].cost;
				fightingPower += charSet [i].fightingPower;
				m_atkPartySprites [i].spriteName = charSet [i].spriteName;
			} else {
				m_atkPartySprites [i].spriteName = FixedConstantValue.EMPTY_CHAR_SPRITE_NAME;
			}
		}
		StringBuilder sb = new StringBuilder ();
		sb.AppendFormat ("Power {0}", fightingPower);
		m_atkPartyLabels [0].text = sb.ToString ();
		sb.Length = 0;
		sb.AppendFormat ("Cost {0}/{1}", cost, user.m_maxPartyCost);
		m_atkPartyLabels [1].text = sb.ToString ();


		/* def party */
		charSet = shamDef.m_charSetList [0];
		cost = 0;
		fightingPower = 0;
		for (int i = 0; i < FixedConstantValue.PARTY_MAX_CHAR_NUM; i++) {
			if (charSet [i] != null) {
				cost += charSet [i].cost;
				fightingPower += charSet [i].fightingPower;
				m_defPartySprites [i].spriteName = charSet [i].spriteName;
			} else {
				m_defPartySprites [i].spriteName = FixedConstantValue.EMPTY_CHAR_SPRITE_NAME;
			}
		}
		sb.Length = 0;
		sb.AppendFormat ("Power {0}", fightingPower);
		m_defPartyLabels [0].text = sb.ToString ();
		sb.Length = 0;
		sb.AppendFormat ("Cost {0}/{1}", cost, user.m_maxPartyCost);
		m_defPartyLabels [1].text = sb.ToString ();
	}

}
