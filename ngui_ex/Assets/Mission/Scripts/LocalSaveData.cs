using UnityEngine;
using System.Collections;

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
	public User m_User;

	private void InitUser() {
		m_User = new User ("집사", 56, 10, 1200);
	}
}

public class User {
	public string m_nickName;
	public int m_nLevel;
	public int m_nAreanaRank;
	public int m_nTeamFightingPower; //방어 팀 전투력.

	public User(string nickName, int level, int areanaRank, int teamFightingPower) {
		m_nickName = nickName;
		m_nLevel = level;
		m_nAreanaRank = areanaRank;
		m_nTeamFightingPower = teamFightingPower;
	}
}
