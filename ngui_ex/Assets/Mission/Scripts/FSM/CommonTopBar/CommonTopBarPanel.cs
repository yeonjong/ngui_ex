using UnityEngine;

public class CommonTopBarPanel : MonoBehaviour {

	bool isProcessComplete = true;

	public void ClickBackwardBtn() {
		if (isProcessComplete) {
			isProcessComplete = false;
			isProcessComplete = GameStateMgr.GetInst ().BackwardState ();
		}
	}

}