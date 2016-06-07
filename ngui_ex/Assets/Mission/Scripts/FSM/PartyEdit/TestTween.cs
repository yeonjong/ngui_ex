/*
using UnityEngine;
using System.Collections;

public class TestTween : MonoBehaviour {

	//TweenPosition pos;
	//TweenScale scale;
	//TweenAlpha alpha;
	UIPanel panel;

	void Awake() {
		panel = GetComponent<UIPanel> ();
		//pos = GetComponent<TweenPosition> ();
		//pos.from = from;
		//pos.to = from;
		//pos.duration = 5f;

		//scale = GetComponent<TweenScale> ();
		//alpha = GetComponent<TweenAlpha> ();
	}

	void Start() {
		//DoAnimation (Vector2.zero, Vector2.down);
	}

	public void PlayAnimation(Vector2 from, Vector2 to, GameObject eventReceiver) {
		//Debug.Log (this.transform.position);
		//Debug.Log ("FROM" + from + " TO" + to);
		//transform.position = from;
		TweenScale tweenScale = TweenScale.Begin (gameObject, 0.3f, new Vector3(112f/240f, 112f/240f, 0f));
		//tweenScale.value = Vector3.one;
		TweenAlpha tweenAlpha = TweenAlpha.Begin (gameObject, 0.3f, 0f);
		//tweenAlpha.value = 1f;
		tweenAlpha.steeperCurves = true;
		tweenAlpha.method = UITweener.Method.EaseIn;
		tweenAlpha.eventReceiver = gameObject;
		tweenAlpha.callWhenFinished = "Reset";

		TweenPosition tweenPostion = TweenPosition.Begin (gameObject, 0.3f, to);
		tweenPostion.from = from;
		tweenPostion.eventReceiver = eventReceiver;
		tweenPostion.callWhenFinished = "CheckFormation";

	}

	private void Reset() {
		transform.localScale = Vector3.one;
		transform.position = new Vector3 (-10f, 0f, 0f);
		panel.alpha = 1f;
	}

}
*/