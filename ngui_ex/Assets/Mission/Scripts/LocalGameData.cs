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
			m_otherUsers [i] = new User (UnityEngine.Random.Range(10000,20000).ToString(), UnityEngine.Random.Range(1,101), UnityEngine.Random.Range(1,21), UnityEngine.Random.Range(1000,3001), randomCharacterNames[UnityEngine.Random.Range(0,4)]);

			CharInfo[] otherUserParty = new CharInfo[FixedConstantValue.PARTY_MAX_NUM];
			for (int j = 0; j < otherUserParty.Length; j++) {
				CharInfo character = new CharInfo(j * 10 + j, randomCharacterNames[UnityEngine.Random.Range(0, 4)], UnityEngine.Random.Range(1, 8), 1);
				otherUserParty [j] = character;
			}
			m_otherUsers [i].AddParty (otherUserParty, FormInfo.GetFormInfo (UnityEngine.Random.Range (1, 9)));
		}
	}

	/* high rank */
	private User[]  m_highRankUsers;
	public User[] GetHighRankUsers(bool reset = false) {
		if (reset)
			ResetHighRankUser ();
		return m_highRankUsers;
	}

	private void InitHighRankUser() {
		m_highRankUsers = new User[3 + 20]; // + 10
		ResetHighRankUser ();
	}

	private void ResetHighRankUser() {
		string[] randomCharacterNames = { "IconHellephant", "IconPlayer", "IconZomBear", "IconZomBunny" };

		for (int i = 0; i < m_highRankUsers.Length; i++) {
			m_highRankUsers [i] = new User (UnityEngine.Random.Range(10000,20000).ToString(), UnityEngine.Random.Range(1,101), UnityEngine.Random.Range(1,21), UnityEngine.Random.Range(1000,3001), randomCharacterNames[UnityEngine.Random.Range(0,4)]);

			CharInfo[] party = new CharInfo[FixedConstantValue.PARTY_MAX_NUM];
			for (int j = 0; j < party.Length; j++) {
				CharInfo character = new CharInfo(j * 10 + j, randomCharacterNames[UnityEngine.Random.Range(0, 4)], UnityEngine.Random.Range(1, 8), 1);
				party [j] = character;
			}
			m_highRankUsers [i].AddParty (party, FormInfo.GetFormInfo (UnityEngine.Random.Range (1, 9)));
		}
	}


	/* record */
	public List<RecordInfo> recordList;

	private void InitRecord() {
		recordList = new List<RecordInfo> ();

		string[] randomCharacterNames = { "IconHellephant", "IconPlayer", "IconZomBear", "IconZomBunny" };


		for (int i = 0; i < 6; i++) {
			User user = new User ("jipsa", UnityEngine.Random.Range(1,101), UnityEngine.Random.Range(1,21), UnityEngine.Random.Range(1000,3001), randomCharacterNames[UnityEngine.Random.Range(0,4)]);
			CharInfo[] userParty = new CharInfo[FixedConstantValue.PARTY_MAX_NUM];
			for (int j = 0; j < userParty.Length; j++) {
				CharInfo character = new CharInfo(j * 10 + j, randomCharacterNames[UnityEngine.Random.Range(0, 4)], UnityEngine.Random.Range(1, 8), 1);
				userParty [j] = character;
			}
			user.AddParty (userParty, FormInfo.GetFormInfo (UnityEngine.Random.Range (1, 9)));

			User otheruser = new User (UnityEngine.Random.Range(10000,20000).ToString(), UnityEngine.Random.Range(1,101), UnityEngine.Random.Range(1,21), UnityEngine.Random.Range(1000,3001), randomCharacterNames[UnityEngine.Random.Range(0,4)]);
			CharInfo[] otherUserParty = new CharInfo[FixedConstantValue.PARTY_MAX_NUM];
			for (int j = 0; j < otherUserParty.Length; j++) {
				CharInfo character = new CharInfo(j * 10 + j, randomCharacterNames[UnityEngine.Random.Range(0, 4)], UnityEngine.Random.Range(1, 8), 1);
				otherUserParty [j] = character;
			}
			otheruser.AddParty (otherUserParty, FormInfo.GetFormInfo (UnityEngine.Random.Range (1, 9)));
			AddRecord (UnityEngine.Random.Range (0, 2) == 1 ? true : false, otheruser, user);
		}
	}

	public void AddRecord(bool isWin, User otherUserInfo, User userInfo) {
		int deviation = UnityEngine.Random.Range (-10, 0);
		recordList.Add (new RecordInfo (isWin, true, DateTime.Now.AddHours (deviation), otherUserInfo, userInfo));
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