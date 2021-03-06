﻿using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class FormationInfoPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {

		switch (btnName) {
		case "btn_back":
		case "spr_modal":
		case "btn_x":
			GuiMgr.GetInst ().PopPnl ();
			break;
		}
	}
		
	private UILabel m_formationEffect;
	private UISprite[] m_formationItems;

	void Awake() {
		m_formationEffect = transform.FindChild ("formation/lbl_formation_effect").GetComponent<UILabel> ();

		m_formationItems = new UISprite[FixedConstantValue.FORM_CELL_NUM];
		for (int i = 0; i < FixedConstantValue.FORM_CELL_NUM; i++) {
			StringBuilder sb = new StringBuilder ("formation/root/spr_formation_item_");
			sb.Append (i);
			m_formationItems[i] = transform.FindChild (sb.ToString()).GetComponent<UISprite> ();
		}
	}

	void OnEnable() {
		Party party = GlobalApp.Inst.GetCachedParties ()[0];
		FormInfo form = party.m_formList [0];
		CharInfo[] charSet = party.m_charSetList [0];

		m_formationEffect.text = form.m_EffectDisc;

		for (int i = 0; i < form.m_Form.Length; i++) {
			int charIndex = form.m_Form [i];
			if (charIndex == -1) {
				m_formationItems [i].spriteName = "UISliderBG";
			} else {
				if (charSet[charIndex] == null)
					m_formationItems [i].spriteName = "btn_9slice_pressed";
				else
					m_formationItems [i].spriteName = charSet[charIndex].spriteName;
			}
		}
	}

}
