using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/* common data */
public partial class LocalGameData {
	public LocalGameData() {
		Init ();
	}

	private void Init() {
		InitOtherUser ();
		InitHighRankUser ();
		InitStrHighRankUser ();
		InitRecord ();
		InitReward ();
	}
}

/* other_user_info */
public partial class LocalGameData {
	private const int OTHER_USER_NUM = 4;

	private User[] m_otherUsers;
	public User[] GetOtherUsers(bool reset = false) {
		if (reset)
			ResetOtherUsers ();
		return m_otherUsers;
	}

	private void InitOtherUser() {
		m_otherUsers = new User[OTHER_USER_NUM];
		ResetOtherUsers ();
	}

	// TODO: request data.
	private void ResetOtherUsers() {
		string[] randomCharacterNames = { "IconHellephant", "IconPlayer", "IconZomBear", "IconZomBunny" };

		for (int i = 0; i < OTHER_USER_NUM; i++) {
			m_otherUsers [i] = new User (UnityEngine.Random.Range(10000,20000).ToString(), UnityEngine.Random.Range(1,101), UnityEngine.Random.Range(1,21), randomCharacterNames[UnityEngine.Random.Range(0,4)]);

			Dictionary<PARTY_TYPE, Party> partyDic = new Dictionary<PARTY_TYPE, Party> ();
			Party areanaDefParty = new Party ();
			for (int j = 0; j < 1; j++) {
				areanaDefParty.m_formList.Add( FormInfo.GetFormInfo (UnityEngine.Random.Range (1, 9)) );
				
				CharInfo[] charSet = new CharInfo[FixedConstantValue.PARTY_MAX_NUM];
				for (int k = 0; k < charSet.Length; k++) {
					CharInfo character = new CharInfo(k * 10 + k, randomCharacterNames[UnityEngine.Random.Range(0, 4)], UnityEngine.Random.Range(1, 8), 1);
					charSet [k] = character;
				}
				areanaDefParty.m_charSetList.Add (charSet);
			}

			Party strAreanaDefParty = new Party ();
			for (int j = 0; j < FixedConstantValue.STRONG_ARENA_PARTY_NUM; j++) {
				strAreanaDefParty.m_formList.Add( FormInfo.GetFormInfo (UnityEngine.Random.Range (1, 9)) );

				CharInfo[] charSet = new CharInfo[FixedConstantValue.PARTY_MAX_NUM];
				for (int k = 0; k < charSet.Length; k++) {
					CharInfo character = new CharInfo(k * 10 + k, randomCharacterNames[UnityEngine.Random.Range(0, 4)], UnityEngine.Random.Range(1, 8), 1);
					charSet [k] = character;
				}
				strAreanaDefParty.m_charSetList.Add (charSet);
			}

			partyDic.Add (PARTY_TYPE.AreanaDef, areanaDefParty);
			partyDic.Add (PARTY_TYPE.StrAreanaDef, strAreanaDefParty);

			m_otherUsers [i].SetPartyDic (partyDic);
		}
	}

	/* high rank */
	private User[] m_highRankUsers;
	private User[] m_strHighRankUsers;
	public User[] GetHighRankUsers(bool reset = false) {
		if (reset)
			ResetHighRankUser ();
		return m_highRankUsers;
	}
	public User[] GetStrHighRankUsers(bool reset = false) {
		if (reset)
			ResetStrHighRankUser ();
		return m_strHighRankUsers;
	}

	private void InitHighRankUser() {
		m_highRankUsers = new User[3 + 20]; // + 10
		ResetHighRankUser ();
	}
	private void InitStrHighRankUser() {
		m_strHighRankUsers = new User[3 + 20];
		ResetStrHighRankUser ();
	}

	private void ResetHighRankUser() {
		string[] randomCharacterNames = { "IconHellephant", "IconPlayer", "IconZomBear", "IconZomBunny" };

		for (int i = 0; i < m_highRankUsers.Length; i++) {
			m_highRankUsers [i] = new User (UnityEngine.Random.Range(10000,20000).ToString(), UnityEngine.Random.Range(1,101), UnityEngine.Random.Range(1,21), randomCharacterNames[UnityEngine.Random.Range(0,4)]);

			Dictionary<PARTY_TYPE, Party> partyDic = new Dictionary<PARTY_TYPE, Party> ();
			Party areanaDefParty = new Party ();
			for (int j = 0; j < 1; j++) {
				areanaDefParty.m_formList.Add( FormInfo.GetFormInfo (UnityEngine.Random.Range (1, 9)) );

				CharInfo[] charSet = new CharInfo[FixedConstantValue.PARTY_MAX_NUM];
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
	private void ResetStrHighRankUser() {
		string[] randomCharacterNames = { "IconHellephant", "IconPlayer", "IconZomBear", "IconZomBunny" };

		for (int i = 0; i < m_strHighRankUsers.Length; i++) {
			m_strHighRankUsers [i] = new User (UnityEngine.Random.Range(10000,20000).ToString(), UnityEngine.Random.Range(1,101), UnityEngine.Random.Range(1,21), randomCharacterNames[UnityEngine.Random.Range(0,4)]);

			Dictionary<PARTY_TYPE, Party> partyDic = new Dictionary<PARTY_TYPE, Party> ();
			Party strAreanaDefParty = new Party ();
			for (int j = 0; j < FixedConstantValue.STRONG_ARENA_PARTY_NUM; j++) {
				strAreanaDefParty.m_formList.Add( FormInfo.GetFormInfo (UnityEngine.Random.Range (1, 9)) );

				CharInfo[] charSet = new CharInfo[FixedConstantValue.PARTY_MAX_NUM];
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


	/* record */
	public List<RecordInfo> recordList;
	public List<RecordInfo> strRecordList;

	private void InitRecord() {
		recordList = new List<RecordInfo> ();
		strRecordList = new List<RecordInfo> ();

		string[] randomCharacterNames = { "IconHellephant", "IconPlayer", "IconZomBear", "IconZomBunny" };


		for (int i = 0; i < 6; i++) {
			User user = new User ("jipsa", UnityEngine.Random.Range(1,101), UnityEngine.Random.Range(1,21), randomCharacterNames[UnityEngine.Random.Range(0,4)]);
			Dictionary<PARTY_TYPE, Party> partyDic = new Dictionary<PARTY_TYPE, Party> ();
			Party areanaAtkParty = new Party ();
			for (int j = 0; j < 1; j++) {
				areanaAtkParty.m_formList.Add( FormInfo.GetFormInfo (UnityEngine.Random.Range (1, 9)) );

				CharInfo[] charSet = new CharInfo[FixedConstantValue.PARTY_MAX_NUM];
				for (int k = 0; k < charSet.Length; k++) {
					CharInfo character = new CharInfo(k * 10 + k, randomCharacterNames[UnityEngine.Random.Range(0, 4)], UnityEngine.Random.Range(1, 8), 1);
					charSet [k] = character;
				}
				areanaAtkParty.m_charSetList.Add (charSet);
			}
			partyDic.Add (PARTY_TYPE.AreanaAtk, areanaAtkParty);
			user.SetPartyDic (partyDic);


			User otheruser = new User (UnityEngine.Random.Range(10000,20000).ToString(), UnityEngine.Random.Range(1,101), UnityEngine.Random.Range(1,21), randomCharacterNames[UnityEngine.Random.Range(0,4)]);
			Dictionary<PARTY_TYPE, Party> otherUserPartyDic = new Dictionary<PARTY_TYPE, Party> ();
			Party areanaDefParty = new Party ();
			for (int j = 0; j < 1; j++) {
				areanaDefParty.m_formList.Add( FormInfo.GetFormInfo (UnityEngine.Random.Range (1, 9)) );

				CharInfo[] charSet = new CharInfo[FixedConstantValue.PARTY_MAX_NUM];
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
			User user = new User ("jipsa", UnityEngine.Random.Range(1,101), UnityEngine.Random.Range(1,21), randomCharacterNames[UnityEngine.Random.Range(0,4)]);
			Dictionary<PARTY_TYPE, Party> partyDic = new Dictionary<PARTY_TYPE, Party> ();
			Party strAreanaAtkParty = new Party ();
			for (int j = 0; j < FixedConstantValue.STRONG_ARENA_PARTY_NUM; j++) {
				strAreanaAtkParty.m_formList.Add( FormInfo.GetFormInfo (UnityEngine.Random.Range (1, 9)) );

				CharInfo[] charSet = new CharInfo[FixedConstantValue.PARTY_MAX_NUM];
				for (int k = 0; k < charSet.Length; k++) {
					CharInfo character = new CharInfo(k * 10 + k, randomCharacterNames[UnityEngine.Random.Range(0, 4)], UnityEngine.Random.Range(1, 8), 1);
					charSet [k] = character;
				}
				strAreanaAtkParty.m_charSetList.Add (charSet);
			}
			partyDic.Add (PARTY_TYPE.StrAreanaAtk, strAreanaAtkParty);
			user.SetPartyDic (partyDic);

			User otheruser = new User (UnityEngine.Random.Range(10000,20000).ToString(), UnityEngine.Random.Range(1,101), UnityEngine.Random.Range(1,21), randomCharacterNames[UnityEngine.Random.Range(0,4)]);
			Dictionary<PARTY_TYPE, Party> otherUserPartyDic = new Dictionary<PARTY_TYPE, Party> ();
			Party strAreanaDefParty = new Party ();
			for (int j = 0; j < FixedConstantValue.STRONG_ARENA_PARTY_NUM; j++) {
				strAreanaDefParty.m_formList.Add( FormInfo.GetFormInfo (UnityEngine.Random.Range (1, 9)) );

				CharInfo[] charSet = new CharInfo[FixedConstantValue.PARTY_MAX_NUM];
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
	public Dictionary<int, Reward> rewardDicByScore = new Dictionary<int, Reward>();

	private void InitReward() {
		for (int i = 1000; i <= FixedConstantValue.REWARD_MAX_SCORE; i += 1000) {
			Reward reward = new Reward (i, new int[] {0, 0, 0, 0});
			rewardDicByScore.Add (reward.m_nRewardScore, reward);
		}
	}

}

public class Reward {
	public int m_nRewardScore; //TODO: reward score -> reward rank 808!!
	public int[] m_nItemIDs; // TODO: make item class with inventory

	public Reward(int rewardScore, int[] itemIDs) {
		m_nRewardScore = rewardScore;
		m_nItemIDs = itemIDs;
	}
}

public class Item {
	public int id = 0;

	public static string GetItemSpriteName(int itemID) {
		if (itemID == 0) {
			return "gold_1";
		} else {
			Debug.LogError ("..");
			return "";
		}
	}
}