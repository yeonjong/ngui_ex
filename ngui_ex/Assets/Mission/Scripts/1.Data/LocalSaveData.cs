using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/* user data */
public partial class LocalSaveData {
	public User m_user;
	public Key m_key;

	public LocalSaveData() {
		Init ();	
	}

	private void Init() {
		InitUser ();
	}
}

/* user information */
public partial class LocalSaveData {
	private void InitUser() {
		string[] randomCharacterNames = { "IconHellephant", "IconPlayer", "IconZomBear", "IconZomBunny" };
		m_user = new User ("jipsa", 56, 10, randomCharacterNames[1]);

		/* character dictionary */
		Dictionary<int, CharInfo> characterDic = new Dictionary<int, CharInfo> ();
		for (int i = 0; i < FixedConstantValue.IMSI_USER_CHAR_GEN_NUM; i++) { // this user have 48 characters;
			CharInfo character = new CharInfo(i * 10 + i, randomCharacterNames[UnityEngine.Random.Range(0, 4)], UnityEngine.Random.Range(1, 8), 1);
			characterDic.Add (character.id, character);
		}
		m_user.SetCharacterDic (characterDic);

		/* party dictionary */
		Dictionary<PARTY_TYPE, Party> partyDic = new Dictionary<PARTY_TYPE, Party> ();

		for (int i = 0; i < Enum.GetNames(typeof(PARTY_TYPE)).Length; i++) {
			Party party = new Party ();

			int max;
			if (i == (int)PARTY_TYPE.Dungeon)
				max = FixedConstantValue.DUNGEION_PARTY_NUM;
			else if (i == (int)PARTY_TYPE.StrAreanaAtk || i == (int)PARTY_TYPE.StrAreanaDef)
				max = FixedConstantValue.STRONG_ARENA_PARTY_NUM;
			else
				max = FixedConstantValue.OTHER_PARTY_NUM;

			for (int j = 0; j < max; j++) {
				party.m_formList.Add( FormInfo.GetFormInfo (UnityEngine.Random.Range (0, 8)) );

				CharInfo[] charSet = new CharInfo[FixedConstantValue.PARTY_MAX_CHAR_NUM];
				List<int> duplicationCheckList = new List<int> ();
				for (int k = 0; k < charSet.Length; k++) {
					int randomCharID;
					while (true) {
						bool isDuplication = false;
						randomCharID = UnityEngine.Random.Range (0, FixedConstantValue.IMSI_USER_CHAR_GEN_NUM + 3);

						for (int l = 0; l < duplicationCheckList.Count; l++) {
							if (duplicationCheckList [l] == randomCharID) {
								isDuplication = true;
								break;
							}
						}

						if (isDuplication == false) {
							duplicationCheckList.Add (randomCharID);
							break;
						}
					}

					if (randomCharID >= FixedConstantValue.IMSI_USER_CHAR_GEN_NUM) {
						charSet [k] = null;
					} else {
						randomCharID = randomCharID * 10 + randomCharID;
						charSet [k] = characterDic [randomCharID];
					}
				}
				party.m_charSetList.Add (charSet);
			}

			partyDic.Add ((PARTY_TYPE)i, party);
		}
		m_user.SetPartyDic (partyDic);

		/* key */
		m_key = new Key (5, 4);
	}
}
	




public partial class User {
	public string m_nickName;
	public int m_nLevel;
	public int m_nAreanaRank;
	public int m_nAreanaRanking = 17;
 public string m_mainCharacterName; // TODO: m_mainTeam's first info is main character! so delete this variable.
	public string m_guildName;
	public int m_maxPartyCost = 1000;

	public Dictionary<int, CharInfo> m_charDicByID;
	private Dictionary<PARTY_TYPE, Party> m_partyDic;

	public User(string nickName, int level, int areanaRank, string mainCharacterName) {
		m_nickName = nickName;
		m_nLevel = level;
		m_nAreanaRank = areanaRank;
		m_mainCharacterName = mainCharacterName;
	}
}

public class CharInfoCompare : IComparer {

	int IComparer.Compare(object x, object y)
	{
		CharInfo charX = (CharInfo)x;
		CharInfo charY = (CharInfo)y;
		int returnValue;

		if (charX.cost == charY.cost) {
			returnValue = 0;
		} else if (charX.cost > charY.cost) {
			returnValue = 1;
		} else {
			returnValue = -1;
		}


		//x.cost;
		//x.starRank;
		//x.upgradeRank;
		//x.fightingPower;

		return returnValue; // -1(x < y?), 0(x == y), 1(x > y?)
	}
}

public partial class User {
	/* all character */
	public void SetCharacterDic(Dictionary<int, CharInfo>  characterDic) {
		m_charDicByID = characterDic;
	}
	/*
	public List<int> GetCharacterIDList() {
		return DoHardSort (new List<int>(m_charDicByID.Keys));
	}
	*/
	public List<CharInfo> GetCharacterList() {
		return DoHardSort (new List<CharInfo> (m_charDicByID.Values));
	}

	private static int CompareDinosByLength(CharInfo x, CharInfo y) {
		int returnValue;

		if (x.cost == y.cost) {									//1. cost

			if (x.starRank == y.starRank) {						//2. starRank

				if (x.upgradeRank == y.upgradeRank) {			//3. upgradeRank
					
					if (x.fightingPower == y.fightingPower) {	//4. fightingPower
						returnValue = 0;
					} else if (x.fightingPower > y.fightingPower) {
						returnValue = -1;
					} else {
						returnValue = 1;
					}

				} else if (x.upgradeRank > y.upgradeRank) {
					returnValue = -1;
				} else {
					returnValue = 1;
				}
			
			} else if (x.starRank > y.starRank) {
				returnValue = -1;
			} else {
				returnValue = 1;
			}

		} else if (x.cost > y.cost) {
			returnValue = -1;
		} else {
			returnValue = 1;
		}

		return returnValue;
	}

	private List<CharInfo> DoHardSort(List<CharInfo> origin) {
		//show before

		//CharInfoCompare compare = (IComparer) new CharInfoCompare ();
		origin.Sort (CompareDinosByLength);

		//show after

		return origin;
	}



	/* party */
	public void SetPartyDic (Dictionary<PARTY_TYPE, Party> partyDic) {
		m_partyDic = partyDic;
	}
	public Party GetParty (PARTY_TYPE partyType) {
		return m_partyDic [partyType];
	}
	public int GetPartyFightingPower(PARTY_TYPE partyType, int listIndex = 0) {
		int fightingPower = 0;
		CharInfo[] charSet = GetCharSet (partyType, listIndex);
		for (int i = 0; i < charSet.Length; i++) {
			if (charSet[i] != null)
				fightingPower += charSet [i].fightingPower;
		}
		return fightingPower;
	}
	public int GetPartyCost(PARTY_TYPE partyType, int listIndex = 0) {
		int cost = 0;
		CharInfo[] charSet = GetCharSet (partyType, listIndex);
		for (int i = 0; i < charSet.Length; i++) {
			if (charSet[i] != null)
				cost += charSet [i].cost;
		}
		return cost;
	}
	public CharInfo[] GetCharSet(PARTY_TYPE partyType, int listIndex = 0) {
		return m_partyDic [partyType].m_charSetList [listIndex];
	}
	public void SetFormation(PARTY_TYPE partyType, int listIndex, int formNum) {
		m_partyDic [partyType].m_formList [listIndex] = FormInfo.GetFormInfo (formNum);
	}
	public FormInfo GetFormation(PARTY_TYPE partyType, int listIndex = 0) {
		return m_partyDic [partyType].m_formList [listIndex];
	}
}


public class Party {
	public List<FormInfo> m_formList = new List<FormInfo> ();
	public List<CharInfo[]> m_charSetList = new List<CharInfo[]> ();
	/*
	public int GetPartyFightingPower(int partyNum = 0) {
		int fightingPower = 0;
		CharInfo[] charSet = m_charSetList [partyNum];
		for (int i = 0; i < charSet.Length; i++) {
			if (charSet[i] != null)
				fightingPower += charSet [i].fightingPower;
		}
		return fightingPower;
	}
	*/
}

public class CharInfo {
	public int id;
	public string name = "NAME";
	public string spriteName;
	public int cost;
	public int hp;
	public int starRank = 0;
	public int upgradeRank = 0;
	public int level = 60;
	public string classKind = "atk";
	public string feature = null;
	public int fightingPower;
	//public int[] formationPos;

	public CharInfo(int id, string spriteName, int cost, int hp) {
		this.id = id;
		this.spriteName = spriteName;
		this.cost = cost;
		this.hp = hp;

		fightingPower = UnityEngine.Random.Range (10, 101);
	}
}

public class FormInfo {
	public string m_Name = "FORM_NAME";
	public string m_EffectDisc = "DISCRIPTION ... ... ...";
	public int[] m_Form;

	public static FormInfo GetFormInfo(int formNum) {
		FormInfo info = new FormInfo ();

		switch (formNum) {
		case 0:
			info.m_Name = "ATK FORM";
			info.m_EffectDisc = "20% ATK UP";
			info.m_Form = new int[FixedConstantValue.FORM_CELL_NUM] {
				-1,	 0,	 1,	-1,
				-1,	-1,	 2,	-1,
				-1,	-1,	 3,	-1,
				-1,	-1,	 4,	-1,
				-1,	 5,	 6,	 7,
				-1,	-1,	-1,	-1
			};
			break;
		case 1:
			info.m_Name = "DEF FORM";
			info.m_EffectDisc = "40% DEF UP, 10% DEF DOWN";
			info.m_Form = new int[FixedConstantValue.FORM_CELL_NUM] {
				-1,	-1,	 0,	-1,
				-1,	-1,	-1,	 1,
				-1,	-1,	-1,	 2,
				-1,	-1,	 3,	-1,
				-1,	 4,	-1,	-1,
				-1,	 5,	 6,	 7
			};
			break;
		case 2:
			info.m_Name = "FORM 3";
			info.m_EffectDisc = "DISCRIPTION ... ... ... 3";
			info.m_Form = new int[FixedConstantValue.FORM_CELL_NUM] {
				-1,	 0,	 1,	-1,
				-1,	-1,	-1,	 2,
				-1,	-1,	 3,	-1,
				-1,	-1,	-1,	 4,
				-1,	-1,	-1,	 5,
				-1,	 6,	 7,	-1
			};
			break;
		case 3:
			info.m_Name = "FORM 4";
			info.m_EffectDisc = "DISCRIPTION ... ... ... 4";
			info.m_Form = new int[FixedConstantValue.FORM_CELL_NUM] {
				-1,	-1,	 0,	-1,
				-1,	 1,	-1,	-1,
				2,	-1,	-1,	-1,
				3,	 4,	 5,	 6,
				-1,	-1,	 7,	-1,
				-1,	-1,	-1,	-1
			};
			break;
		case 4:
			info.m_Name = "FORM 5";
			info.m_EffectDisc = "DISCRIPTION ... ... ... 5";
			info.m_Form = new int[FixedConstantValue.FORM_CELL_NUM] {
				-1,	 0,	 1,	-1,
				-1,	 2,	-1,	-1,
				-1,	 3,	 4,	-1,
				-1,	-1,	-1,	 5,
				-1,	-1,	-1,	 6,
				-1,	-1,	 7,	-1
			};
			break;
		case 5:
			info.m_Name = "FORM 6";
			info.m_EffectDisc = "DISCRIPTION ... ... ... 6";
			info.m_Form = new int[FixedConstantValue.FORM_CELL_NUM] {
				-1,	-1,	 0,	-1,
				-1,	 1,	-1,	-1,
				-1,	 2,	-1,	-1,
				-1,	 3,	 7,	-1,
				-1,	 4,	-1,	 6,
				-1,	-1,	 5,	-1
			};
			break;
		case 6:
			info.m_Name = "FORM 7";
			info.m_EffectDisc = "DISCRIPTION ... ... ... 7";
			info.m_Form = new int[FixedConstantValue.FORM_CELL_NUM] {
				-1,	 0,	 1,	 2,
				-1,	-1,	-1,	 3,
				-1,	-1,	-1,	 4,
				-1,	-1,	-1,	 5,
				-1,	-1,	-1,	 6,
				-1,	-1,	-1,	 7
			};
			break;
		case 7:
			info.m_Name = "FORM 8";
			info.m_EffectDisc = "DISCRIPTION ... ... ... 8";
			info.m_Form = new int[FixedConstantValue.FORM_CELL_NUM] {
				-1,	-1,	 0,	-1,
				-1,	 1,	 2,	-1,
				-1,	-1,	 3,	-1,
				-1,	 4,	-1,	-1,
				-1,	 5,	 6,	-1,
				-1,	 7,	-1,	-1
			};
			break;
		}

		return info;
	}


}


public class Key {
	public int m_nMaxAreanaKey;
	public int m_nAreanaKey;

	public int m_nMaxDungeonKey;
	public int m_nDungeonKey;

	public Key(int maxAreanaKey, int areanaKey) {
		m_nMaxAreanaKey = maxAreanaKey;
		m_nAreanaKey = areanaKey;
	}

	public void AddAreanaKey(int addCnt) {
		m_nAreanaKey += addCnt;
	}

	public void SubAreanaKey(int subCnt) {
		m_nAreanaKey -= subCnt;
	}
}