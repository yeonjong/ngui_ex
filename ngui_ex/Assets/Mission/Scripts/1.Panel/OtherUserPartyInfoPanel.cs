using UnityEngine;
using System;
using System.Text;
using System.Collections;

public class OtherUserPartyInfoPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		switch (btnName) {
		case "btn_back":
		case "spr_modal":
			GuiMgr.GetInst ().PopPnl ();
			break;
		case "btn_ok":
			//TODO: save info
			Debug.Log ("todo");

			GuiMgr.GetInst ().PopPnl ();
			break;
		case "btn_formation_info":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.FormationInfo, false);
			break;
		case "btn_character_info0":
		case "btn_character_info1":
		case "btn_character_info2":
		case "btn_character_info3":
		case "btn_character_info4":
		case "btn_character_info5":
		case "btn_character_info6":
		case "btn_character_info7":
			int index = Int32.Parse (btnName.Substring (btnName.Length - 1));
			if (!m_otherTeamFormation [index].spriteName.Equals (FixedConstantValue.EMPTY_SPRITE_NAME)) {
				GlobalApp.Inst.charIndex = Int32.Parse (btnName.Substring (btnName.Length - 1));
				
				GlobalApp.Inst.SetBtnIndex (Int32.Parse (btnName.Substring (btnName.Length - 1)));
				
				GuiMgr.GetInst ().PushPnl (PANEL_TYPE.CharacterInfo, false);
			}
			break;
		}
	}
		
	private UILabel m_otherTeamFightingPower;
	private UISprite[] m_otherTeamFormation;

	void Awake() {
		m_otherTeamFightingPower = transform.FindChild ("lbl_other_team_fighting_power").GetComponent<UILabel> ();
		
		m_otherTeamFormation = new UISprite[FixedConstantValue.PARTY_MAX_CHAR_NUM];
		m_otherTeamFormation [0] = transform.FindChild ("btn_character_info0/spr_thumbnail").GetComponent<UISprite> ();
		m_otherTeamFormation [1] = transform.FindChild ("btn_character_info1/spr_thumbnail").GetComponent<UISprite> ();
		m_otherTeamFormation [2] = transform.FindChild ("btn_character_info2/spr_thumbnail").GetComponent<UISprite> ();
		m_otherTeamFormation [3] = transform.FindChild ("btn_character_info3/spr_thumbnail").GetComponent<UISprite> ();
		m_otherTeamFormation [4] = transform.FindChild ("btn_character_info4/spr_thumbnail").GetComponent<UISprite> ();
		m_otherTeamFormation [5] = transform.FindChild ("btn_character_info5/spr_thumbnail").GetComponent<UISprite> ();
		m_otherTeamFormation [6] = transform.FindChild ("btn_character_info6/spr_thumbnail").GetComponent<UISprite> ();
		m_otherTeamFormation [7] = transform.FindChild ("btn_character_info7/spr_thumbnail").GetComponent<UISprite> ();
	}

	void OnEnable() {
		Debug.Log ("OnEnable");

		Debug.Log (GlobalApp.Inst.userIndex);

		StringBuilder sb = new StringBuilder ();

		//User otherUser;
		/*
		if (GuiMgr.GetInst ().CheckContainsTargetPanel (PANEL_TYPE.AreanaRanking))
			otherUser = GlobalApp.Inst.GetUserAtSpecialCase ("AreanaEntrancePanel/AreanaRankingPanel/OtherUserPartyInfoPanel");
		else
			otherUser = GlobalApp.Inst.GetUserAtSpecialCase ("AreanaEntrancePanel/OtherUserPartyInfoPanel");
		*/
		Party party = GlobalApp.Inst.GetCachedParties ()[0];
		//CharInfo character = party.m_charSetList [0] [charIndex];
		//party.get

		int fightingPower = 0;
		CharInfo[] charSet = party.m_charSetList[0];
		for (int i = 0; i < charSet.Length; i++) {
			if (charSet[i] != null)
				fightingPower += charSet [i].fightingPower;
		}

		sb.AppendFormat ("Power {0}", fightingPower);
		m_otherTeamFightingPower.text = sb.ToString ();

		//CharInfo[] charSet = otherUser.GetCharSet (PARTY_TYPE.AreanaDef);
		for (int i = 0; i < m_otherTeamFormation.Length; i++) {
			if (charSet [i] != null) {
				m_otherTeamFormation [i].spriteName = charSet [i].spriteName;
			} else
				m_otherTeamFormation [i].spriteName = "btn_9slice_pressed";
		}
	}

}