using UnityEngine;
using System.Text;
using System.Collections;

public class AreanaRecordReviewCheckPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
		case "spr_modal":
			GuiMgr.GetInst ().PopPnl ();
			break;
		case "btn_review":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaBattle);
			break;
		}
	}

	private UILabel userInfoLabel;
	private UISprite[] userPartySprites;

	private UILabel otherUserInfoLabel;
	private UISprite[] otherUserPartySprites;

	void Awake() {
		userInfoLabel = transform.FindChild ("lbl_user_info").GetComponent<UILabel> ();

		userPartySprites = new UISprite[FixedConstantValue.PARTY_MAX_CHAR_NUM];
		userPartySprites [0] = transform.FindChild ("user_party/spr_user_char_0").GetComponent<UISprite> ();
		userPartySprites [1] = transform.FindChild ("user_party/spr_user_char_1").GetComponent<UISprite> ();
		userPartySprites [2] = transform.FindChild ("user_party/spr_user_char_2").GetComponent<UISprite> ();
		userPartySprites [3] = transform.FindChild ("user_party/spr_user_char_3").GetComponent<UISprite> ();
		userPartySprites [4] = transform.FindChild ("user_party/spr_user_char_4").GetComponent<UISprite> ();
		userPartySprites [5] = transform.FindChild ("user_party/spr_user_char_5").GetComponent<UISprite> ();
		userPartySprites [6] = transform.FindChild ("user_party/spr_user_char_6").GetComponent<UISprite> ();
		userPartySprites [7] = transform.FindChild ("user_party/spr_user_char_7").GetComponent<UISprite> ();

		otherUserInfoLabel = transform.FindChild ("lbl_other_user_info").GetComponent<UILabel> ();

		otherUserPartySprites = new UISprite[FixedConstantValue.PARTY_MAX_CHAR_NUM];
		otherUserPartySprites [0] = transform.FindChild ("other_user_party/spr_user_char_0").GetComponent<UISprite> ();
		otherUserPartySprites [1] = transform.FindChild ("other_user_party/spr_user_char_1").GetComponent<UISprite> ();
		otherUserPartySprites [2] = transform.FindChild ("other_user_party/spr_user_char_2").GetComponent<UISprite> ();
		otherUserPartySprites [3] = transform.FindChild ("other_user_party/spr_user_char_3").GetComponent<UISprite> ();
		otherUserPartySprites [4] = transform.FindChild ("other_user_party/spr_user_char_4").GetComponent<UISprite> ();
		otherUserPartySprites [5] = transform.FindChild ("other_user_party/spr_user_char_5").GetComponent<UISprite> ();
		otherUserPartySprites [6] = transform.FindChild ("other_user_party/spr_user_char_6").GetComponent<UISprite> ();
		otherUserPartySprites [7] = transform.FindChild ("other_user_party/spr_user_char_7").GetComponent<UISprite> ();
	}

	void OnEnable() {
		StringBuilder sb = new StringBuilder ();

		RecordInfo record = GlobalApp.Inst.commData.GetRecordList()[0]; // TODO: get real index from pnl_areana_record!!!

		User user = record.m_userInfo;
		sb.AppendFormat ("Level {0} {1}", user.m_nLevel, user.m_nickName);
		userInfoLabel.text = sb.ToString ();


		CharInfo[] charSet = user.GetCharSet (PARTY_TYPE.AreanaAtk);
		for (int i = 0; i < FixedConstantValue.PARTY_MAX_CHAR_NUM; i++) {
			if (charSet [i] != null)
				userPartySprites [i].spriteName = charSet [i].spriteName;
			else {
				userPartySprites [i].spriteName = "UISliderBG"; // TODO: please empty sprite name enter;
			}
		}

		user = record.m_otherUserInfo;
		sb.Length = 0;
		sb.AppendFormat ("Level {0} {1}", user.m_nLevel, user.m_nickName);
		otherUserInfoLabel.text = sb.ToString ();
		charSet = user.GetCharSet (PARTY_TYPE.AreanaDef);
		for (int i = 0; i < FixedConstantValue.PARTY_MAX_CHAR_NUM; i++) {
			if (charSet [i] != null)
				otherUserPartySprites [i].spriteName = charSet [i].spriteName;
			else {
				otherUserPartySprites [i].spriteName = "UISliderBG"; // TODO: please empty sprite name enter;
			}
		}






	}

}
