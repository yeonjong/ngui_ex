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
			GlobalApp.Inst.selectedOtherUserChar = Int32.Parse (btnName.Substring (btnName.Length - 1));
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.CharacterInfo, false);
			break;
		}
	}
		
	private UILabel m_otherTeamFightingPower;
	private UISprite[] m_otherTeamFormation;

	void Awake() {
		m_otherTeamFightingPower = transform.FindChild ("lbl_other_team_fighting_power").GetComponent<UILabel> ();
	
		m_otherTeamFormation = new UISprite[FixedConstantValue.PARTY_MAX_NUM];
		m_otherTeamFormation [0] = transform.FindChild ("btn_character_info0").GetComponent<UISprite> ();
		m_otherTeamFormation [1] = transform.FindChild ("btn_character_info1").GetComponent<UISprite> ();
		m_otherTeamFormation [2] = transform.FindChild ("btn_character_info2").GetComponent<UISprite> ();
		m_otherTeamFormation [3] = transform.FindChild ("btn_character_info3").GetComponent<UISprite> ();
		m_otherTeamFormation [4] = transform.FindChild ("btn_character_info4").GetComponent<UISprite> ();
		m_otherTeamFormation [5] = transform.FindChild ("btn_character_info5").GetComponent<UISprite> ();
		m_otherTeamFormation [6] = transform.FindChild ("btn_character_info6").GetComponent<UISprite> ();
		m_otherTeamFormation [7] = transform.FindChild ("btn_character_info7").GetComponent<UISprite> ();
	}

	void OnEnable() {
		Debug.Log ("OnEnable");

		StringBuilder sb = new StringBuilder ();

		User otherUser = GlobalApp.Inst.GetOtherUser ();
		sb.AppendFormat ("Power {0}", otherUser.GetPartyFightingPower(PARTY_TYPE.AreanaDef));
		m_otherTeamFightingPower.text = sb.ToString ();

		CharInfo[] charSet = otherUser.GetCharSet (PARTY_TYPE.AreanaDef);
		for (int i = 0; i < m_otherTeamFormation.Length; i++) {
			if (charSet[i] != null)
				m_otherTeamFormation [i].spriteName = charSet[i].spriteName;
		}
	}

}