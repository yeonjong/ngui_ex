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
			CheckCharCell (cellIndex);
			break;
		case CELL_TYPE.formation:
			CheckFormCell (cellIndex);
			break;
		}
	}

	private List<CharInfo> realCharList;
	private List<CharacterCell> wrapCharList;
	private List<FormationCell> formCellList;

	int minRowIndex = 0;
	int maxRowIndex = 0;


	//Dictionary<int, CharInfo> charDicByID; //= GlobalApp.Inst.userData.m_user.m_charDicByID;
	//Party dungeonParty; //= GlobalApp.Inst.userData.m_user.GetParty(PARTY_TYPE.Dungeon);
	CharInfo[] charSet; //= dungeonParty.m_charSetList [selectedParty];
	FormInfo form; //= dungeonParty.m_formList [selectedParty];


	//private Dictionary<int, int> formPosDicByCharID;
	//private Dictionary<int, int> charIDDicByFormPos;
	//private Dictionary<int, int> charSetIndexByFormPos;
	//private Dictionary<int, int> charCellListIndexByformPos;

	Dictionary<int, int> formCellListIndexByCharID = new Dictionary<int, int> ();
	Dictionary<int, int> charIDDicByFormCellListIndex = new Dictionary<int, int> ();
	Dictionary<int, int> charCellListIndexByCharID = new Dictionary<int, int> ();
	//Dictionary<int, int> charSetIndexByCharID = new Dictionary<int, int> ();


	private UIScrollView m_scrollView;
	private UIWrapContent m_wrapContent;
	private GameObject[] wrapRows;

	private GameObject tweenPrefab;
	private Queue<GameObject> tweenQueue;
	private List<Vector2> formCellPosList;


	private int selectedParty = 0; //TODO: maybe add ui tab and use this variable.

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
		formCellPosList = new List<Vector2> ();
		Transform formRoot = transform.FindChild ("formation/btn_formation_items");
		foreach (FormationCell formCell in formRoot.GetComponentsInChildren<FormationCell>()) {
			formCell.SetBtnsTarget (this);
			formCellList.Add (formCell);

			formCellPosList.Add (formCell.transform.position);
		}

		// set tween item.
		tweenPrefab = transform.FindChild("pnl_tween_item").gameObject;
		tweenQueue = new Queue<GameObject> ();
	}

	void OnEnable() {
		// set real character list.
		//charDicByID = GlobalApp.Inst.userData.m_user.m_charDicByID;
		realCharList = GlobalApp.Inst.userData.GetUser().GetCharacterList ();
		
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
		Party dungeonParty = GlobalApp.Inst.userData.GetUser().GetParty(PARTY_TYPE.Dungeon);
		charSet = dungeonParty.m_charSetList [selectedParty];
		form = dungeonParty.m_formList [selectedParty];


		//formPosDicByCharID = new Dictionary<int, int> ();
		//charIDDicByFormPos = new Dictionary<int, int> ();

		formCellListIndexByCharID = new Dictionary<int, int> ();
		charIDDicByFormCellListIndex = new Dictionary<int, int> ();
		charCellListIndexByCharID = new Dictionary<int, int> ();
		//charSetIndexByCharID = new Dictionary<int, int> ();

		// charcelllist index => charid // from realCharlist.
		// charid => fromcelllist index // from formCellListIndexByCharID.

		// formcelllist index => charid // from charIDDicByFormCellListIndex == charIDDicByFormPos.
		// charid => charcelllist index // from charCellListIndexByCharID
		// charid => charset index		// form charSetIndexByCharID

		for (int i = 0; i < formCellList.Count; i++) {
			if (form.m_Form [i] != -1) {
				if (charSet [form.m_Form[i]] != null) {
					formCellListIndexByCharID.Add (charSet [form.m_Form[i]].id, i);
					charIDDicByFormCellListIndex.Add (i, charSet [form.m_Form[i]].id);
					int charCellListIndex = -1;
					for (int j = 0; j < realCharList.Count; j++) {
						if (realCharList [j].id == charSet [form.m_Form [i]].id) {
							charCellListIndex = j;
						}
					}
					charCellListIndexByCharID.Add (charSet [form.m_Form[i]].id, charCellListIndex);
					//charSetIndexByCharID.Add (charSet [form.m_Form[i]].id, form.m_Form[i]);
				}
			}
		}

		for (int i = 0; i < form.m_Form.Length; i++) {
			SetFormCell (i);
		}
	}

	public void ChangeFormation(int formNum) {
		GlobalApp.Inst.userData.GetUser().SetFormation (PARTY_TYPE.Dungeon, selectedParty, formNum);

		Party dungeonParty = GlobalApp.Inst.userData.GetUser().GetParty (PARTY_TYPE.Dungeon);
		charSet = dungeonParty.m_charSetList [selectedParty];
		form = dungeonParty.m_formList [selectedParty];

		for (int i = 0; i < form.m_Form.Length; i++) {
			int charIndex = form.m_Form [i];
			if (charIndex == -1) {
				formCellList [i].Set (i, FixedConstantValue.BLOCK_SPRITE_NAME);
			} else {
				if (charSet [charIndex] == null)
					formCellList [i].Set (i, FixedConstantValue.EMPTY_SPRITE_NAME);
				else
					formCellList [i].Set (i, charSet [charIndex].spriteName);
			}
		}


		formCellListIndexByCharID = new Dictionary<int, int> ();
		charIDDicByFormCellListIndex = new Dictionary<int, int> ();
		//charSetIndexByCharID = new Dictionary<int, int> ();

		// charcelllist index => charid // from realCharlist.
		// charid => fromcelllist index // from formCellListIndexByCharID.

		// formcelllist index => charid // from charIDDicByFormCellListIndex == charIDDicByFormPos.
		// charid => charcelllist index // from charCellListIndexByCharID
		// charid => charset index		// form charSetIndexByCharID

		for (int i = 0; i < formCellList.Count; i++) {
			if (form.m_Form [i] != -1) {
				if (charSet [form.m_Form[i]] != null) {
					formCellListIndexByCharID.Add (charSet [form.m_Form[i]].id, i);
					charIDDicByFormCellListIndex.Add (i, charSet [form.m_Form[i]].id);
				}
			}
		}

	}

	//GlobalApp.Inst.userData.m_user.SetFormation (PARTY_TYPE.Dungeon, selectedParty, formNum);
	private void CheckFormCell(int cellIndex) {
		if (charIDDicByFormCellListIndex.ContainsKey (cellIndex)) {
			Debug.LogFormat ("charID{0} contains at {1}", charIDDicByFormCellListIndex[cellIndex], cellIndex);

			//release character from charSet, ui/formcell, ui/charcell
			int charSetIndex = form.m_Form[cellIndex];
			GlobalApp.Inst.userData.GetUser().GetParty(PARTY_TYPE.Dungeon).m_charSetList [selectedParty][charSetIndex] = null;
			charSet[charSetIndex] = null;

			int charID = charIDDicByFormCellListIndex[cellIndex];
			formCellListIndexByCharID.Remove (charID);

			int charCellIndex = charCellListIndexByCharID[charID];
			SetFormCell (cellIndex);
			SetCharCell (charCellIndex);

			charIDDicByFormCellListIndex.Remove (cellIndex);
			charCellListIndexByCharID.Remove (charID);
			//charSetIndexByCharID.Remove (charID);
		} else {
			Debug.Log ("char not contains");
		}
	}

	private void CheckCharCell (int cellIndex) {
		int charID = realCharList [cellIndex].id;

		if (formCellListIndexByCharID.ContainsKey (charID)) {
			Debug.LogFormat ("in form pos{0}, charID{1}", formCellListIndexByCharID[ realCharList [cellIndex].id ], realCharList [cellIndex].id);
			// release formCell, active charCell // release from charSet

			int formCellListIndex = formCellListIndexByCharID [charID];
			int charSetIndex = form.m_Form [formCellListIndex];

			GlobalApp.Inst.userData.GetUser().GetParty(PARTY_TYPE.Dungeon).m_charSetList [selectedParty][charSetIndex] = null;
			charSet[charSetIndex] = null;

			formCellListIndexByCharID.Remove (charID);

			SetFormCell (formCellListIndex);
			SetCharCell (cellIndex);

			charIDDicByFormCellListIndex.Remove (formCellListIndex);
			charCellListIndexByCharID.Remove (charID);
		
		} else {
			Debug.Log ("out form");

			// regist formCell, disable charCell // regist to charSet
			int firstEmptyCharCellIndex = -1; //rename
			for (int i = 0; i < charSet.Length; i++) {
				if (charSet [i] == null) {
					firstEmptyCharCellIndex = i;
					break;
				}
			}

			if (firstEmptyCharCellIndex == -1) {
				Debug.Log ("full form cell list");
			} else {
				Debug.LogFormat ("can regist to {0}", firstEmptyCharCellIndex);

				if (checkDic.ContainsKey (cellIndex))
					return;
				else
					checkDic.Add (cellIndex, null);

				int formCellListIndex = -1;
				for (int j = 0; j < form.m_Form.Length; j++) {
					if (form.m_Form [j] == firstEmptyCharCellIndex) {
						formCellListIndex = j;
						break;
					}
				}

				int charSetIndex = firstEmptyCharCellIndex;//form.m_Form [formCellListIndex];
				StringBuilder sbbbbbb = new StringBuilder ();
				sbbbbbb.AppendFormat ("CharSetIndex{0}\n", charSetIndex);
				for (int i = 0; i < charSet.Length; i++) {
					if (charSet [i] == null) {
						sbbbbbb.Append ("x, ");
					} else {
						sbbbbbb.AppendFormat ("{0}, ", charSet[i].id);
					}
				}
				Debug.Log (sbbbbbb.ToString());

				CharInfo character = realCharList[cellIndex];
				GlobalApp.Inst.userData.GetUser().GetParty(PARTY_TYPE.Dungeon).m_charSetList [selectedParty][charSetIndex] = character;
				charSet[charSetIndex] = character;

				//
				int currRowIndex = cellIndex / 4 * -1;
				//int wrapRow = Mathf.Abs (currRowIndex - minRowIndex);
				int wrapRow = currRowIndex - minRowIndex;
				wrapRow = Mathf.Abs (wrapRow + minRowIndex % 4) % 4;
				int wrapIndex = wrapRow * 4 + cellIndex % 4;





				GameObject tweenObj = gameObject.AddChild(tweenPrefab);//gameObject.AddChild(tweenPrefab);
				tweenObj.GetComponentInChildren<UISprite> ().spriteName = realCharList[cellIndex].spriteName;
				tweenQueue.Enqueue (tweenObj);
				tweenObj.transform.position = wrapCharList [wrapIndex].transform.position;

				float time = 0.3f;
				tweenObj.transform.DOScale (new Vector3 (112f / 240f, 112f / 240f, 0f), time);//0.3f);
				DOTween.ToAlpha (() => tweenObj.GetComponentInChildren<UISprite> ().color,
					x => tweenObj.GetComponentInChildren<UISprite> ().color = x,
					0f,
					time).SetEase (Ease.InQuad).OnComplete (()=>OnTweenFinished (charID, formCellListIndex, cellIndex));

				Debug.Log (formCellPosList[formCellListIndex]);
				tweenObj.transform.DOLocalMove (formCellPosList[formCellListIndex], time).OnComplete(DestroyTweenObj);
				//tweenObj.transform.DOMove (formCellPosList[formCellListIndex], time).OnComplete(DestroyTweenObj);
				/*
				Sequence seq = DOTween.Sequence ();
				seq.Append (tweenObj.transform.DOScale (new Vector3 (112f / 240f, 112f / 240f, 0f), 0.3f))
					.Insert (0f, tweenObj.transform.DOMove (formCellPosList [formCellListIndex], 0.3f))
					.Insert (0f, DOTween.ToAlpha (() => tweenObj.GetComponentInChildren<UISprite> ().color,
					x => tweenObj.GetComponentInChildren<UISprite> ().color = x,
					0f,
					0.3f));
				seq.OnComplete (()=>OnTweenFinished (charID, formCellListIndex, cellIndex));
				*/




				//
				/*
				formCellListIndexByCharID.Add (charID, formCellListIndex); //.Remove (charID);

				SetFormCell (formCellListIndex);
				SetCharCell (cellIndex);

				charIDDicByFormCellListIndex.Add (formCellListIndex, charID); //Remove (cellIndex);
				charCellListIndexByCharID.Add (charID, cellIndex);//Remove (charID);
				*/
			}
		}
	
	}
	Dictionary<int, object> checkDic = new Dictionary<int, object>(); //cellindex, ""

	private void OnTweenFinished(int charID, int formCellListIndex, int charCellListIndex) {
		formCellListIndexByCharID.Add (charID, formCellListIndex); //.Remove (charID);

		SetFormCell (formCellListIndex);
		SetCharCell (charCellListIndex);

		charIDDicByFormCellListIndex.Add (formCellListIndex, charID); //Remove (cellIndex);
		charCellListIndexByCharID.Add (charID, charCellListIndex);//Remove (charID);

		checkDic.Remove (charCellListIndex);
	}

	private void DestroyTweenObj() {
		Destroy (tweenQueue.Dequeue());
	}

	private void SetFormCell(int cellIndex) {
		int charSetIndex = form.m_Form [cellIndex];
		if (charSetIndex == -1) {
			formCellList [cellIndex].Set (cellIndex, FixedConstantValue.BLOCK_SPRITE_NAME);
		} else {
			if (charSet [charSetIndex] == null)
				formCellList [cellIndex].Set (cellIndex, FixedConstantValue.EMPTY_SPRITE_NAME);
			else
				formCellList [cellIndex].Set (cellIndex, charSet [charSetIndex].spriteName);
		}
	}
	// charcelllist index => charid => charset index

	// charcelllist index => charid // from realCharList [cellIndex].id
	// charid => fromcelllist index // from formCellListIndexByCharID.

	// formcelllist index => charid // from charIDDicByFormCellListIndex == charIDDicByFormPos.
	// charid => charcelllist index // from charCellListIndexByCharID
	// charid => charset index		// form form.m_Form [cellIndex];
	private void SetCharCell(int cellIndex) {
		
		// cellIndex -> rowIndex -> wrapIndex
		//			min 0
		//0~3 -> 0  curr -1
		//			max -3
		//4~7 -> -1
		//8~11 -> -2

		//int charID = charIDDicByFormCellListIndex[cellIndex];
		//int charCellListIndex = charCellListIndexByCharID[charID];

		int currRowIndex = cellIndex / 4 * -1;

		Debug.LogFormat ("charCellListIndex{0}", cellIndex);
		Debug.LogFormat ("curr{0}, min{1}, max{2}", currRowIndex, minRowIndex, maxRowIndex);
		if (currRowIndex <= minRowIndex && currRowIndex >= maxRowIndex) {



			//int wrapRow = Mathf.Abs (currRowIndex - minRowIndex);
			int wrapRow = currRowIndex - minRowIndex;
			wrapRow = Mathf.Abs (wrapRow + minRowIndex % 4) % 4;
			int wrapIndex = wrapRow * 4 + cellIndex % 4;
			//int wrapIndex = Mathf.Abs (currRowIndex - minRowIndex);
			Debug.LogFormat ("need change, wrapIndex{0}", wrapIndex);

			if (formCellListIndexByCharID.ContainsKey (realCharList [cellIndex].id)) {
				Debug.Log ("regist");
				wrapCharList [wrapIndex].Set (realCharList [cellIndex], cellIndex, true);
			} else {
				Debug.Log ("release");
				wrapCharList [wrapIndex].Set (realCharList [cellIndex], cellIndex, false);
			}

		} else {
			Debug.Log ("don't need change");
		}
			
	
	}

	private void OnWrapContentChanged(int wrapIndex, int realIndex) {


		// 0, 0
		// 1, -1
		// 2, -2
		// 3, -3

		// 0, -4 real
		// 1, -5 min
		// 2, -6
		// 3, -7
		// 0, -8 max

		if (realIndex < maxRowIndex) {
			maxRowIndex = realIndex;
			minRowIndex = maxRowIndex + 3;
		} else if (realIndex > minRowIndex) {
			minRowIndex = realIndex;
			maxRowIndex = minRowIndex - 3;
		}
		Debug.LogFormat ("Wrap{0}, Real{1} / MinReal{2}, MaxReal{3}", wrapIndex, realIndex, minRowIndex, maxRowIndex);



		realIndex = Mathf.Abs(realIndex) * 4;
		wrapIndex = wrapIndex * 4;
		//Debug.LogFormat ("update row: RealIdx {0}, WrapIdx {1}", realIndex, wrapIndex);
		
		for (int i = 0; i < 4; i++) {
			if (realIndex < realCharList.Count) {
				//Debug.Log (realCharList [realIndex].cost);
				int id = realCharList [realIndex].id;
				if (formCellListIndexByCharID.ContainsKey (id)) {
					//Debug.LogFormat ("Char realIdx{0}, formPos{1}", realIndex, formPosDicByCharID[id]);
					wrapCharList [wrapIndex].Set (realCharList [realIndex], realIndex, true);
				} else {
					wrapCharList [wrapIndex].Set (realCharList [realIndex], realIndex, false);
				}
			} else {
				wrapCharList [wrapIndex].Set (null, realIndex, false);
			}
			wrapIndex++;
			realIndex++;
		}
	}
	
}