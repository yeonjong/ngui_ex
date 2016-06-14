using UnityEngine;

public class GlobalApp {

	private static GlobalApp inst;
	public static GlobalApp Inst {
		get {
			if (inst == null) {
				inst = new GlobalApp ();
			}
			return inst;
		}
	}
	private GlobalApp() {
		userData = new LocalSaveData ();
		commData = new LocalGameData ();
	}

	public LocalSaveData userData; //유저 정보.
	public LocalGameData commData; //공통 정보.


	private int selectedOtherUser;
	public int selectedOtherUserChar;
	private PARTY_TYPE partyType; // int partyType => 0: dungeionParty, 1: areanaAtkParty, 2: areanaDefParty
	private PANEL_TYPE selectedEventPanel;
	public void SetOtherUser(int selectedUserNum, PANEL_TYPE panelType, PARTY_TYPE partyType = PARTY_TYPE.Dungeon) {
		this.partyType = partyType;
		selectedOtherUser = selectedUserNum;
		selectedEventPanel = panelType;
	}
	public User GetOtherUser() {
		if (selectedEventPanel.Equals (PANEL_TYPE.AreanaEntrance)) {
			return commData.GetOtherUsers () [selectedOtherUser];
		} else if (selectedEventPanel.Equals (PANEL_TYPE.AreanaRanking)) {
			return commData.GetHighRankUsers () [selectedOtherUser];
		} else if (selectedEventPanel.Equals (PANEL_TYPE.ShamBattleEntrance)) {
			return userData.m_user;
		} else {
			Debug.LogError ("...");
			return null;
		}
	}
	public CharInfo GetOtherUserCharacter() {
		User otherUser = GetOtherUser ();
		return otherUser.m_partyList[(int)partyType][selectedOtherUserChar];
	}
	public FormInfo GetUserForm() {
		User otherUser = GetOtherUser ();
		return otherUser.m_formList[(int)partyType];
	}
	public CharInfo[] GetUserParty() {
		User otherUser = GetOtherUser ();
		return otherUser.m_partyList [(int)partyType];
	}

}