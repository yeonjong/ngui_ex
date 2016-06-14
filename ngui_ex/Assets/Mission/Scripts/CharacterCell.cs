using UnityEngine;
using System.Collections;

public class CharacterCell : MonoBehaviour {

	private UILabel[] m_labels;
	private UISprite[] m_sprites;

	void Awake() {
		m_labels = new UILabel[3];
		m_labels [0] = transform.FindChild ("lbl_character_cost").GetComponent<UILabel> ();
		m_labels [1] = transform.FindChild ("lbl_character_class_kind").GetComponent<UILabel> ();
		m_labels [2] = transform.FindChild ("lbl_character_level").GetComponent<UILabel> ();

		m_sprites = new UISprite[3];
		m_sprites [0] = transform.FindChild ("spr_character_thumbnail").GetComponent<UISprite> ();
		m_sprites [1] = transform.FindChild ("spr_character_upgrade_rank").GetComponent<UISprite> ();
		m_sprites [2] = transform.FindChild ("spr_character_star_rank").GetComponent<UISprite> ();
	}

	void SetCharCell(CharInfo charInfo) {
		m_labels [2].text = charInfo.cost.ToString();
		m_labels [3].text = charInfo.classKind;
		m_labels [4].text = charInfo.level.ToString();

		m_sprites [0].spriteName = charInfo.spriteName;
		Color[] colors = new Color[2] { Color.yellow, Color.black };
		m_sprites [1].color = charInfo.upgradeRank == 0 ? colors[0] : colors[1];
		m_sprites [2].color = charInfo.starRank == 0 ? colors[0] : colors[1];
	}

}
