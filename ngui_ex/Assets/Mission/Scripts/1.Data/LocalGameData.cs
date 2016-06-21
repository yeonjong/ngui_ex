using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/* common data */
public partial class LocalGameData {
	private User[] m_areanaUsers;
	public User[] GetAreanaUsers() {
		Debug.Log ("!!Get areana other user");
		return m_areanaUsers;
	}
	private User[] m_highRankUsers;		//TODO: convert to list?
	public User[] GetHighRankUsers() {
		Debug.Log ("!!Get high rank other user");
		return m_highRankUsers;
	}
	private User[] m_strHighRankUsers;	//TODO: convert to list?
	public User[] GetStrHighRankUsers() {
		Debug.Log ("!!Get str high rank other user");
		return m_strHighRankUsers;
	}

	private List<RecordInfo> recordList;
	public List<RecordInfo> GetRecordList() {
		Debug.Log ("!!Get record list");
		return recordList;
	}
	private List<RecordInfo> strRecordList;
	public List<RecordInfo> GetStrRecordList() {
		Debug.Log ("!!Get str record list");
		return strRecordList;
	}

	public Dictionary<int, RewardInfo> rewardDicByScore = new Dictionary<int, RewardInfo>();

	public LocalGameData() {
		Init ();
	}

	private void Init() {
		InitAreanaUsers ();
		InitHighRankUsers ();
		InitStrHighRankUsers ();
		InitRecord ();
		InitReward ();
	}
}

/* areana user */
public partial class LocalGameData {
	public User[] GetAreanaUsers(bool reset) {// = false) {
		if (reset)
			ResetAreanaUsers ();
		Debug.Log ("!!Get areana other user");
		return m_areanaUsers;
	}

	private void InitAreanaUsers() {
		m_areanaUsers = new User[FixedConstantValue.AREANA_USER_NUM];
		ResetAreanaUsers ();
	}

	// TODO: request data.
	private void ResetAreanaUsers() {
		string[] randomCharacterNames = { "IconHellephant", "IconPlayer", "IconZomBear", "IconZomBunny" };
		 
		for (int i = 0; i < FixedConstantValue.AREANA_USER_NUM; i++) {
			m_areanaUsers [i] = new User (UnityEngine.Random.Range(10000,20000).ToString(), UnityEngine.Random.Range(1,101), UnityEngine.Random.Range(1,21));

			Dictionary<PARTY_TYPE, Party> partyDic = new Dictionary<PARTY_TYPE, Party> ();
			Party areanaDefParty = new Party ();
			for (int j = 0; j < 1; j++) {
				areanaDefParty.m_formList.Add( FormInfo.GetFormInfo (UnityEngine.Random.Range (0, 8)) );
				
				CharInfo[] charSet = new CharInfo[FixedConstantValue.PARTY_MAX_CHAR_NUM];
				for (int k = 0; k < charSet.Length; k++) {
					CharInfo character = new CharInfo(k * 10 + k, randomCharacterNames[UnityEngine.Random.Range(0, 4)], UnityEngine.Random.Range(1, 8), 1);
					charSet [k] = character;
				}
				areanaDefParty.m_charSetList.Add (charSet);
			}

			Party strAreanaDefParty = new Party ();
			for (int j = 0; j < FixedConstantValue.STRONG_ARENA_PARTY_NUM; j++) {
				strAreanaDefParty.m_formList.Add( FormInfo.GetFormInfo (UnityEngine.Random.Range (0, 8)) );

				CharInfo[] charSet = new CharInfo[FixedConstantValue.PARTY_MAX_CHAR_NUM];
				for (int k = 0; k < charSet.Length; k++) {
					CharInfo character = new CharInfo(k * 10 + k, randomCharacterNames[UnityEngine.Random.Range(0, 4)], UnityEngine.Random.Range(1, 8), 1);
					charSet [k] = character;
				}
				strAreanaDefParty.m_charSetList.Add (charSet);
			}

			partyDic.Add (PARTY_TYPE.AreanaDef, areanaDefParty);
			partyDic.Add (PARTY_TYPE.StrAreanaDef, strAreanaDefParty);

			m_areanaUsers [i].SetPartyDic (partyDic);
		}
	}
}

/* high rank user */
public partial class LocalGameData {
	private void InitHighRankUsers() {
		m_highRankUsers = new User[3 + 20]; // + 10
		ResetHighRankUsers ();
	}

	private void ResetHighRankUsers() {
		string[] randomCharacterNames = { "IconHellephant", "IconPlayer", "IconZomBear", "IconZomBunny" };

		for (int i = 0; i < m_highRankUsers.Length; i++) {
			m_highRankUsers [i] = new User (UnityEngine.Random.Range(10000,20000).ToString(), UnityEngine.Random.Range(1,101), UnityEngine.Random.Range(1,21));

			Dictionary<PARTY_TYPE, Party> partyDic = new Dictionary<PARTY_TYPE, Party> ();
			Party areanaDefParty = new Party ();
			for (int j = 0; j < 1; j++) {
				areanaDefParty.m_formList.Add( FormInfo.GetFormInfo (UnityEngine.Random.Range (0, 8)) );

				CharInfo[] charSet = new CharInfo[FixedConstantValue.PARTY_MAX_CHAR_NUM];
				for (int k = 0; k < charSet.Length; k++) {
					CharInfo character = new CharInfo(k * 10 + k, randomCharacterNames[UnityEngine.Random.Range(0, 4)], UnityEngine.Random.Range(1, 8), 1);
					charSet [k] = character;
				}
				areanaDefParty.m_charSetList.Add (charSet);
			}
			partyDic.Add (PARTY_TYPE.AreanaDef, areanaDefParty);

			m_highRankUsers [i].SetPartyDic (partyDic);
		}
	}
}

/* strongest high rank user */
public partial class LocalGameData {
	private void InitStrHighRankUsers() {
		m_strHighRankUsers = new User[3 + 20];
		ResetStrHighRankUsers ();
	}

	private void ResetStrHighRankUsers() {
		string[] randomCharacterNames = { "IconHellephant", "IconPlayer", "IconZomBear", "IconZomBunny" };

		for (int i = 0; i < m_strHighRankUsers.Length; i++) {
			m_strHighRankUsers [i] = new User (UnityEngine.Random.Range(10000,20000).ToString(), UnityEngine.Random.Range(1,101), UnityEngine.Random.Range(1,21));

			Dictionary<PARTY_TYPE, Party> partyDic = new Dictionary<PARTY_TYPE, Party> ();
			Party strAreanaDefParty = new Party ();
			for (int j = 0; j < FixedConstantValue.STRONG_ARENA_PARTY_NUM; j++) {
				strAreanaDefParty.m_formList.Add( FormInfo.GetFormInfo (UnityEngine.Random.Range (0, 8)) );

				CharInfo[] charSet = new CharInfo[FixedConstantValue.PARTY_MAX_CHAR_NUM];
				for (int k = 0; k < charSet.Length; k++) {
					CharInfo character = new CharInfo(k * 10 + k, randomCharacterNames[UnityEngine.Random.Range(0, 4)], UnityEngine.Random.Range(1, 8), 1);
					charSet [k] = character;
				}
				strAreanaDefParty.m_charSetList.Add (charSet);
			}
			partyDic.Add (PARTY_TYPE.StrAreanaDef, strAreanaDefParty);

			m_strHighRankUsers [i].SetPartyDic (partyDic);
		}
	}
}

/* record */
public class RecordInfo {
	public bool m_isWin;
	public bool m_isNew;
	public DateTime m_time;

	public User m_userInfo;
	public User m_otherUserInfo;

	public RecordInfo(bool isWin, bool isNew, DateTime time, User otherUserInfo, User userInfo) {
		m_isWin = isWin;
		m_isNew = isNew;
		m_time = time;
		m_otherUserInfo = otherUserInfo;
		m_userInfo = userInfo;
	}
}
public partial class LocalGameData {
	private void InitRecord() {
		recordList = new List<RecordInfo> ();
		strRecordList = new List<RecordInfo> ();

		string[] randomCharacterNames = { "IconHellephant", "IconPlayer", "IconZomBear", "IconZomBunny" };

		for (int i = 0; i < 6; i++) {
			User user = new User ("jipsa", UnityEngine.Random.Range(1,101), UnityEngine.Random.Range(1,21));
			Dictionary<PARTY_TYPE, Party> partyDic = new Dictionary<PARTY_TYPE, Party> ();
			Party areanaAtkParty = new Party ();
			for (int j = 0; j < 1; j++) {
				areanaAtkParty.m_formList.Add( FormInfo.GetFormInfo (UnityEngine.Random.Range (0, 8)) );

				CharInfo[] charSet = new CharInfo[FixedConstantValue.PARTY_MAX_CHAR_NUM];
				for (int k = 0; k < charSet.Length; k++) {
					CharInfo character = new CharInfo(k * 10 + k, randomCharacterNames[UnityEngine.Random.Range(0, 4)], UnityEngine.Random.Range(1, 8), 1);
					charSet [k] = character;
				}
				areanaAtkParty.m_charSetList.Add (charSet);
			}
			partyDic.Add (PARTY_TYPE.AreanaAtk, areanaAtkParty);
			user.SetPartyDic (partyDic);


			User otheruser = new User (UnityEngine.Random.Range(10000,20000).ToString(), UnityEngine.Random.Range(1,101), UnityEngine.Random.Range(1,21));
			Dictionary<PARTY_TYPE, Party> otherUserPartyDic = new Dictionary<PARTY_TYPE, Party> ();
			Party areanaDefParty = new Party ();
			for (int j = 0; j < 1; j++) {
				areanaDefParty.m_formList.Add( FormInfo.GetFormInfo (UnityEngine.Random.Range (0, 8)) );

				CharInfo[] charSet = new CharInfo[FixedConstantValue.PARTY_MAX_CHAR_NUM];
				for (int k = 0; k < charSet.Length; k++) {
					CharInfo character = new CharInfo(k * 10 + k, randomCharacterNames[UnityEngine.Random.Range(0, 4)], UnityEngine.Random.Range(1, 8), 1);
					charSet [k] = character;
				}
				areanaDefParty.m_charSetList.Add (charSet);
			}
			otherUserPartyDic.Add (PARTY_TYPE.AreanaDef, areanaDefParty);
			otheruser.SetPartyDic (otherUserPartyDic);

			AddRecord (UnityEngine.Random.Range (0, 2) == 1 ? true : false, otheruser, user);
		}

		for (int i = 0; i < 6; i++) {
			User user = new User ("jipsa", UnityEngine.Random.Range(1,101), UnityEngine.Random.Range(1,21));
			Dictionary<PARTY_TYPE, Party> partyDic = new Dictionary<PARTY_TYPE, Party> ();
			Party strAreanaAtkParty = new Party ();
			for (int j = 0; j < FixedConstantValue.STRONG_ARENA_PARTY_NUM; j++) {
				strAreanaAtkParty.m_formList.Add( FormInfo.GetFormInfo (UnityEngine.Random.Range (0, 8)) );

				CharInfo[] charSet = new CharInfo[FixedConstantValue.PARTY_MAX_CHAR_NUM];
				for (int k = 0; k < charSet.Length; k++) {
					CharInfo character = new CharInfo(k * 10 + k, randomCharacterNames[UnityEngine.Random.Range(0, 4)], UnityEngine.Random.Range(1, 8), 1);
					charSet [k] = character;
				}
				strAreanaAtkParty.m_charSetList.Add (charSet);
			}
			partyDic.Add (PARTY_TYPE.StrAreanaAtk, strAreanaAtkParty);
			user.SetPartyDic (partyDic);

			User otheruser = new User (UnityEngine.Random.Range(10000,20000).ToString(), UnityEngine.Random.Range(1,101), UnityEngine.Random.Range(1,21));
			Dictionary<PARTY_TYPE, Party> otherUserPartyDic = new Dictionary<PARTY_TYPE, Party> ();
			Party strAreanaDefParty = new Party ();
			for (int j = 0; j < FixedConstantValue.STRONG_ARENA_PARTY_NUM; j++) {
				strAreanaDefParty.m_formList.Add( FormInfo.GetFormInfo (UnityEngine.Random.Range (0, 8)) );

				CharInfo[] charSet = new CharInfo[FixedConstantValue.PARTY_MAX_CHAR_NUM];
				for (int k = 0; k < charSet.Length; k++) {
					CharInfo character = new CharInfo(k * 10 + k, randomCharacterNames[UnityEngine.Random.Range(0, 4)], UnityEngine.Random.Range(1, 8), 1);
					charSet [k] = character;
				}
				strAreanaDefParty.m_charSetList.Add (charSet);
			}
			otherUserPartyDic.Add (PARTY_TYPE.StrAreanaDef, strAreanaDefParty);
			otheruser.SetPartyDic (otherUserPartyDic);

			AddStrRecord (UnityEngine.Random.Range (0, 2) == 1 ? true : false, otheruser, user);
		}
	}

	public void AddRecord(bool isWin, User otherUserInfo, User userInfo) {
		int deviation = UnityEngine.Random.Range (-10, 0);
		recordList.Add (new RecordInfo (isWin, true, DateTime.Now.AddHours (deviation), otherUserInfo, userInfo));
	}
	public void AddStrRecord(bool isWin, User otherUserInfo, User userInfo) {
		int deviation = UnityEngine.Random.Range (-10, 0);
		strRecordList.Add (new RecordInfo (isWin, true, DateTime.Now.AddHours (deviation), otherUserInfo, userInfo));
	}
}

/* reward */
public class RewardInfo {
	public int m_nRewardScore; //TODO: reward score -> reward rank 808!!
	public ItemInfo[] m_items; // TODO: make item class with inventory

	public RewardInfo(int rewardScore, ItemInfo[] items) {
		m_nRewardScore = rewardScore;
		m_items = items;
	}
}
public class ItemInfo {
	public int id = 0;
	public string name = "Gold";
	public string spriteName = "gold_1";
}
public partial class LocalGameData {
	private void InitReward() {
		for (int i = 1000; i <= FixedConstantValue.REWARD_MAX_SCORE; i += 1000) {
			RewardInfo reward = new RewardInfo (i, new ItemInfo[] {new ItemInfo(), new ItemInfo(), new ItemInfo(), new ItemInfo()});
			rewardDicByScore.Add (reward.m_nRewardScore, reward);
		}
	}
}