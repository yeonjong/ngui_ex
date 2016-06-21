using UnityEngine;
using System;
using System.Text;
using System.Collections;

public class AreanaEntrancePanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		switch (btnName) {
		case "btn_back":
			GameStateMgr.GetInst ().Backward ();
			GuiMgr.GetInst ().PopPnl ();
			break;
		case "btn_other_user_party_info0":
		case "btn_other_user_party_info1":
		case "btn_other_user_party_info2":
		case "btn_other_user_party_info3":
			GlobalApp.Inst.userIndex = Int32.Parse (btnName.Substring (btnName.Length - 1)); //delete.


			int userIndex = Int32.Parse (btnName.Substring (btnName.Length - 1));
			User otherUser = GlobalApp.Inst.commData.GetAreanaUsers() [userIndex];
			Party otherUserAreanaDefParty = otherUser.GetParty (PARTY_TYPE.AreanaDef);
			GlobalApp.Inst.SetCachedParties (otherUserAreanaDefParty);

			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.OtherUserPartyInfo, false);
			break;
		case "btn_attack_party_edit_0":
		case "btn_attack_party_edit_1":
		case "btn_attack_party_edit_2":
		case "btn_attack_party_edit_3":

			int userIndex2 = Int32.Parse (btnName.Substring (btnName.Length - 1));
			User otherUser2 = GlobalApp.Inst.commData.GetAreanaUsers() [userIndex2];
			Party otherUserAreanaDefParty2 = otherUser2.GetParty (PARTY_TYPE.AreanaDef);
			User user2 = GlobalApp.Inst.userData.GetUser();
			Party userAreanaAtkParty2 = user2.GetParty (PARTY_TYPE.AreanaAtk);
			GlobalApp.Inst.SetCachedParties (userAreanaAtkParty2, otherUserAreanaDefParty2);

			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AttackPartyEdit);
			break;
		case "btn_defense_party_edit":

			User user3 = GlobalApp.Inst.userData.GetUser();
			Party userAreanaDefParty3 = user3.GetParty (PARTY_TYPE.AreanaDef);
			GlobalApp.Inst.SetCachedParties (userAreanaDefParty3);

			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.DefensePartyEdit);
			break;
		case "btn_reset":
			Reset ();
			break;
		case "btn_ranking":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaRanking);
			break;
		case "btn_record":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaRecord);
			break;
		case "btn_market":
			// TODO: market
			Debug.Log ("todo");
			break;
		case "btn_reward":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaReward);
			break;
		case "btn_help":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaHelp);
			break;
		case "btn_cumulative":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaCumulative);
			break;
		}
	}

	private UILabel m_userInfo;
	private UISprite m_userMainCharacter;

	private UILabel[] m_otherUserInfos;
	private UISprite[] m_otherUserMainCharacters;

	private UILabel m_areanaKey;

	void Awake() {
		m_userInfo = transform.FindChild ("user_info/lbl_info1").GetComponent<UILabel> ();
		m_userMainCharacter = transform.FindChild ("user_info").GetComponent<UISprite> ();

		m_otherUserInfos = new UILabel[FixedConstantValue.AREANA_USER_NUM * 2];
		m_otherUserInfos[0] = transform.FindChild ("other_user_info1/lbl_info1").GetComponent<UILabel> ();
		m_otherUserInfos[1] = transform.FindChild ("other_user_info1/lbl_info2").GetComponent<UILabel> ();
		m_otherUserInfos[2] = transform.FindChild ("other_user_info2/lbl_info1").GetComponent<UILabel> ();
		m_otherUserInfos[3] = transform.FindChild ("other_user_info2/lbl_info2").GetComponent<UILabel> ();
		m_otherUserInfos[4] = transform.FindChild ("other_user_info3/lbl_info1").GetComponent<UILabel> ();
		m_otherUserInfos[5] = transform.FindChild ("other_user_info3/lbl_info2").GetComponent<UILabel> ();
		m_otherUserInfos[6] = transform.FindChild ("other_user_info4/lbl_info1").GetComponent<UILabel> ();
		m_otherUserInfos[7] = transform.FindChild ("other_user_info4/lbl_info2").GetComponent<UILabel> ();

		m_otherUserMainCharacters = new UISprite[FixedConstantValue.AREANA_USER_NUM];
		m_otherUserMainCharacters [0] = transform.FindChild ("other_user_info1/btn_other_user_party_info0").GetComponent<UISprite> ();
		m_otherUserMainCharacters [1] = transform.FindChild ("other_user_info2/btn_other_user_party_info1").GetComponent<UISprite> ();
		m_otherUserMainCharacters [2] = transform.FindChild ("other_user_info3/btn_other_user_party_info2").GetComponent<UISprite> ();
		m_otherUserMainCharacters [3] = transform.FindChild ("other_user_info4/btn_other_user_party_info3").GetComponent<UISprite> ();
	
		m_areanaKey = transform.FindChild ("lbl_areana_key").GetComponent<UILabel>();
	}

	void OnEnable() {
		Debug.Log ("OnEnable");
		StringBuilder sb = new StringBuilder ();
		//sb.Length = 0;

		User userInfo = GlobalApp.Inst.userData.GetUser();
		sb.AppendFormat ("Rank {0}\nPower {1}", userInfo.m_nAreanaRank, userInfo.GetPartyFightingPower(PARTY_TYPE.AreanaAtk));
		m_userInfo.text = sb.ToString ();

		if (userInfo.GetCharSet (PARTY_TYPE.AreanaAtk) == null)
			Debug.Log ("null");
		else
			Debug.Log ("not null");
		m_userMainCharacter.spriteName = userInfo.GetCharSet (PARTY_TYPE.AreanaAtk)[0].spriteName;

		Reset ();
	}

	void Reset() {
		StringBuilder sb = new StringBuilder ();

		User[] otherUserInfos = GlobalApp.Inst.commData.GetAreanaUsers(true);
		for (int i = 0; i < FixedConstantValue.AREANA_USER_NUM; i++) {
			sb.Length = 0;
			sb.AppendFormat ("Rank {0}\nLevel {1} {2}",
				otherUserInfos[i].m_nAreanaRank,
				otherUserInfos[i].m_nLevel,
				otherUserInfos[i].m_nickName);
			m_otherUserInfos [i * 2].text = sb.ToString ();
			sb.Length = 0;
			sb.AppendFormat ("Power {0}",
				otherUserInfos [i].GetPartyFightingPower (PARTY_TYPE.AreanaDef));
			m_otherUserInfos [i * 2 + 1].text = sb.ToString ();
			m_otherUserMainCharacters [i].spriteName = otherUserInfos [i].GetCharSet (PARTY_TYPE.AreanaDef)[0].spriteName;
		}

		Key key = GlobalApp.Inst.userData.m_key;
		sb.Length = 0;
		sb.AppendFormat ("{0} / {1}", key.m_nAreanaKey, key.m_nMaxAreanaKey);
		m_areanaKey.text = sb.ToString ();
	}

}
