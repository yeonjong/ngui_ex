using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefensePartyEditPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
		case "btn_x":
			GuiMgr.GetInst ().PopPnl ();
			break;
		case "btn_edit_formation":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.FormationEdit, false);
			break;
		case "btn_auto_form":
			// TODO: auto form.
			Debug.Log("todo");
			break;
		case "btn_save":
			// TODO: save logic
			Debug.Log ("todo");
			GuiMgr.GetInst ().PopPnl ();
			break;
		}
	}

	private UIWrapContent partyScvWrapContent; // set range limit & use item generate root

	private GameObject[] rows;

	private Dictionary<int, CharInfo> charDic;
	//private int maxCharCount; // = charDic.count;

	void Awake() {
		
		partyScvWrapContent = transform.FindChild ("party/scv_party_scroll_view/wrap_content").GetComponent<UIWrapContent>();

		/*
		rows = new GameObject[FixedConstantValue.PARTY_SCV_ROW_NUM];
		rows [0] = partyScvWrapContent.transform.FindChild ("row_0").gameObject;
		rows [1] = partyScvWrapContent.transform.FindChild ("row_1").gameObject;
		rows [2] = partyScvWrapContent.transform.FindChild ("row_2").gameObject;
		rows [3] = partyScvWrapContent.transform.FindChild ("row_3").gameObject;
		rows [4] = partyScvWrapContent.transform.FindChild ("row_4").gameObject;
		*/



	}

	void OnEnable() {
		charDic = GlobalApp.Inst.userData.GetUser().m_charDicByID;

		/*
		// set endless scroll range.
		int temp = ((charDic.Count - 1) / 4) * -1;
		partyScvWrapContent.minIndex = temp;

		// set endless scroll row. (max 5)
		if (charDic.Count == 0) {
			for (int i = 0; i < rows.Length; i++) {
				rows [i].SetActive (false);
			}
		} else if (temp <= 0) {
			if (temp < -4) {
				temp = -4; // because max 4 row.
			}
			for (int i = 0; i >= -4; i--) {
				if (i >= temp)
					rows [i * -1].SetActive (true);
				else
					rows [i * -1].SetActive (false);
			}
		}
		*/

		/*
		partyScvWrapContent.onInitializeItem = delegate(GameObject go, int wrapIndex, int realIndex) {
			realIndex = Mathf.Abs(realIndex);
			int realItemIndex = realIndex * 4;
			int uiItemIndex = wrapIndex * 4;

			for (int i = 0; i < FixedConstantValue.PARTY_SCV_COL_NUM; i++) {
				if (realItemIndex <=charDic.Count) {
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

	}

	/*
	void OnDisable() {
		if (rowPrefabList != null) {
			for (int i = 0; i < rowPrefabList.Count; i++) {
				Destroy (rowPrefabList[i]);
				rowPrefabList [i] = null;
			}
			rowPrefabList = null;
		}
	}
	*/
}
