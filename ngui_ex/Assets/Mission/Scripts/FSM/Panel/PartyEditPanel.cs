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



	//private int realItemMaxIndex; // note: = realItemList.Count;
	//private List<string> realItemList;
	//private Dictionary<int, int> formationMemberDic = new Dictionary<int, int> (); //charID, formationPos

	//private const int MAX_UI_ITEM_COUNT = 20; //5 rows and 4 cols.
	//private UILabel[] uiItemList = new UILabel[MAX_UI_ITEM_COUNT];
	//public GameObject wrapContent; // TODO: please delete this parameter.

	//private const int MAX_UI_FORMATION_ITEM_COUNT = 24; //6 rows and 4 cols.
	//private UISprite[] uiFromationItemList = new UISprite[MAX_UI_FORMATION_ITEM_COUNT];
	//private UILabel[] uiFormationItemLabelList = new UILabel[MAX_UI_FORMATION_ITEM_COUNT];
	//public GameObject formationRoot; // TODO: please delete this parameter.

	//public Transform t;
	//public Camera cam;
	//public TestTween testTween; // TODO: please delete this parameter.
	//public GameObject tweenPrefab;
	//public Vector2[] formationTweenPosList = new Vector2[MAX_UI_FORMATION_ITEM_COUNT];

	private List<CharInfo> realCharList;
	private List<CharacterCell> wrapCharList;

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


				Debug.Log (realCharList [realIndex].cost);
				wrapCharList [wrapIndex].Set (realCharList [realIndex], realIndex);

				// if formation! check!
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
			}
			*/
		}
	}

	void OnEnable() {
		Debug.Log ("Enable");

		// set real character list.
		Dictionary<int, CharInfo> charDic = GlobalApp.Inst.userData.m_user.m_characterDic;
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

		for (int i = 0; i < form.m_Form.Length; i++) {
			int charIndex = form.m_Form [i];
			if (charIndex == -1) {
				formCellList [i].Set (i, "UISliderBG");
			} else {
				if (charSet[charIndex] == null)
					formCellList [i].Set (i, "btn_9slice_pressed");
				else 	
					formCellList [i].Set (i, charSet [charIndex].spriteName);
			}
		}

	}
	//int[] form = GlobalApp.Inst.userData.m_user.m_formList[(int)PARTY_TYPE.Dungeon].m_Form;

	public void dddStart() {
		//realItemList = GlobalApp.Inst.userData.m_user.GetCharacterKeyList (); // GameData.Inst.GetCharacterKeyList (); realItemMaxIndex = realItemList.Count;
		//formationMemberDic = GameData.Inst.GetFormationMemberDic ();
		/*
		foreach (CharacterCell charCell in wrapContent.GetComponentsInChildren<CharacterCell> ()) {
			if (formationMemberDic.ContainsKey (index)) {
				uiLabel.text = realItemList [index] + " Ready";
			} else {
				uiLabel.text = realItemList [index];
			}
			uiItemList [index] = uiLabel;
			index++;
		}
		*/
		/* when inventory was dragged, this event function will be call. */
		/* fucntion: inject character information to each item slot. */
		/*
		wrapContent.GetComponent<UIWrapContent> ().onInitializeItem = delegate(GameObject go, int wrapIndex, int realIndex) {
			realIndex = Mathf.Abs(realIndex);
			int realItemIndex = realIndex * 4;
			int uiItemIndex = wrapIndex * 4;

			for (int i = 0; i < 4; i++) {
				if (realItemIndex <= realItemMaxIndex) {
					//Debug.Log(uiItemList[uiItemIndex].text);
					//Debug.Log(realItemList[realItemIndex]);
					//Debug.Log("");
					if (formationMemberDic.ContainsKey (realItemIndex)) {
						uiItemList[uiItemIndex++].text = realItemList [realItemIndex++] + " Ready";
					} else {
						uiItemList[uiItemIndex++].text = realItemList[realItemIndex++];
					}
				}
			}
		};
	*/
	/*
		index = 0;
		foreach (UISprite uiSprite in formationRoot.GetComponentsInChildren<UISprite> ()) {
			uiFromationItemList [index] = uiSprite;
			uiFormationItemLabelList[index] = uiFromationItemList [index].GetComponentInChildren<UILabel> ();

			formationTweenPosList [index] = uiSprite.transform.position;
			index++;
		}

		CheckFormation ();
		*/
	}

	public void CheckFormation() {
	
	}
	/*
	public void CheckFormation() {
		Debug.Log ("CheckFormation");

		int[,] formation = GameData.Inst.GetFormation ();

		formationMemberDic = GameData.Inst.GetFormationMemberDic (); //edit
		// uiItemList
		for (int i = 0; i < MAX_UI_ITEM_COUNT; i++) {
			//Debug.Log (uiItemList[i].name);
			string charId = uiItemList [i].text;
			charId = charId.Replace (" Ready", "");
			int id = Int32.Parse (charId);
			if (formationMemberDic.ContainsKey (id)) {
				uiItemList [i].text = realItemList [id] + " Ready";
			} else {
				uiItemList [i].text = realItemList [id];
			}
		}

		int formationItem;
		for (int i = 0; i < MAX_UI_FORMATION_ITEM_COUNT; i++) {
			formationItem = formation [i / 4, i % 4];
			if (formationItem == -2) {	// empty and can enter.
				uiFromationItemList [i].spriteName = "UISliderBG";
				uiFromationItemList [i].color = Color.clear;
				uiFormationItemLabelList [i].text = i.ToString();
			} else if (formationItem == -1) { // empty and can't enter.
				uiFromationItemList [i].spriteName = "UISliderBG";
				uiFromationItemList [i].color = Color.black;
				uiFormationItemLabelList [i].text = "X";
			} else {
				switch (formationItem) {
				case 0:
				case 4:
				case 8:
				case 12:
				case 16:
					uiFromationItemList [i].spriteName = "IconHellephant";
					break;
				case 1:
				case 5:
				case 9:
				case 13:
				case 17:
					uiFromationItemList [i].spriteName = "IconPlayer";
					break;
				case 2:
				case 6:
				case 10:
				case 14:
				case 18:
					uiFromationItemList [i].spriteName = "IconZomBear";
					break;
				default:
					uiFromationItemList [i].spriteName = "IconZomBunny";
					break;
				}
				uiFromationItemList [i].color = Color.white;
				uiFormationItemLabelList [i].text = formationItem.ToString();
			}	
		}

	}
	*/
	/*
	public override void OnClickXXXBtn(string btnName) {
		switch (btnName) {
		case "btn_back":
			GuiMgr.GetInst ().PopPnl (PANEL_TYPE.ChapterMap);
			break;
		case "btn_edit_formation":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.FormationEdit, false);
			break;
		case "btn_forward_to_battle":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.Battle);
			break;
		}
	}

	public void ClickFormationItemBtn(UnityEngine.Object formationItemObject) {
		string s = formationItemObject.name;
		int a = s.LastIndexOf ('_') + 1;
		s = s.Substring (a, s.Length - a);

		int formationPos = Int32.Parse (s);

		int characterId;
		if (Extensions.TryGetKey (formationMemberDic, formationPos, out characterId)) {
			// release at formation and inventory.
			GameData.Inst.SetFormationItem (formationPos / 4 , formationPos % 4, -2);

			CheckFormation ();
		}
	}

	public void ClickItemBtn(GameObject btn) { // GameObject object
		
		string charId = btn.GetComponentInChildren<UILabel> ().text;

		Debug.Log (charId);
		if (!charId.Contains (" Ready")) { // insert.
			int id = Int32.Parse (charId);
		
			int targetPos = GameData.Inst.GetFormationFirstEmptyPos ();
			if (targetPos != -1) {
				GameData.Inst.SetFormationItem (targetPos / 4, targetPos % 4, id);

				GameObject tweenObj = tweenRoot.AddChild (tweenPrefab);//Instantiate (tweenPrefab);
				tweenQueue.Enqueue (tweenObj);
				tweenObj.transform.position = btn.transform.position;
				tweenObj.transform.DOScale (new Vector3(112f/240f, 112f/240f, 0f), 0.3f);
				DOTween.ToAlpha (() => tweenObj.GetComponentInChildren<UISprite> ().color,
					x => tweenObj.GetComponentInChildren<UISprite> ().color = x,
					0f,
					0.3f).SetEase (Ease.InQuad).OnComplete (CheckFormation);
				//tweenObj.transform.DOLocalMove (formationTweenPosList [targetPos], 0.3f);
				tweenObj.transform.DOMove (formationTweenPosList[targetPos], 0.3f).OnComplete(Destroy);

				//Vector2 itemPos = NGUIMath.WorldToLocalPoint (btn.transform.position, Camera.main, UICamera.currentCamera, this.transform);
				/*TweenScale tweenScale = *//*TweenScale.Begin (tweenObj, 0.3f, new Vector3(112f/240f, 112f/240f, 0f));
				//tweenScale.value = Vector3.one;
				TweenAlpha tweenAlpha = TweenAlpha.Begin (tweenObj, 0.3f, 0f);
				//tweenAlpha.value = 1f;
				tweenAlpha.steeperCurves = true;
				tweenAlpha.method = UITweener.Method.EaseIn;
				tweenAlpha.eventReceiver = gameObject;
				tweenAlpha.callWhenFinished = "Destroy";

				TweenPosition tweenPostion = TweenPosition.Begin (tweenObj, 0.3f, formationTweenPosList[targetPos]);
				tweenPostion.from = itemPos;
				tweenPostion.eventReceiver = gameObject;
				tweenPostion.callWhenFinished = "CheckFormation";*/
	/*
			} else {
				Debug.Log ("please make full popup msg.");
			}
		} else { // release.
			string sId = charId.Replace (" Ready", "");
			int id = Int32.Parse (sId);

			int targetPos = formationMemberDic [id];
			GameData.Inst.SetFormationItem (targetPos / 4 , targetPos % 4, -2);
			CheckFormation ();
		}


	}

	private void Destroy() {
		Destroy (tweenQueue.Dequeue ());
	}

	public GameObject tweenRoot;
	public Queue<GameObject> tweenQueue = new Queue<GameObject>();
	*/
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