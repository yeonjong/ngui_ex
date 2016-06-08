using UnityEngine;
using System.Collections;

public class CommonTopBarPanel : MonoBehaviour {

	static public CommonTopBarPanel current;

	public void ClickBackwardBtn() {
		if (current == null) {
			current = this;
			StartCoroutine (Execute());
			current = null;
		}
	}

	IEnumerator Execute() {
		yield return new WaitUntil (() => GameStateMgr.GetInst ().BackwardState ());
	}

}