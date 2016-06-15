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
	public void SetOtherUser(int selectedUserNum, PANEL_TYPE panelType, PARTY_TYPE partyType) {
		selectedOtherUser = selectedUserNum;

		switch (selectedEventPanel) {
		case PANEL_TYPE.AreanaRanking: // AreanaRankingPanel/PARTY_TYPE.AreanaDef
		case PANEL_TYPE.AreanaEntrance: // AreanaEntrancePanel/PARTY_TYPE.AreanaDef
		case PANEL_TYPE.ShamBattleEntrance: //ShamBattleEntrancePanel/PARTY_TYPE.AreanaAtk,PARTY_TYPE.AreanaDef
		case PANEL_TYPE.AttackPartyEdit: //AttackPartyEditPanel/PARTY_TYPE.AreanaAtk, PARTY_TYPE.ShamAtk
			this.partyType = partyType;
			selectedEventPanel = panelType;
			break;
		default:
			Debug.LogError ("...");
			break;
		}
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

		switch (selectedEventPanel) {
		case PANEL_TYPE.AreanaEntrance: //4명 중 1.
		case PANEL_TYPE.AreanaRanking: // 랭커 중 1.
		case PANEL_TYPE.AttackPartyEdit: // areanaEnt->4명 중 1. shamBatEnt->내 ShamDef.
			CharInfo[] charSet = otherUser.GetCharSet(partyType);
			return charSet[selectedOtherUserChar];
		default:
			Debug.LogError ("..");
			return null;
		}
	}
	public FormInfo GetUserForm() {
		User otherUser = GetOtherUser ();
		return otherUser.GetFormation (partyType);
	}
	public CharInfo[] GetUserParty() {
		User otherUser = GetOtherUser ();
		return otherUser.GetCharSet(partyType);
	}

}