﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* user data */
public partial class LocalSaveData {
	public LocalSaveData() {
		Init ();	
	}

	private void Init() {
		InitUser ();
	}


}

/* user information */
public partial class LocalSaveData {
	public User m_user;
	public Key m_key;

	private void InitUser() {
		string[] randomCharacterNames = { "IconHellephant", "IconPlayer", "IconZomBear", "IconZomBunny" };
		m_user = new User ("jipsa", 56, 10, 1200, randomCharacterNames[1]);

		Dictionary<int, CharInfo> characterDic = new Dictionary<int, CharInfo> ();
		for (int i = 0; i < FixedConstantValue.TEMP_USER_CHAR_NUM; i++) { // this user have 48 characters;
			CharInfo character = new CharInfo(i * 10 + i, randomCharacterNames[Random.Range(0, 4)], Random.Range(1, 8), 1);
			characterDic.Add (character.id, character);
		}
		m_user.SetCharacterDic (characterDic);

		for (int j = 0; j < 3; j++) {
			CharInfo[] party = new CharInfo[FixedConstantValue.PARTY_MAX_NUM];
			for (int i = 0; i < party.Length; i++) {
				int randomCharID = Random.Range (0, FixedConstantValue.TEMP_USER_CHAR_NUM);
				if (randomCharID == FixedConstantValue.TEMP_USER_CHAR_NUM) {
					party [i] = null;
				} else {
					randomCharID = randomCharID * 10 + randomCharID;
					party [i] = characterDic [randomCharID];
				}
			}
			m_user.AddParty (party, FormInfo.GetFormInfo(Random.Range (1, 9)) ); // add dungeonParty, areanaAtkParty, areanaDefParty.
		}

		m_key = new Key (5, 4);
	}
}

public enum PARTY_TYPE {
	Dungeon,
	AreanaAtk,
	AreanaDef,
}

public class User {
	/* party edit */
	public List<int> GetCharacterKeyList() {
		return new List<int>(m_characterDic.Keys);
	}







	/* party edit - end */




	public string m_nickName;
	public int m_nLevel;
	public int m_nAreanaRank;
	public int m_nAreanaRanking = 17;
	public string m_mainCharacterName; // m_mainTeam's first info is main character! so delete this variable.
	public string m_guildName;

	//public int[] m_mainFormation;
	//public string m_formationEffectDisc; // maybe made formation class.. but...
	//public List<CharInfo> m_mainTeam;

	public Dictionary<int, CharInfo> m_characterDic; // key: CharInfo.id, val: CharInfo

	/* dungeonParty/Form, areanaAtkParty/Form, areanaDefParty/Form, ... */

	public int m_nTeamFightingPower; // 방어 팀 전투력. // TODO: delete this param...
	public List<CharInfo[]> m_partyList = new List<CharInfo[]> ();
	public List<FormInfo> m_formList = new List<FormInfo> ();
	public List<int> m_partyFightingPowerLlist = new List<int> ();
	public int m_maxPartyCost = 1000;

	public User(string nickName, int level, int areanaRank, int teamFightingPower, string mainCharacterName) {
		m_nickName = nickName;
		m_nLevel = level;
		m_nAreanaRank = areanaRank;
		m_nTeamFightingPower = teamFightingPower;
		m_mainCharacterName = mainCharacterName;
	}

	/*
	public void SetFormation(int[] mainFormation, string formationEffectDisc) {
		m_mainFormation = mainFormation;
		m_formationEffectDisc = formationEffectDisc;
	}
	public void SetMainTeam(List<CharInfo> mainTeam) {
		m_mainTeam = mainTeam;
	}
	*/

	public void SetCharacterDic(Dictionary<int, CharInfo>  characterDic) {
		m_characterDic = characterDic;
	}
	public void AddParty(CharInfo[] newParty, FormInfo newForm) {
		m_partyList.Add (newParty);
		m_formList.Add (newForm);

		int fightingPower = 0;
		for (int i = 0; i < FixedConstantValue.PARTY_MAX_NUM; i++) {
			if (newParty [i] != null) {
				fightingPower += newParty [i].fightingPower;
			}
		}
		m_partyFightingPowerLlist.Add (fightingPower);
	}

}

public class FormInfo {
	public string m_Name = "FORM_NAME";
	public string m_EffectDisc = "DISCRIPTION ... ... ...";
	public int[] m_Form;

	public static FormInfo GetFormInfo(int formNum) {
		FormInfo info = new FormInfo ();

		switch (formNum) {
		case 1:
			info.m_Name = "ATK FORM";
			info.m_EffectDisc = "20% ATK UP";
			info.m_Form = new int[24] {
				-1,	 0,	 1,	-1,
				-1,	-1,	 2,	-1,
				-1,	-1,	 3,	-1,
				-1,	-1,	 4,	-1,
				-1,	 5,	 6,	 7,
				-1,	-1,	-1,	-1
			};
			break;
		case 2:
			info.m_Name = "DEF FORM";
			info.m_EffectDisc = "40% DEF UP, 10% DEF DOWN";
			info.m_Form = new int[24] {
				-1,	-1,	 0,	-1,
				-1,	-1,	-1,	 1,
				-1,	-1,	-1,	 2,
				-1,	-1,	 3,	-1,
				-1,	 4,	-1,	-1,
				-1,	 5,	 6,	 7
			};
			break;
		case 3:
			info.m_Name = "FORM 3";
			info.m_EffectDisc = "DISCRIPTION ... ... ... 3";
			info.m_Form = new int[24] {
				-1,	 0,	 1,	-1,
				-1,	-1,	-1,	 2,
				-1,	-1,	 3,	-1,
				-1,	-1,	-1,	 4,
				-1,	-1,	-1,	 5,
				-1,	 6,	 7,	-1
			};
			break;
		case 4:
			info.m_Name = "FORM 4";
			info.m_EffectDisc = "DISCRIPTION ... ... ... 4";
			info.m_Form = new int[24] {
				-1,	-1,	 0,	-1,
				-1,	 1,	-1,	-1,
				2,	-1,	-1,	-1,
				3,	 4,	 5,	 6,
				-1,	-1,	 7,	-1,
				-1,	-1,	-1,	-1
			};
			break;
		case 5:
			info.m_Name = "FORM 5";
			info.m_EffectDisc = "DISCRIPTION ... ... ... 5";
			info.m_Form = new int[24] {
				-1,	 0,	 1,	-1,
				-1,	 2,	-1,	-1,
				-1,	 3,	 4,	-1,
				-1,	-1,	-1,	 5,
				-1,	-1,	-1,	 6,
				-1,	-1,	 7,	-1
			};
			break;
		case 6:
			info.m_Name = "FORM 6";
			info.m_EffectDisc = "DISCRIPTION ... ... ... 6";
			info.m_Form = new int[24] {
				-1,	-1,	 0,	-1,
				-1,	 1,	-1,	-1,
				-1,	 2,	-1,	-1,
				-1,	 3,	 7,	-1,
				-1,	 4,	-1,	 6,
				-1,	-1,	 5,	-1
			};
			break;
		case 7:
			info.m_Name = "FORM 7";
			info.m_EffectDisc = "DISCRIPTION ... ... ... 7";
			info.m_Form = new int[24] {
				-1,	 0,	 1,	 2,
				-1,	-1,	-1,	 3,
				-1,	-1,	-1,	 4,
				-1,	-1,	-1,	 5,
				-1,	-1,	-1,	 6,
				-1,	-1,	-1,	 7
			};
			break;
		case 8:
			info.m_Name = "FORM 8";
			info.m_EffectDisc = "DISCRIPTION ... ... ... 8";
			info.m_Form = new int[24] {
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

/* character data */
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