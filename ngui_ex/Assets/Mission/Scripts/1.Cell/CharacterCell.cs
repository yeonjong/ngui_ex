using UnityEngine;
using System.Collections.Generic;

public class CharacterCell : MonoBehaviour {

	/* button */
	private PanelBase m_panelBase;
	public void SetBtnsTarget(PanelBase panelBase) {
		m_panelBase = panelBase;
	}

	private CELL_TYPE m_cellType;
	private int m_cellIndex;

	static public CharacterCell current;
	void OnClick() {
		if (m_panelBase == null) {
			Debug.Log ("panelBase is null");
			return;
		}

		if (current == null && UICamera.currentTouchID != -2 && UICamera.currentTouchID != -3) {
			current = this;
			m_panelBase.OnClickXXXCell (m_cellType, m_cellIndex);
			current = null;
		}
	}

	/* cell info */
	private UILabel[] m_labels;
	private UISprite[] m_sprites;

	void Awake() {
		m_cellType = CELL_TYPE.character;

		m_labels = new UILabel[3];
		m_labels [0] = transform.FindChild ("lbl_character_cost").GetComponent<UILabel> ();
		m_labels [1] = transform.FindChild ("lbl_character_class_kind").GetComponent<UILabel> ();
		m_labels [2] = transform.FindChild ("lbl_character_level").GetComponent<UILabel> ();

		m_sprites = new UISprite[3];
		m_sprites [0] = transform.FindChild ("spr_character_thumbnail").GetComponent<UISprite> ();
		m_sprites [1] = transform.FindChild ("spr_character_upgrade_rank").GetComponent<UISprite> ();
		m_sprites [2] = transform.FindChild ("spr_character_star_rank").GetComponent<UISprite> ();
	}

	public void Set(CharInfo charInfo, int cellIndex, bool belongFormation = false) {
		m_cellIndex = cellIndex;

		if (m_labels == null)
			Awake ();

		if (belongFormation) {
			m_sprites [0].color = new Color (0.3f, 0.3f, 0.3f);
		} else {
			m_sprites [0].color = Color.white;
		}

		if (charInfo == null) {
			m_labels [0].text = "";
			m_labels [1].text = "";
			m_labels [2].text = "";

			m_sprites [0].spriteName = "dark";
			m_sprites [1].color = Color.clear;
			m_sprites [2].color = Color.clear;
		} else {
			m_labels [0].text = charInfo.cost.ToString();
			m_labels [1].text = charInfo.classKind;
			m_labels [2].text = charInfo.level.ToString();
			
			m_sprites [0].spriteName = charInfo.spriteName;
			Color[] colors = new Color[2] { Color.yellow, Color.black };
			m_sprites [1].color = charInfo.upgradeRank == 0 ? colors[0] : colors[1];
			m_sprites [2].color = charInfo.starRank == 0 ? colors[0] : colors[1];
		}

	}

}
