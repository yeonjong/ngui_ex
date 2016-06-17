using UnityEngine;
using System;
using System.Text;
using System.Collections;

public class RecordCell : MonoBehaviour {

	UISprite[] m_sprites; //0: areana result, 1: other user main character
	UILabel[] m_labels; //0: other user info, 1: new, 2: time

	void Awake() {
		m_sprites = new UISprite[2];
		m_sprites [0] = transform.FindChild ("spr_areana_result").GetComponent<UISprite> ();
		m_sprites [1] = transform.FindChild ("spr_other_user_main_character").GetComponent<UISprite> ();

		m_labels = new UILabel[3];
		m_labels [0] = transform.FindChild ("lbl_other_user_info").GetComponent<UILabel> ();
		m_labels [1] = transform.FindChild ("lbl_new").GetComponent<UILabel> ();
		m_labels [2] = transform.FindChild ("lbl_time").GetComponent<UILabel> ();
	}

	public void Set(RecordInfo recordInfo) {
		if (m_sprites == null)
			Awake ();

		m_sprites [0].spriteName = recordInfo.m_isWin ? "UISliderHandle" : "Unity - Check Mark";
		m_sprites [1].spriteName = recordInfo.m_otherUserInfo.m_mainCharacterName;

		StringBuilder sb = new StringBuilder ();
		sb.AppendFormat ("Level {0} {1}\nGuild {2}\nPower {3}",
			recordInfo.m_otherUserInfo.m_nLevel,
			recordInfo.m_otherUserInfo.m_nickName,
			recordInfo.m_otherUserInfo.m_guildName,
			recordInfo.m_otherUserInfo.GetPartyFightingPower(PARTY_TYPE.AreanaDef));
		m_labels [0].text = sb.ToString ();

		m_labels[1].text = recordInfo.m_isNew ? "NEW" : "";

		double difHour = DateTime.Now.Subtract (recordInfo.m_time).TotalHours;
		sb.Length = 0;
		sb.AppendFormat ("{0} hours ago", (int)difHour);
		m_labels [2].text = sb.ToString ();
	}

}
