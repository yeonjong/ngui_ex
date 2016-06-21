using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class AreanaRecordPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
			GuiMgr.GetInst ().PopPnl ();
			break;

		case "btn_social_share":
			// TODO: social
			Debug.Log("todo");
			break;

		case "btn_record_review_check":
			//TODO:
			// please save selected data
			Debug.Log("todo");
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaRecordReviewCheck, false);
			break;
		}
	}


	private UILabel userInfoLabel;

	private RecordCell[] records;

	// TODO: you must sort record array (recent top);

	void Awake() {
		userInfoLabel = transform.FindChild ("user_info/lbl_info1").GetComponent<UILabel>();

		records = new RecordCell[FixedConstantValue.RECORD_CELL_NUM];
		records [0] = transform.FindChild ("record/scv_record_scroll_view/wrap_content/spr_cell_0").GetComponent<RecordCell>();
		records [1] = transform.FindChild ("record/scv_record_scroll_view/wrap_content/spr_cell_1").GetComponent<RecordCell>();
		records [2] = transform.FindChild ("record/scv_record_scroll_view/wrap_content/spr_cell_2").GetComponent<RecordCell>();
		records [3] = transform.FindChild ("record/scv_record_scroll_view/wrap_content/spr_cell_3").GetComponent<RecordCell>();
	}

	void OnEnable() {
		StringBuilder sb = new StringBuilder ();
		User user = GlobalApp.Inst.userData.GetUser();
		sb.AppendFormat ("Rank {0}\nPower {1}",user.m_nAreanaRank, user.GetPartyFightingPower(PARTY_TYPE.AreanaAtk));
		userInfoLabel.text = sb.ToString ();

		List<RecordInfo> recordList = GlobalApp.Inst.commData.GetRecordList();
		for (int i = 0; i < FixedConstantValue.RECORD_CELL_NUM; i++) {
			records [i].Set (recordList[i]);
		}
	}

}
