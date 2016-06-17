using UnityEngine;
using System;
using System.Text;
using System.Collections;

public class AreanaRankingPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
			GuiMgr.GetInst ().PopPnl ();
			break;

		case "btn_other_user_party_info_1":
		case "btn_other_user_party_info_2":
		case "btn_other_user_party_info_3":
			GlobalApp.Inst.userIndex = Int32.Parse (btnName.Substring (btnName.Length - 1)) -1;
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.OtherUserPartyInfo, false);
			break;
		}
	}

	private UILabel m_userInfo;
	private UISprite m_userMainCharacter;

	private UILabel[] m_labels;
	private UISprite[] m_sprites;
	private CharacterCell[] m_charCells;

	void Awake() {
		m_userInfo = transform.FindChild ("user_info/lbl_info1").GetComponent<UILabel> ();
		m_userMainCharacter = transform.FindChild ("user_info").GetComponent<UISprite> ();

		m_labels = new UILabel[14];
		m_labels [0] = transform.FindChild ("other_user_info_1/lbl_info1").GetComponent<UILabel> ();
		m_labels [1] = transform.FindChild ("other_user_info_1/lbl_info2").GetComponent<UILabel> ();
		m_labels [2] = transform.FindChild ("other_user_info_2/lbl_info1").GetComponent<UILabel> ();
		m_labels [3] = transform.FindChild ("other_user_info_2/lbl_info2").GetComponent<UILabel> ();
		m_labels [4] = transform.FindChild ("other_user_info_3/lbl_info1").GetComponent<UILabel> ();
		m_labels [5] = transform.FindChild ("other_user_info_3/lbl_info2").GetComponent<UILabel> ();

		m_labels [6] = transform.FindChild ("other_user_info_x/scv_oui_scroll_view/wrap_content/spr_cell_0/lbl_info_1").GetComponent<UILabel> ();
		m_labels [7] = transform.FindChild ("other_user_info_x/scv_oui_scroll_view/wrap_content/spr_cell_0/lbl_info_2").GetComponent<UILabel> ();
		m_labels [8] = transform.FindChild ("other_user_info_x/scv_oui_scroll_view/wrap_content/spr_cell_1/lbl_info_1").GetComponent<UILabel> ();
		m_labels [9] = transform.FindChild ("other_user_info_x/scv_oui_scroll_view/wrap_content/spr_cell_1/lbl_info_2").GetComponent<UILabel> ();
		m_labels [10] = transform.FindChild ("other_user_info_x/scv_oui_scroll_view/wrap_content/spr_cell_2/lbl_info_1").GetComponent<UILabel> ();
		m_labels [11] = transform.FindChild ("other_user_info_x/scv_oui_scroll_view/wrap_content/spr_cell_2/lbl_info_2").GetComponent<UILabel> ();
		m_labels [12] = transform.FindChild ("other_user_info_x/scv_oui_scroll_view/wrap_content/spr_cell_3/lbl_info_1").GetComponent<UILabel> ();
		m_labels [13] = transform.FindChild ("other_user_info_x/scv_oui_scroll_view/wrap_content/spr_cell_3/lbl_info_2").GetComponent<UILabel> ();

		m_sprites = new UISprite[7];//3];
		m_sprites [0] = transform.FindChild ("other_user_info_1/btn_other_user_party_info_1").GetComponent<UISprite> ();
		m_sprites [1] = transform.FindChild ("other_user_info_2/btn_other_user_party_info_2").GetComponent<UISprite> ();
		m_sprites [2] = transform.FindChild ("other_user_info_3/btn_other_user_party_info_3").GetComponent<UISprite> ();
		m_sprites [3] = transform.FindChild ("other_user_info_x/scv_oui_scroll_view/wrap_content/spr_cell_0/spr_character/spr_character_thumbnail").GetComponent<UISprite> ();
		m_sprites [4] = transform.FindChild ("other_user_info_x/scv_oui_scroll_view/wrap_content/spr_cell_1/spr_character/spr_character_thumbnail").GetComponent<UISprite> ();
		m_sprites [5] = transform.FindChild ("other_user_info_x/scv_oui_scroll_view/wrap_content/spr_cell_2/spr_character/spr_character_thumbnail").GetComponent<UISprite> ();
		m_sprites [6] = transform.FindChild ("other_user_info_x/scv_oui_scroll_view/wrap_content/spr_cell_3/spr_character/spr_character_thumbnail").GetComponent<UISprite> ();


		m_charCells = new CharacterCell[4];
		m_charCells [0] = transform.FindChild ("other_user_info_x/scv_oui_scroll_view/wrap_content/spr_cell_0/spr_character").GetComponent<CharacterCell> ();
		m_charCells [1] = transform.FindChild ("other_user_info_x/scv_oui_scroll_view/wrap_content/spr_cell_1/spr_character").GetComponent<CharacterCell> ();
		m_charCells [2] = transform.FindChild ("other_user_info_x/scv_oui_scroll_view/wrap_content/spr_cell_2/spr_character").GetComponent<CharacterCell> ();
		m_charCells [3] = transform.FindChild ("other_user_info_x/scv_oui_scroll_view/wrap_content/spr_cell_3/spr_character").GetComponent<CharacterCell> ();
	}

	void OnEnable() {
		StringBuilder sb = new StringBuilder ();

		User userInfo = GlobalApp.Inst.userData.m_user;
		sb.AppendFormat ("Rank {0}\nPower {1}", userInfo.m_nAreanaRank, userInfo.GetPartyFightingPower(PARTY_TYPE.AreanaAtk));
		m_userInfo.text = sb.ToString ();
		m_userMainCharacter.spriteName = userInfo.m_mainCharacterName;

		User[] users = GlobalApp.Inst.commData.m_highRankUsers;
		for (int i = 0; i < 3 * 2; i+=2) {
			sb.Length = 0;
			sb.AppendFormat ("Level {0} {1}", users [i/2].m_nLevel, users [i/2].m_nickName);
			m_labels [i].text = sb.ToString ();
			sb.Length = 0;
			sb.AppendFormat ("Power {0}", users[i/2].GetPartyFightingPower(PARTY_TYPE.AreanaDef));
			m_labels [i + 1].text = sb.ToString ();
			m_sprites [i / 2].spriteName = users[i/2].m_mainCharacterName;
		}

		for (int i = 6; i < 14; i += 2) {
			sb.Length = 0;
			sb.AppendFormat ("Rank {0}", users[i/2].m_nAreanaRank);
			m_labels [i].text = sb.ToString ();
			sb.Length = 0;
			sb.AppendFormat ("Power {0}\nGuild {1}", users[i/2].GetPartyFightingPower(PARTY_TYPE.AreanaDef), users[i/2].m_guildName);
			m_labels [i + 1].text = sb.ToString ();
			m_sprites [i / 2].spriteName = users [i / 2].m_mainCharacterName;
		}
	}


	//TODO: add 100 items!


}
