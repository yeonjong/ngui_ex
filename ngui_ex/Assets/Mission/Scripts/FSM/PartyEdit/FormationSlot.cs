using UnityEngine;
using System.Collections;

public class FormationSlot : MonoBehaviour {

	public static FormationSlot current; // public?

	public delegate void OnClickFormationSlot (int formationSlotIndex);
	public OnClickFormationSlot onClickFormationSlot;

	private int formationSlotIndex = -1;
	public int FormationSlotIndex {
		set {
			formationSlotIndex = value;
		
			//if (formationSlotIndex != null)
			// initialize?
		}
	}

	void OnClick() {
		if (current == null && UICamera.currentTouchID != -2 && UICamera.currentTouchID != -3) {
			current = this;
			if (onClickFormationSlot != null) {
				#if UNITY_EDITOR
				Debug.Log ("onclick formation slot");
				if (formationSlotIndex == -1) Debug.LogError ("Please set FormationSlot/formationSlotIndex");
				#endif
				onClickFormationSlot(formationSlotIndex);
			}
			current = null;
		}
	}

}
