using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;

public class PartyEditPanel : PanelBase {

	public override void OnClickXXXBtn (string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
			GuiMgr.GetInst ().PopPnl ();
			break;
		case "btn_edit_formation":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.FormationEdit, false);
			break;
		case "btn_start_battle":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.Battle);
			break;
		default:
			int formNum = Int32.Parse (btnName);
			ChangeFormation (formNum);
			break;
		}
	}

	public override void OnClickXXXCell(CELL_TYPE cellType, int cellIndex) {
		Debug.LogFormat ("{0}: {1}", cellType, cellIndex);

		switch (cellType) {
		case CELL_TYPE.character:
			break;
		case CELL_TYPE.formation:
			break;
		}
	}
		
	private List<CharInfo> realCharList;
	private List<CharacterCell> wrapCharList;

	private Dictionary<int, int> formPosDicByCharID;

	private UIScrollView m_scrollView;
	private UIWrapContent m_wrapContent;
	private GameObject[] wrapRows;

	private List<FormationCell> formCellList;
	CharInfo[] charSet;

	private int selectedParty = 0;

	void Awake() {
		// set wrap character list.
		Transform wrapContent = transform.FindChild ("party/scv_party_scroll_view/wrap_content");
		wrapCharList = new List<CharacterCell>();
		foreach (CharacterCell charCell in wrapContent.GetComponentsInChildren<CharacterCell> ()) {
			charCell.SetBtnsTarget (this);
			wrapCharList.Add (charCell);
		}

		// set endless scroll view's rows.
		m_scrollView = transform.FindChild("party/scv_party_scroll_view").GetComponent<UIScrollView>();
		m_wrapContent = wrapContent.GetComponent<UIWrapContent> ();
		wrapRows = new GameObject[FixedConstantValue.PARTY_SCV_ROW_NUM];
		wrapRows [0] = wrapContent.FindChild ("row_0").gameObject;
		wrapRows [1] = wrapContent.FindChild ("row_1").gameObject;
		wrapRows [2] = wrapContent.FindChild ("row_2").gameObject;
		wrapRows [3] = wrapContent.FindChild ("row_3").gameObject;

		// set formation cell list.
		formCellList = new List<FormationCell> ();
		Transform formRoot = transform.FindChild ("formation/btn_formation_items");
		foreach (FormationCell formCell in formRoot.GetComponentsInChildren<FormationCell>()) {
			formCell.SetBtnsTarget (this);
			formCellList.Add (formCell);
		}
	}

	private void OnWrapContentChanged(int wrapIndex, int realIndex) {
		Debug.Log ("OnWrapContentChanged()");
		
		realIndex = Mathf.Abs(realIndex) * 4;
		wrapIndex = wrapIndex * 4;
		Debug.LogFormat ("RealIdx {0}, WrapIdx {1}", realIndex, wrapIndex);

		for (int i = 0; i < 4; i++) {
			if (realIndex < realCharList.Count) {
				//Debug.Log (realCharList [realIndex].cost);
				int id = realCharList [realIndex].id;
				if (formPosDicByCharID.ContainsKey (id)) {
					wrapCharList [wrapIndex].Set (realCharList [realIndex], realIndex, true);
				} else {
					wrapCharList [wrapIndex].Set (realCharList [realIndex], realIndex);
				}
			} else {
				wrapCharList [wrapIndex].Set (null, realIndex);
			}
			wrapIndex++;
			realIndex++;

			/*
				//Debug.Log(uiItemList[uiItemIndex].text);
				//Debug.Log(realItemList[realItemIndex]);
				//Debug.Log("");
				if (formationMemberDic.ContainsKey (realItemIndex)) {
					uiItemList[uiItemIndex++].text = realItemList [realItemIndex++] + " Ready";
				} else {
					uiItemList[uiItemIndex++].text = realItemList[realItemIndex++];
				}
			*/
		}
	}

	void OnEnable() {
		Debug.Log ("Enable");
		// set real character list.
		Dictionary<int, CharInfo> charDic = GlobalApp.Inst.userData.m_user.m_charDicByID;
		realCharList = new List<CharInfo> (charDic.Values);
		
		// set endless scroll view's rows.
		int minIndex = ((realCharList.Count - 1) / 4) * -1;
		if (minIndex == 0) {
			wrapRows [3].SetActive (false);
			wrapRows [2].SetActive (false);
			wrapRows [1].SetActive (true);
			wrapRows [0].SetActive (true);
		}
		else {
			for (int i = 0; i < FixedConstantValue.PARTY_SCV_ROW_NUM; i++) {
				if (i > minIndex * -1)
					wrapRows [i].SetActive (false);
				else
					wrapRows [i].SetActive (true);
			}
		}
		m_wrapContent.minIndex = minIndex;

		if (m_wrapContent.onInitializeItem == null) {
			// add event function for updating endless scroll view's each row & set contents.
			m_wrapContent.onInitializeItem = delegate(GameObject go, int wrapIndex, int realIndex) {
				OnWrapContentChanged(wrapIndex, realIndex);
			};
		} else {
			// reset contents & reset scv position.
			m_wrapContent.SortBasedOnScrollMovement();
			
			Vector3 pos = Vector3.zero;
			for (int i = 0; i < wrapRows.Length; i++) {
				pos.y = i * -240;
				wrapRows [i].transform.localPosition = pos;
			}

			m_scrollView.ResetPosition ();
		}

		// set formation cell list.
		Party dungeonParty = GlobalApp.Inst.userData.m_user.GetParty(PARTY_TYPE.Dungeon);
		CharInfo[] charSet = dungeonParty.m_charSetList [selectedParty];
		FormInfo form = dungeonParty.m_formList [selectedParty];

		formPosDicByCharID = new Dictionary<int, int> ();
		int charIndex = 0;
		for (int i = 0; i < form.m_Form.Length; i++) {
			if (charIndex >= charSet.Length)
				break;
			
			Debug.LogFormat ("{0}, {1}", i, charIndex);
			Debug.Log (form.m_Form[i]);
			if (form.m_Form [i] != -1 && charSet[charIndex] != null) {
				formPosDicByCharID.Add (charSet[charIndex++].id, i);
			}
		}

		for (int i = 0; i < form.m_Form.Length; i++) {
			charIndex = form.m_Form [i];
			if (charIndex == -1) {
				formCellList [i].Set (i, "UISliderBG");
			} else {
				if (charSet [charIndex] == null)
					formCellList [i].Set (i, "btn_9slice_pressed");
				else
					formCellList [i].Set (i, charSet [charIndex].spriteName);
			}
		}

	}

	public void ChangeFormation(int formNum) {
		GlobalApp.Inst.userData.m_user.SetFormation (PARTY_TYPE.Dungeon, selectedParty, formNum);

		Party dungeonParty = GlobalApp.Inst.userData.m_user.GetParty (PARTY_TYPE.Dungeon);
		CharInfo[] charSet = dungeonParty.m_charSetList [selectedParty];
		FormInfo form = dungeonParty.m_formList [selectedParty];

		for (int i = 0; i < form.m_Form.Length; i++) {
			int charIndex = form.m_Form [i];
			if (charIndex == -1) {
				formCellList [i].Set (i, "UISliderBG");
			} else {
				if (charSet [charIndex] == null)
					formCellList [i].Set (i, "btn_9slice_pressed");
				else
					formCellList [i].Set (i, charSet [charIndex].spriteName);
			}
		}

	}

}


public static class Extensions {

	public static bool TryGetKey<K, V>(this IDictionary<K, V> instance, V value, out K key)
	{
		foreach (var entry in instance)
		{
			if (!entry.Value.Equals(value))
			{
				continue;
			}
			key = entry.Key;
			return true;
		}
		key = default(K);
		return false;
	}

}