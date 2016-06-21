using UnityEngine;
using System.Collections.Generic;

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
	
		cachedParties = new Party[2];
	}

	public LocalSaveData userData; //유저 정보.
	public LocalGameData commData; //공통 정보.

	public bool isWin = true;

	private Party[] cachedParties;
	private int btnIndex;
	public Party[] GetCachedParties() {
		return cachedParties;
	}
	public void SetCachedParties(params Party[] args) {
		if (args.Length == 1) {
			cachedParties [0] = args [0];
			cachedParties [1] = null;
		} else {
			cachedParties [0] = args [0];
			cachedParties [1] = args [1];
		}
	}
	public int GetBtnIndex() {
		return btnIndex;
	}
	public void SetBtnIndex(int idx) {
		btnIndex = idx;
	}



	/* party */
	public int currDungeonParty = 0;

	public Party GetParty (PARTY_TYPE partyType) {
		return userData.GetUser().GetParty (partyType);
	}

	// hard coding
	public int userIndex = -1; //n
	public int charIndex = -1; //m

	/*
	public Party GetPartyAtSpecialCase (string specialCase) {
		Debug.LogFormat ("userIndex: {0}", userIndex);

		Party party = null;

		switch (specialCase) {
		case "AreanaEntrancePanel/OtherUserPartyInfoPanel":
			party = commData.m_areanaUsers [userIndex].GetParty (PARTY_TYPE.AreanaDef);
			break;
		case "AreanaEntrancePanel/AreanaRankingPanel/OtherUserPartyInfoPanel":
			party = commData.m_highRankUsers [userIndex].GetParty (PARTY_TYPE.AreanaDef);
			break;

		}

		return party;
	}
	*/
	public User GetUserAtSpecialCase (string specialCase) {
		Debug.LogFormat ("{0}\nuserIndex: {1}", specialCase, userIndex);

		User user = null;

		switch (specialCase) {
		case "AreanaEntrancePanel/OtherUserPartyInfoPanel":
			user = commData.GetAreanaUsers() [userIndex];
			break;
		case "AreanaEntrancePanel/AreanaRankingPanel/OtherUserPartyInfoPanel":
			user = commData.GetHighRankUsers() [userIndex];
			break;

		case "ShamBattleEntrancePanel/AttackPartyEditPanel/CharacterInfoPanel":
			user = userData.GetUser();
			break;
		case "AreanaEntrancePanel/AttackPartyEditPanel/CharacterInfoPanel":
			user = commData.GetAreanaUsers() [userIndex];
			break;
		case "AreanaEntrancePanel/AreanaRankingPanel/OtherUserPartyInfoPanel/CharacterInfoPanel":
			user = commData.GetHighRankUsers() [userIndex];
			break;
		case "AreanaEntrancePanel/OtherUserPartyInfoPanel/CharacterInfoPanel":
			user = commData.GetAreanaUsers() [userIndex];
			break;

		case "ShamBattleEntrancePanel/AttackPartyEditPanel/FormationInfoPanel":
			user = userData.GetUser();
			break;
		case "ShamBattleEntrancePanel/DefensePartyEditPanel/ChangePartyPanel/FormationInfoPanel":
			user = userData.GetUser();
			break;
		case "AreanaEntrancePanel/AreanaRankingPanel/OtherUserPartyInfoPanel/FormationInfoPanel":
			user = commData.GetHighRankUsers() [userIndex];
			break;
		case "AreanaEntrancePanel/OtherUserPartyInfoPanel/FormationInfoPanel":
			user = commData.GetAreanaUsers() [userIndex];
			break;
		case "AreanaEntrancePanel/DefensePartyEditPanel/ChangePartyPanel/FormationInfoPanel":
			user = userData.GetUser();
			break;
		case "AreanaEntrancePanel/AttackPartyEditPanel/FormationInfoPanel":
			user = commData.GetAreanaUsers() [userIndex];
			break;

		}

		return user;
	}

	public CharInfo GetCharacterAtSpecialCase (string specialCase) {
		Debug.LogFormat ("charIndex: {0}", charIndex);

		User user = null;
		CharInfo character = null;

		switch (specialCase) {
		case "ShamBattleEntrancePanel/AttackPartyEditPanel/CharacterInfoPanel":
			user = GetUserAtSpecialCase (specialCase);
			character = user.GetCharSet (PARTY_TYPE.ShamDef, 0) [charIndex];
			break;
		case "AreanaEntrancePanel/AttackPartyEditPanel/CharacterInfoPanel":
			user = GetUserAtSpecialCase (specialCase);
			character = user.GetCharSet (PARTY_TYPE.AreanaDef, 0) [charIndex];
			break;
		case "AreanaEntrancePanel/AreanaRankingPanel/OtherUserPartyInfoPanel/CharacterInfoPanel":
			user = GetUserAtSpecialCase (specialCase);
			character = user.GetCharSet (PARTY_TYPE.AreanaDef, 0) [charIndex];
			break;
		case "AreanaEntrancePanel/OtherUserPartyInfoPanel/CharacterInfoPanel":
			user = GetUserAtSpecialCase (specialCase);
			character = user.GetCharSet (PARTY_TYPE.AreanaDef, 0) [charIndex];
			break;

		}

		return character;
	}





	/*
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
			return commData.GetAreanaUsers (false) [selectedOtherUser];
		} else if (selectedEventPanel.Equals (PANEL_TYPE.AreanaRanking)) {
			return commData.m_highRankUsers[selectedOtherUser];
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
	*/





}