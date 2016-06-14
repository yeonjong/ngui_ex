using UnityEngine;
using System.Text;
using System.Collections;

public class RewardCell : MonoBehaviour {

	UILabel m_rewardScore;
	UISprite[] m_rewardItems;

	void Awake() {
		m_rewardScore = transform.FindChild ("lbl_reward_score").GetComponent<UILabel> ();

		m_rewardItems = new UISprite[FixedConstantValue.REWARD_ITEM_NUM];
		m_rewardItems[0] = transform.FindChild ("spr_reward_items/spr_reward_item_0").GetComponent<UISprite> ();
		m_rewardItems[1] = transform.FindChild ("spr_reward_items/spr_reward_item_1").GetComponent<UISprite> ();
		m_rewardItems[2] = transform.FindChild ("spr_reward_items/spr_reward_item_2").GetComponent<UISprite> ();
		m_rewardItems[3] = transform.FindChild ("spr_reward_items/spr_reward_item_3").GetComponent<UISprite> ();
	}

	public void Set(Reward reward) {
		if (m_rewardScore == null)
			Awake ();

		StringBuilder sb = new StringBuilder ();
		sb.AppendFormat ("Reward score: {0}", reward.m_nRewardScore);
		m_rewardScore.text = sb.ToString ();

		m_rewardItems[0].spriteName = Item.GetItemSpriteName (reward.m_nItemIDs[0]);
		m_rewardItems[1].spriteName = Item.GetItemSpriteName (reward.m_nItemIDs[1]);
		m_rewardItems[2].spriteName = Item.GetItemSpriteName (reward.m_nItemIDs[2]);
		m_rewardItems[3].spriteName = Item.GetItemSpriteName (reward.m_nItemIDs[3]);
	}

}
