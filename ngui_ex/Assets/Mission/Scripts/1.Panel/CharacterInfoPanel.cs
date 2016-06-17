using UnityEngine;
using System.Collections;

public class CharacterInfoPanel : PanelBase {

	public override void OnClickXXXBtn(string btnName) {
		switch (btnName) {
		case "btn_back":
		case "spr_modal":
		case "btn_x":
			GuiMgr.GetInst ().PopPnl ();
			break;
		}
	}

	private UILabel[] m_labels;
	private UISprite[] m_sprites;

	void Awake() {
		m_labels = new UILabel[5];
		m_labels [0] = transform.FindChild ("character/lbl_character_name").GetComponent<UILabel> ();
		m_labels [1] = transform.FindChild ("character/lbl_character_feature").GetComponent<UILabel> ();
		m_labels [2] = transform.FindChild ("character/lbl_character_cost").GetComponent<UILabel> ();
		m_labels [3] = transform.FindChild ("character/lbl_character_class_kind").GetComponent<UILabel> ();
		m_labels [4] = transform.FindChild ("character/lbl_character_level").GetComponent<UILabel> ();

		m_sprites = new UISprite[3];
		m_sprites [0] = transform.FindChild ("character/spr_character_thumbnail").GetComponent<UISprite> ();
		m_sprites [1] = transform.FindChild ("character/spr_character_upgrade_rank").GetComponent<UISprite> ();
		m_sprites [2] = transform.FindChild ("character/spr_character_star_rank").GetComponent<UISprite> ();
	}

	void OnEnable() {
		Party party = GlobalApp.Inst.GetCachedParties()[0];
		int charIndex = GlobalApp.Inst.GetBtnIndex ();

		CharInfo character = party.m_charSetList [0] [charIndex];

		m_labels [0].text = character.name;
		m_labels [1].text = character.feature;
		m_labels [2].text = character.cost.ToString();
		m_labels [3].text = character.classKind;
		m_labels [4].text = character.level.ToString();

		m_sprites [0].spriteName = character.spriteName;
		Color[] colors = new Color[2] { Color.yellow, Color.black };
		m_sprites [1].color = character.upgradeRank == 0 ? colors[0] : colors[1];
		m_sprites [2].color = character.starRank == 0 ? colors[0] : colors[1];
	}

}
