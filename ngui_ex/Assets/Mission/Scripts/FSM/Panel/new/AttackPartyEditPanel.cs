﻿using UnityEngine;
using System;
using System.Collections;

public class AttackPartyEditPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
		case "btn_x":
			GuiMgr.GetInst ().PopPnl ();
			break;
		case "btn_start":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaIntroChoreography);
			break;
		case "btn_edit_formation":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.FormationEdit, false);
			break;


		case "btn_formation_info":
			if (GuiMgr.GetInst ().CheckContainsTargetPanel (PANEL_TYPE.AreanaEntrance)) {
				GlobalApp.Inst.SetOtherUser (-1, PANEL_TYPE.AreanaEntrance, PARTY_TYPE.AreanaDef);
			} else if (GuiMgr.GetInst ().CheckContainsTargetPanel (PANEL_TYPE.ShamBattleEntrance)) {
				GlobalApp.Inst.SetOtherUser (-1, PANEL_TYPE.AttackPartyEdit, PARTY_TYPE.ShamDef);
			} else {
				Debug.LogError ("..");
			}
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.FormationInfo, false);
			break;
		case "btn_character_info":
			Debug.LogError ("..");
			break;
		case "btn_character_info_0":
		case "btn_character_info_1":
		case "btn_character_info_2":
		case "btn_character_info_3":
		case "btn_character_info_4":
		case "btn_character_info_5":
		case "btn_character_info_6":
		case "btn_character_info_7":
			if (GuiMgr.GetInst ().CheckContainsTargetPanel (PANEL_TYPE.AreanaEntrance)) {
				GlobalApp.Inst.SetOtherUser (Int32.Parse (btnName.Substring (btnName.Length - 1)), PANEL_TYPE.AttackPartyEdit, PARTY_TYPE.AreanaDef);
			} else if (GuiMgr.GetInst ().CheckContainsTargetPanel (PANEL_TYPE.ShamBattleEntrance)) {
				GlobalApp.Inst.SetOtherUser (Int32.Parse (btnName.Substring (btnName.Length - 1)), PANEL_TYPE.AttackPartyEdit, PARTY_TYPE.ShamDef);
			} else {
				Debug.LogError ("..");
			}
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.CharacterInfo, false);
			break;
		}
	}
	/*
	void Awake() {
		m_labels = new UILabel[5];
		m_labels [0] = transform.FindChild ("character/lbl_character_name").GetComponent<UILabel> ();
		m_labels [1] = transform.FindChild ("character/lbl_character_feature").GetComponent<UILabel> ();
		m_labels [2] = transform.FindChild ("character/lbl_character_cost").GetComponent<UILabel> ();
		m_labels [3] = transform.FindChild ("character/lbl_character_class_kind").GetComponent<UILabel> ();
		m_labels [4] = transform.FindChild ("character/lbl_character_level").GetComponent<UILabel> ();

		m_sprites = new UISprite[3];
		m_sprites [0] = transform.FindChild ("character/spr_character_thumbnail").GetComponent<UISprite> ();
		m_sprites [1] = transform.FindChild ("character/spr_character_upgrade_rank").GetComponent<UISprite> ();
		m_sprites [2] = transform.FindChild ("character/spr_character_star_rank").GetComponent<UISprite> ();
	}

	void OnEnable() {
		CharInfo otherUserCharacter = GlobalApp.Inst.GetOtherUserCharacter ();

		m_labels [0].text = otherUserCharacter.name;
		m_labels [1].text = otherUserCharacter.feature;
		m_labels [2].text = otherUserCharacter.cost.ToString();
		m_labels [3].text = otherUserCharacter.classKind;
		m_labels [4].text = otherUserCharacter.level.ToString();

		m_sprites [0].spriteName = otherUserCharacter.spriteName;
		Color[] colors = new Color[2] { Color.yellow, Color.black };
		m_sprites [1].color = otherUserCharacter.upgradeRank == 0 ? colors[0] : colors[1];
		m_sprites [2].color = otherUserCharacter.starRank == 0 ? colors[0] : colors[1];
	}
*/
}
