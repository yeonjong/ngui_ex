using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameData {

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
		for (int i = 0; i < 52; i++) {
			SetCharInfo (new CharInfo (i.ToString(), i, i));
		}

		/* dummy formation data */
		SetFormation (0);
	}

	/* formation data */
	// -1: empty and can't enter.
	// -2: empty and can enter.
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

	//private int[,] currentFormation;
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

	public void SetFormation (int formationNumber) {
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

	/* character data */
	private Dictionary<string, CharInfo> characterInfoDic = new Dictionary<string, CharInfo> ();

	public void SetCharInfo(CharInfo info) {
		if (!characterInfoDic.ContainsKey (info.name)) {
			characterInfoDic.Add (info.name, info);
		} else {
			Debug.LogError ("duplicate character. it is not add to dictionary.");
		}
	}

	public CharInfo GetCharInfo(string name) {
		if (characterInfoDic.ContainsKey (name)) {
			return characterInfoDic [name];
		} else {
			Debug.LogError ("this character not exist. " + name);
			return null;
		}
	}

	public int GetCharacterCount() {
		return characterInfoDic.Count;
	}

	public List<string> GetCharacterKeyList() {
		return new List<string>(characterInfoDic.Keys);
	}






}
	
/* character data */
public class CharInfo {
	public int id;
	public string name;
	public int cost;
	public int hp;
	//public int[] formationPos;

	public CharInfo(string name, int cost, int hp) {
		this.name = name;
		this.cost = cost;
		this.hp = hp;
	}
}