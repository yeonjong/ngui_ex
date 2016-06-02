using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* GameData */
partial class GameData {

	private GameData() { }
	private static GameData inst;
	public static GameData Inst {
		get {
			if (inst == null) {
				inst = new GameData ();
				inst.Initialize ();
			}
			return inst;
		}
	}

	// TODO: please make some json files and get those from HttpMgr class.
	private void Initialize() {
		/* dummy character data */
		for (int i = 1001; i < MAX_COMMON_CHARACTER_KINDS + 1001; i++) {
			CharacterPrivateSpec.AddCharacterCommonSpec(new CharacterCommonSpec(i, i+"name", i+"className", i % 1000));
		}
		for (int i = 0; i < MAX_PRIVATE_CHARACTER_COUNT; i++) {
			AddCharacterSpec (new CharacterPrivateSpec (i, UnityEngine.Random.Range(1001, 1001+MAX_COMMON_CHARACTER_KINDS)));
		}

		/* dummy formation data */
		SetCurrentFormation (1);
	}

}

/* character data */
public class CharacterPrivateSpec {
	private static Dictionary<int, CharacterCommonSpec> characterCommonSpecDic = new Dictionary<int, CharacterCommonSpec> ();
	public static void AddCharacterCommonSpec(CharacterCommonSpec spec) {
		if (!characterCommonSpecDic.ContainsKey (spec.commonCharacterID)) {
			characterCommonSpecDic.Add (spec.commonCharacterID, spec);
		}
	}
	public CharacterCommonSpec CommonSpec {
		get {
			return characterCommonSpecDic [commonCharacterID];
		}
	}

	public int commonCharacterID; //케릭터의 공통스펙을 찾을 수 있는 아이디.

	public int privateCharacterID; //유저마다 가지고 있는 케릭터의 고유 아이디. 유저가 영웅을 획득한 순서대로 int값 넣어주면 될 것 같다. 나중에 획득순 정렬이 있으면 쓸 수 있을듯.
	public int starRank;
	public int upgradeRank;
	public int upgradeRankEXP; // upgrade 총 경험치.
	public int level;
	public int levelEXP; // level 총 경험치.

	public CharacterPrivateSpec(int privateID, int commonID) {
		privateCharacterID = privateID;
		commonCharacterID = commonID;
	}

	public string GetStarRankImageName() {
		return "spr_" + starRank;
	}
	
	public string GetUpgradeRankImageName() {
		return "spr_" + upgradeRank;
	}
}
public class CharacterCommonSpec {
	public int commonCharacterID;

	public string name;
	public string className;
	public int cost;

	public CharacterCommonSpec(int commonID, string name, string className, int cost) {
		commonCharacterID = commonID;
		this.name = name;
		this.className = className;
		this.cost = cost;
	}

	public string GetThumbnailImageName() {
		return "spr_" + name;
	}
}
partial class GameData {

	private const int MAX_COMMON_CHARACTER_KINDS = 10; // 게임의 영웅 종류가 10종이다. 라는 뜻이다.
	private const int MAX_PRIVATE_CHARACTER_COUNT = 50; // 유저가 가진 영웅의 수가 종류에 상관없이 50명이다. 라는 뜻이다. // TODO: = 3; (little items)

	private Dictionary<int, CharacterPrivateSpec> characterPrivateSpecDic = new Dictionary<int, CharacterPrivateSpec> ();

	public void AddCharacterSpec(CharacterPrivateSpec spec) {
		if (!characterPrivateSpecDic.ContainsKey (spec.privateCharacterID)) {
			characterPrivateSpecDic.Add (spec.privateCharacterID, spec);
		} else {
			Debug.LogError ("duplicate character. it is not add to dictionary.");
		}
	}

	public CharacterPrivateSpec GetCharacterSpec(int privateCharacterID) {
		if (characterPrivateSpecDic.ContainsKey (privateCharacterID)) {
			return characterPrivateSpecDic [privateCharacterID];
		} else {
			Debug.LogError ("this character not exist. ID: " + privateCharacterID);
			return null;
		}
	}

	public int GetCharacterCount() {
		return characterPrivateSpecDic.Count;
	}

	public List<int> GetCharacterIDList() {
		return new List<int>(characterPrivateSpecDic.Keys);
	}

}

/* formation data */
partial class GameData {

	// -2: empty and can enter.
	// -1: empty and can't enter.
	// 0~: character id.
	private int[,] formation0 = new int[6,4] {
		{ -1,	0,	1,	-1 },
		{ -1,	-1,	2,	-1 },
		{ -1,	-1,	3,	-1 },
		{ -1,	-1,	4,	-1 },
		{ -1,	-1,	5,	-1 },
		{ -1,	6,	7,	8 }
	};

	private int[,] formation1 = new int[6,4] {
		{ -1,	-2,	-2,	-1 },
		{ -1,	-1,	-1,	-2 },
		{ -1,	-1,	-1,	-2 },
		{ -1,	-1,	-2,	-1 },
		{ -1,	-2,	-1,	-1 },
		{ -1,	-2,	-2,	-2 }
	};

	private int[,] formation2 = new int[6,4] {
		{ -1,	-2,	-2,	-1 },
		{ -1,	-1,	-1,	-2 },
		{ -1,	-1,	-2,	-1 },
		{ -1,	-1,	-1,	-2 },
		{ -1,	-1,	-1,	-2 },
		{ -1,	-2,	-2,	-1 }
	};

	private int[,] formation3 = new int[6,4] {
		{ -1,	-1,	-2,	-1 },
		{ -1,	-2,	-1,	-1 },
		{ -2,	-1,	-1,	-1 },
		{ -2,	-2,	-2,	-2 },
		{ -1,	-1,	-2,	-1 },
		{ -1,	-1,	-2,	-1 }
	};

	private int[,] formation4 = new int[6,4] {
		{ -1,	-2,	-2,	-2 },
		{ -1,	-2,	-1,	-1 },
		{ -1,	-2,	-2,	-2 },
		{ -1,	-1,	-1,	-2 },
		{ -1,	-1,	-1,	-2 },
		{ -1,	-2,	-2,	-2 }
	};

	private int[,] formation5 = new int[6,4] {
		{ -1,	-1,	-2,	-2 },
		{ -1,	-2,	-1,	-1 },
		{ -1,	-2,	-1,	-1 },
		{ -1,	-2,	-2,	-1 },
		{ -1,	-2,	-1,	-2 },
		{ -1,	-1,	-2,	-1 }
	};

	private int[,] formation6 = new int[6,4] {
		{ -1,	-2,	-2,	-2 },
		{ -1,	-1,	-1,	-2 },
		{ -1,	-1,	-1,	-2 },
		{ -1,	-1,	-1,	-2 },
		{ -1,	-1,	-1,	-2 },
		{ -1,	-1,	-1,	-2 }
	};

	private int[,] formation7 = new int[6,4] {
		{ -1,	-2,	-2,	-1 },
		{ -2,	-1,	-1,	-2 },
		{ -1,	-2,	-2,	-1 },
		{ -2,	-1,	-1,	-2 },
		{ -2,	-1,	-1,	-2 },
		{ -1,	-2,	-2,	-1 }
	};

	private int curFormationNumber;
	public int[,] GetFormation () {
		switch (curFormationNumber) {
		case 0:
			return formation0;
		case 1:
			return formation1;
		case 2:
			return formation2;
		case 3:
			return formation3;
		case 4:
			return formation4;
		case 5:
			return formation5;
		case 6:
			return formation6;
		case 7:
			return formation7;
		default:
			return null;
		}
	}

	public void SetCurrentFormation (int formationNumber) {
		curFormationNumber = formationNumber;
	}

	public int GetFormationFirstEmptyPos() {
		int formationItem;
		int[,] formation = GetFormation ();
		for (int i = 0; i < 24; i++) {
			formationItem = formation [i / 4, i % 4];
			if (formationItem == -2) {
				return i;
			}
		}

		return -1; // formation was full.
	}

	public void SetFormationItem (int row, int col, int flag) {
		switch (curFormationNumber) {
		case 0:
			formation0 [row, col] = flag;
			return;
		case 1:
			formation1 [row, col] = flag;
			return;
		case 2:
			formation2 [row, col] = flag;
			return;
		case 3:
			formation3 [row, col] = flag;
			return;
		case 4:
			formation4 [row, col] = flag;
			return;
		case 5:
			formation5 [row, col] = flag;
			return;
		case 6:
			formation6 [row, col] = flag;
			return;
		case 7:
			formation7 [row, col] = flag;
			return;
		default:
			return;
		}
	}

	public Dictionary<int, int> GetFormationMemberDic() {
		Dictionary<int, int> formationMemberDic = new Dictionary<int, int> (); //charID, formationPos

		int formationItem;
		int[,] formation = GetFormation ();
		for (int i = 0; i < 24; i++) {
			formationItem = formation [i / 4, i % 4];
			if (formationItem >= 0) { // is there a character's id?
				formationMemberDic.Add(formationItem, i);			
			}
		}

		return formationMemberDic;
	}

}