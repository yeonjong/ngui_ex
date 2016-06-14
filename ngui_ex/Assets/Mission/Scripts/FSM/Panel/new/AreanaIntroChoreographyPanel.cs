using UnityEngine;
using System.Collections;
using DG.Tweening;

public class AreanaIntroChoreographyPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "btn_back":
		case "btn_imsi_back":
			if (GuiMgr.GetInst ().CheckContainsTargetPanel (PANEL_TYPE.AreanaEntrance))
				GuiMgr.GetInst ().PopPnl (PANEL_TYPE.AreanaEntrance);
			else if (GuiMgr.GetInst ().CheckContainsTargetPanel (PANEL_TYPE.ShamBattleEntrance))
				GuiMgr.GetInst ().PopPnl (PANEL_TYPE.ShamBattleEntrance);
			else
				Debug.LogError ("this case not implemented");
			break;
		case "btn_imsi_after2seconds":
			GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaBattle);
			break;
		}
	}

	UISprite m_map;
	Transform m_diagonalLine;
	Transform m_userMainCharacter;
	Transform m_otherUserMainCharacter;
	UILabel m_userInfo;
	UILabel m_otherUserInfo;
	Transform m_v;
	Transform m_s;
	Transform m_splash;

	void Awake() {
		m_map = transform.FindChild ("spr_basic_bg").GetComponent<UISprite> ();

		m_diagonalLine = transform.FindChild ("spr_diagonal_line");
		m_userMainCharacter = transform.FindChild ("spr_user_main_character");
		m_otherUserMainCharacter = transform.FindChild ("spr_other_user_main_character");
		m_userInfo = transform.FindChild ("lbl_user_nickname").GetComponent<UILabel> ();
		m_otherUserInfo = transform.FindChild ("lbl_other_user_nickname").GetComponent<UILabel> ();
		m_v = transform.FindChild ("lbl_v");
		m_s = transform.FindChild ("lbl_s");
		m_splash = transform.FindChild ("spr_splash");
	}

	void OnEnable() {
		Debug.Log ("Enable");

		m_map.spriteName = "Start Screen";
		m_diagonalLine.localPosition = new Vector2 (574f, 1306f);
		m_userMainCharacter.localPosition = new Vector2 (-1700f, 0f);
		m_otherUserMainCharacter.localPosition = new Vector2 (1700f, 0f);
		m_userInfo.color = new Color (1f, 1f, 1f, 0f);
		m_otherUserInfo.color = new Color (1f, 1f, 1f, 0f);
		m_v.localPosition = new Vector2 (-117f, 710f);
		m_s.localPosition = new Vector2 (152f, -710f);
		m_splash.localScale = new Vector2 (0f, 0f);

		//StartCoroutine (StartChoreography());
		StartChoreography();
	}

	public float diagonalLineTime = 0.5f;
	public Ease diagonalLineEase = Ease.OutBack;
	public float mainCharTime = 0.45f;
	public Ease mainCharEase = Ease.InOutCubic;
	public float infoTime = 0.2f;
	public Ease infoEase = Ease.InQuad;
	public float vsTime = 0.25f;
	public Ease vsEase = Ease.OutBack;
	public float splashTime = 0.55f;
	public float splashTime2 = 0.35f;
	public Ease splashEase = Ease.OutQuad;
	public Ease splashEase2 = Ease.InQuad;
	public float waitTime = 0.25f;

	Sequence seq;
	/*IEnumerator*/ void StartChoreography() {
		//yield return new WaitForFixedUpdate();

		seq = DOTween.Sequence ();
		seq.Append (m_diagonalLine.DOLocalMove (Vector3.zero, diagonalLineTime).SetEase (diagonalLineEase));
		seq.Append (m_userMainCharacter.DOLocalMoveX (-700f, mainCharTime).SetEase(mainCharEase));
		seq.Append (DOTween.ToAlpha (() => m_userInfo.color,
			x => m_userInfo.color = x,
			1f,
			infoTime).SetEase (infoEase));
		seq.Append (m_otherUserMainCharacter.DOLocalMoveX (700f, mainCharTime).SetEase(mainCharEase));
		seq.Append (DOTween.ToAlpha (() => m_otherUserInfo.color,
			x => m_otherUserInfo.color = x,
			1f,
			infoTime).SetEase (infoEase));
		seq.Append (m_v.DOLocalMoveY (0f, vsTime).SetEase (vsEase));
		seq.Append (m_s.DOLocalMoveY (0f, vsTime).SetEase (vsEase));
		seq.Append (m_splash.DOScale(new Vector3(4f, 4f, 1f), splashTime).SetEase(splashEase));
		seq.Append (m_splash.DOScale (Vector3.zero, splashTime2).SetEase(splashEase2));
		seq.AppendInterval (waitTime);
		seq.OnComplete (OnCompleteChoreography);

		//yield return new WaitForSeconds (0.1f);
		//GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaBattle);
	}

	void OnDisable() {
		Debug.Log ("Kill sequence");
		seq.Kill ();
	}

	private void OnCompleteChoreography() {
		Debug.Log ("Complete sequence");
		GuiMgr.GetInst ().PushPnl (PANEL_TYPE.AreanaBattle);
	}

}