using UnityEngine;
using System;
using System.Text;
using System.Collections;

public class AreanaRewardPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
			GuiMgr.GetInst ().PopPnl ();
			break;
		case "btn_item_info_cell_0":
		case "btn_item_info_cell_1":
		case "btn_item_info_cell_2":
		case "btn_item_info_cell_3":
			// TODO: save selected item info.
			Debug.Log ("todo");
			Debug.Log (Int32.Parse (btnName.Substring (btnName.Length - 1)));
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.ItemInfo, false);
			break;
		}
	}

	private UILabel[] m_userInfoLabels;
	private UISprite[] m_userInfoSprites;
	private RewardCell[] m_rewardCells;

	void Awake() {
		m_userInfoLabels = new UILabel[4];
		m_userInfoLabels[0] = transform.FindChild ("user_info/lbl_info1").GetComponent<UILabel> ();
		m_userInfoLabels[1] = transform.FindChild ("user_info_2/lbl_info1").GetComponent<UILabel> ();
		m_userInfoLabels[2] = transform.FindChild ("user_info_2/lbl_info2").GetComponent<UILabel> ();
		m_userInfoLabels[3] = transform.FindChild ("user_info_2/lbl_ranking").GetComponent<UILabel> ();
	
		m_userInfoSprites = new UISprite[2];
		m_userInfoSprites[0] = transform.FindChild ("user_info").GetComponent<UISprite> ();
		m_userInfoSprites[1] = transform.FindChild ("user_info_2/spr_main_character").GetComponent<UISprite> ();
	
		m_rewardCells = new RewardCell[FixedConstantValue.REWARD_CELL_NUM];
		m_rewardCells[0] = transform.FindChild ("reward/scv_reward_scroll_view/wrap_content/btn_item_info_cell_0").GetComponent<RewardCell> ();
		m_rewardCells[1] = transform.FindChild ("reward/scv_reward_scroll_view/wrap_content/btn_item_info_cell_1").GetComponent<RewardCell> ();
		m_rewardCells[2] = transform.FindChild ("reward/scv_reward_scroll_view/wrap_content/btn_item_info_cell_2").GetComponent<RewardCell> ();
		m_rewardCells[3] = transform.FindChild ("reward/scv_reward_scroll_view/wrap_content/btn_item_info_cell_3").GetComponent<RewardCell> ();
	}

	void OnEnable() {
		StringBuilder sb = new StringBuilder ();

		User userInfo = GlobalApp.Inst.userData.m_user;

		sb.AppendFormat ("Rank {0}\nPower {1}", userInfo.m_nAreanaRank, userInfo.GetPartyFightingPower(PARTY_TYPE.AreanaAtk));
		m_userInfoLabels [0].text = sb.ToString ();

		sb.Length = 0;
		sb.AppendFormat ("Level {0} {1}", userInfo.m_nLevel, userInfo.m_nickName);
		m_userInfoLabels [1].text = sb.ToString ();

		sb.Length = 0;
		sb.AppendFormat ("Power {0}\nGuild {1}", userInfo.GetPartyFightingPower(PARTY_TYPE.AreanaAtk), userInfo.m_guildName);
		m_userInfoLabels [2].text = sb.ToString ();

		sb.Length = 0;
		// 1st 2nd 3rd 4th 5th ...
		string postFix;
		switch (userInfo.m_nAreanaRanking) {
		case 1: postFix = "st"; break;
		case 2: postFix = "nd"; break;
		case 3: postFix = "rd"; break;
		default: postFix = "th"; break;
		}
		sb.AppendFormat ("{0}{1}", userInfo.m_nAreanaRanking, postFix);
		m_userInfoLabels [3].text = sb.ToString ();

		m_userInfoSprites [1].spriteName = userInfo.m_mainCharacterName;

		m_rewardCells [0].Set (GlobalApp.Inst.commData.rewardDicByScore[1000]);
		m_rewardCells [1].Set (GlobalApp.Inst.commData.rewardDicByScore[2000]);
		m_rewardCells [2].Set (GlobalApp.Inst.commData.rewardDicByScore[3000]);
		m_rewardCells [3].Set (GlobalApp.Inst.commData.rewardDicByScore[4000]);

	}
}
