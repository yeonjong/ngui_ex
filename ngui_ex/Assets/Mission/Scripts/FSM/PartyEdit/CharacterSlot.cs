using UnityEngine;

public class CharacterSlot : MonoBehaviour {

	public static CharacterSlot current; // public?

	public delegate void OnClickCharacterSlot (int characterID); // public?
	public OnClickCharacterSlot onClickCharacterSlot;

	private UISprite thumbnailSprite;
	private UISprite starRankSprite; //성급.
	private UISprite upgradeRankSprite; //등급이 영어로 뭐지?
	private UILabel costLabel;
	private UILabel classNameLabel; //병종.
	private UILabel levelLabel;

	private CharacterPrivateSpec privateSpec;
	public CharacterPrivateSpec PrivateSpec {
		set {
			privateSpec = value;

			if (privateSpec != null) {
				CharacterCommonSpec commonSpec = privateSpec.CommonSpec;
				thumbnailSprite.spriteName = commonSpec.GetThumbnailImageName ();
				starRankSprite.spriteName = privateSpec.GetStarRankImageName ();
				upgradeRankSprite.spriteName = privateSpec.GetUpgradeRankImageName ();
				costLabel.text = commonSpec.cost.ToString ();
				classNameLabel.text = commonSpec.className.ToString ();
				levelLabel.text = privateSpec.level.ToString ();
			} else {
				thumbnailSprite.spriteName = null;
				starRankSprite.spriteName = null;
				upgradeRankSprite.spriteName = null;
				costLabel.text = null;
				classNameLabel.text = null;
				levelLabel.text = null;
			}
		}
	}

	void Awake() {
		thumbnailSprite = GetComponent<UISprite> ();
		starRankSprite = GetComponent<UISprite> ();
		upgradeRankSprite = GetComponent<UISprite> ();
		costLabel = GetComponent<UILabel> ();
		classNameLabel = GetComponent<UILabel> ();
		levelLabel = GetComponent<UILabel> ();
	}

	void OnClick() {
		if (current == null && UICamera.currentTouchID != -2 && UICamera.currentTouchID != -3) {
			current = this;
			if (onClickCharacterSlot != null) {
				#if UNITY_EDITOR
				Debug.Log ("onclick character slot");
				if (privateSpec == null) Debug.LogError ("Please set CharacterSlot/privateSpec");
				#endif
				onClickCharacterSlot(privateSpec.privateCharacterID);
			}
			current = null;
		}
	}

}
