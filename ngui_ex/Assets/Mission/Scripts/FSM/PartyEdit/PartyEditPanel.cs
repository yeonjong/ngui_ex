using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;

public class PartyEditPanel : MonoBehaviour {

	private int realItemMaxIndex;
	private List<string> realItemList;
	private Dictionary<int, int> formationMemberDic = new Dictionary<int, int> (); //charID, formationPos

	private const int MAX_UI_ITEM_COUNT = 20; //5 rows and 4 cols.
	private UILabel[] uiItemList = new UILabel[MAX_UI_ITEM_COUNT];
	public GameObject wrapContent; // TODO: please delete this parameter.

	private const int MAX_UI_FORMATION_ITEM_COUNT = 24; //6 rows and 4 cols.
	private UISprite[] uiFromationItemList = new UISprite[MAX_UI_FORMATION_ITEM_COUNT];
	private UILabel[] uiFormationItemLabelList = new UILabel[MAX_UI_FORMATION_ITEM_COUNT];
	public GameObject formationRoot; // TODO: please delete this parameter.

	//public Transform t;
	public Camera cam;
	public TestTween testTween; // TODO: please delete this parameter.
	public GameObject tweenPrefab;
	public Vector2[] formationTweenPosList = new Vector2[MAX_UI_FORMATION_ITEM_COUNT];

	public void Start() {
		realItemList = GameData.Inst.GetCharacterKeyList ();
		realItemMaxIndex = realItemList.Count;

		formationMemberDic = GameData.Inst.GetFormationMemberDic ();
		int index = 0;
		foreach (UILabel uiLabel in wrapContent.GetComponentsInChildren<UILabel> ()) {
			if (formationMemberDic.ContainsKey (index)) {
				uiLabel.text = realItemList [index] + " Ready";
			} else {
				uiLabel.text = realItemList [index];
			}
			uiItemList [index] = uiLabel;
			index++;
		}

		/* when inventory was dragged, this event function will be call. */
		/* fucntion: inject character information to each item slot. */
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

		index = 0;
		foreach (UISprite uiSprite in formationRoot.GetComponentsInChildren<UISprite> ()) {
			uiFromationItemList [index] = uiSprite;
			uiFormationItemLabelList[index] = uiFromationItemList [index].GetComponentInChildren<UILabel> ();

			formationTweenPosList [index] = uiSprite.transform.position;
			/*
			#if UNITY_EDITOR
			formationTweenPosList [index] = NGUIMath.WorldToLocalPoint (uiSprite.transform.position, Camera.main, cam, this.transform);
			#elif
			formationTweenPosList [index] = NGUIMath.WorldToLocalPoint (uiSprite.transform.position, Camera.main, UICamera.currentCamera, this.transform);
			#endif
			*/
			index++;
		}

		CheckFormation ();
	}

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

	public void ClickForwardToChapterMapBtn() {
		GameStateMgr.GetInst().ForwardState(GAME_STATE.ChapterMapState);
	}

	public void ClickForwardToFormationEditBtn() {
		GameStateMgr.GetInst().ForwardState(GAME_STATE.FormationEditState);
	}

	public void ClickForwardToBattleBtn() {
		GameStateMgr.GetInst().ForwardState(GAME_STATE.BattleState);
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