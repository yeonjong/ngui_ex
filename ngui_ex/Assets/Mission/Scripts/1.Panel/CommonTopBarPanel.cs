using UnityEngine;
using System.Collections;

public class CommonTopBarPanel : MonoBehaviour {

	static public CommonTopBarPanel current;

	public void ClickBackwardBtn() {
		if (current == null) {
			current = this;

			if ((GameStateMgr.GetInst ().Curr).Equals (GAME_STATE.OtherState)) {
				GuiMgr.GetInst ().Backward ();
			} else {
				GameStateMgr.GetInst ().Backward ();
			}

			current = null;
		}
	}

	/*
	//StartCoroutine (Execute());
	IEnumerator Execute() {
		yield return new WaitUntil (() => GameStateMgr.GetInst ().BackwardState ());
	}
	*/
}