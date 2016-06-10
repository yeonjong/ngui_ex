using UnityEngine;
using System.Collections;

public class ItemInfoPanel : MonoBehaviour {

	public void OnClickXXXBtn(string btnName) {
		Debug.Log (btnName);

		switch (btnName) {
		case "spr_modal":
			//GuiMgr.GetInst ().PopPanel ();
			break;
		}
	}

}
