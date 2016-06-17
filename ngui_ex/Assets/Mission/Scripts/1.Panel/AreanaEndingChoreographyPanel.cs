using UnityEngine;
using System.Collections;
using DG.Tweening;

public class AreanaEndingChoreographyPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
		case "btn_ok":
			if (GuiMgr.GetInst ().CheckContainsTargetPanel (PANEL_TYPE.AreanaRecord))
				GuiMgr.GetInst ().PopPnl (PANEL_TYPE.AreanaRecord, PANEL_TYPE.AreanaRecordReviewCheck);
			else if (GuiMgr.GetInst ().CheckContainsTargetPanel (PANEL_TYPE.AreanaEntrance))
				GuiMgr.GetInst ().PopPnl (PANEL_TYPE.AreanaEntrance);
			else if (GuiMgr.GetInst ().CheckContainsTargetPanel (PANEL_TYPE.ShamBattleEntrance))
				GuiMgr.GetInst ().PopPnl (PANEL_TYPE.ShamBattleEntrance);
			else
				Debug.LogError ("this case not implemented");
			break;
		case "btn_review":
			//TODO: you must give review information.
			Debug.Log("todo logic");
			GuiMgr.GetInst ().PopPnl ();
			break;
		}
	}

	Transform[] m_winner;

	UISprite m_modal;

	Transform m_userMainCharacter;
	Transform m_otherUserMainCharacter;

	GameObject[] m_btns;

	void Awake() {
		m_winner = new Transform[6];
		m_winner [0] = transform.FindChild ("lbl_w");
		m_winner [1] = transform.FindChild ("lbl_i");
		m_winner [2] = transform.FindChild ("lbl_n");
		m_winner [3] = transform.FindChild ("lbl_n2");
		m_winner [4] = transform.FindChild ("lbl_e");
		m_winner [5] = transform.FindChild ("lbl_r");

		m_modal = transform.FindChild ("spr_modal").GetComponent<UISprite> ();

		m_userMainCharacter = transform.FindChild ("spr_user_main_character");
		m_otherUserMainCharacter = transform.FindChild ("spr_other_user_main_character");

		m_btns = new GameObject[3];
		m_btns [0] = transform.FindChild ("btn_social_share").gameObject;
		m_btns [1] = transform.FindChild ("btn_review").gameObject;
		m_btns [2] = transform.FindChild ("btn_ok").gameObject;
	}

	void OnEnable() {
		m_winner [0].localScale = new Vector2 (winnerFistSize, winnerFistSize);
		m_winner [1].localScale = new Vector2 (winnerFistSize, winnerFistSize);
		m_winner [2].localScale = new Vector2 (winnerFistSize, winnerFistSize);
		m_winner [3].localScale = new Vector2 (winnerFistSize, winnerFistSize);
		m_winner [4].localScale = new Vector2 (winnerFistSize, winnerFistSize);
		m_winner [5].localScale = new Vector2 (winnerFistSize, winnerFistSize);

		m_winner [0].localPosition = new Vector2 (-537f, 850f);
		m_winner [1].localPosition = new Vector2 (-283f, 850f);
		m_winner [2].localPosition = new Vector2 (-113f, 850f);
		m_winner [3].localPosition = new Vector2 (151f, 850f);
		m_winner [4].localPosition = new Vector2 (392f, 850f);
		m_winner [5].localPosition = new Vector2 (626f, 850f);

		m_modal.color = new Color (1f, 1f, 1f, 1f / 255f);

		m_userMainCharacter.localPosition = new Vector2 (-1700f, 0f);
		m_otherUserMainCharacter.localPosition = new Vector2 (1700f, 0f);

		m_btns [0].SetActive (false);
		m_btns [1].SetActive (false);
		m_btns [2].SetActive (false);

		StartChoreography ();
	}

	public float winnerFistSize = 1.7f;
	public float winnerTime = 0.12f;
	public Ease winnerEase = Ease.OutQuad;

	public float modalAlpha = 100f / 255f;
	public float modalTime = 0.1f;

	public float winnerTime2 = 0.45f;
	public Ease mainCharEase = Ease.InOutCubic;

	public float waitTime = 0.25f;

	Sequence seq;
	void StartChoreography() {
		seq = DOTween.Sequence ();
		seq.Append (m_winner [0].DOLocalMoveY (0f, winnerTime).SetEase (winnerEase))
			.Insert (0f, m_winner [0].DOScale (new Vector3 (1f, 1f, 1f), winnerTime).SetEase (winnerEase));
		seq.Append (m_winner [1].DOLocalMoveY (0f, winnerTime).SetEase (winnerEase))
			.Insert (winnerTime, m_winner [1].DOScale (new Vector3 (1f, 1f, 1f), winnerTime).SetEase (winnerEase));
		seq.Append (m_winner [2].DOLocalMoveY (0f, winnerTime).SetEase (winnerEase))
			.Insert (winnerTime * 2f, m_winner [2].DOScale (new Vector3 (1f, 1f, 1f), winnerTime).SetEase (winnerEase));
		seq.Append (m_winner [3].DOLocalMoveY (0f, winnerTime).SetEase (winnerEase))
			.Insert (winnerTime * 3f, m_winner [3].DOScale (new Vector3 (1f, 1f, 1f), winnerTime).SetEase (winnerEase));
		seq.Append (m_winner [4].DOLocalMoveY (0f, winnerTime).SetEase (winnerEase))
			.Insert (winnerTime * 4f, m_winner [4].DOScale (new Vector3 (1f, 1f, 1f), winnerTime).SetEase (winnerEase));
		seq.Append (m_winner [5].DOLocalMoveY (0f, winnerTime).SetEase (winnerEase))
			.Insert (winnerTime * 5f, m_winner [5].DOScale (new Vector3 (1f, 1f, 1f), winnerTime).SetEase (winnerEase));
	
		seq.Append (DOTween.ToAlpha (() => m_modal.color,
			x => m_modal.color = x,
			modalAlpha,
			modalTime));

		//GlobalApp.Inst.isWin = false;
		if (GlobalApp.Inst.isWin) {
			Win ();
		} else {
			Lose ();
		}
	}

	void Win() {
		Debug.Log ("WIN");
		float time = winnerTime * 6f;

		seq.Append (m_userMainCharacter.DOLocalMoveX (-700f, winnerTime2))
			.Insert (time, m_winner [0].DOLocalMove (new Vector2 (264f, 371f), winnerTime2))
			.Insert (time, m_winner [1].DOLocalMove (new Vector2 (393f, 371f), winnerTime2))
			.Insert (time, m_winner [2].DOLocalMove (new Vector2 (477f, 371f), winnerTime2))
			.Insert (time, m_winner [3].DOLocalMove (new Vector2 (604f, 371f), winnerTime2))
			.Insert (time, m_winner [4].DOLocalMove (new Vector2 (721f, 371f), winnerTime2))
			.Insert (time, m_winner [5].DOLocalMove (new Vector2 (840f, 371f), winnerTime2))
			.Insert (time, m_winner [0].DOScale (new Vector3 (0.5f, 0.5f, 0.5f), winnerTime2))
			.Insert (time, m_winner [1].DOScale (new Vector3 (0.5f, 0.5f, 0.5f), winnerTime2))
			.Insert (time, m_winner [2].DOScale (new Vector3 (0.5f, 0.5f, 0.5f), winnerTime2))
			.Insert (time, m_winner [3].DOScale (new Vector3 (0.5f, 0.5f, 0.5f), winnerTime2))
			.Insert (time, m_winner [4].DOScale (new Vector3 (0.5f, 0.5f, 0.5f), winnerTime2))
			.Insert (time, m_winner [5].DOScale (new Vector3 (0.5f, 0.5f, 0.5f), winnerTime2));
		seq.AppendInterval (waitTime);
		seq.OnComplete (OnCompleteChoreography);

		m_btns [0].transform.localPosition = new Vector2 (619f, 86f);
	}

	void Lose() {
		Debug.Log ("LOSE");
		float time = winnerTime * 6f;

		seq.Append (m_otherUserMainCharacter.DOLocalMoveX (700f, winnerTime2))
			.Insert (time, m_winner [0].DOLocalMove (new Vector2 (-840f, 371f), winnerTime2))
			.Insert (time, m_winner [1].DOLocalMove (new Vector2 (-721f, 371f), winnerTime2))
			.Insert (time, m_winner [2].DOLocalMove (new Vector2 (-604f, 371f), winnerTime2))
			.Insert (time, m_winner [3].DOLocalMove (new Vector2 (-477f, 371f), winnerTime2))
			.Insert (time, m_winner [4].DOLocalMove (new Vector2 (-393f, 371f), winnerTime2))
			.Insert (time, m_winner [5].DOLocalMove (new Vector2 (-264f, 371f), winnerTime2))
			.Insert (time, m_winner [0].DOScale (new Vector3 (0.5f, 0.5f, 0.5f), winnerTime2))
			.Insert (time, m_winner [1].DOScale (new Vector3 (0.5f, 0.5f, 0.5f), winnerTime2))
			.Insert (time, m_winner [2].DOScale (new Vector3 (0.5f, 0.5f, 0.5f), winnerTime2))
			.Insert (time, m_winner [3].DOScale (new Vector3 (0.5f, 0.5f, 0.5f), winnerTime2))
			.Insert (time, m_winner [4].DOScale (new Vector3 (0.5f, 0.5f, 0.5f), winnerTime2))
			.Insert (time, m_winner [5].DOScale (new Vector3 (0.5f, 0.5f, 0.5f), winnerTime2));
		seq.AppendInterval (waitTime);
		seq.OnComplete (OnCompleteChoreography);

		m_btns [0].transform.localPosition = new Vector2 (81f, 86f);
	}

	void OnDisable() {
		Debug.Log ("Kill sequence");
		seq.Kill ();
	}

	private void OnCompleteChoreography() {
		Debug.Log ("Complete sequence");
		m_btns [0].SetActive (true);
		m_btns [1].SetActive (true);
		m_btns [2].SetActive (true);
	}

}
