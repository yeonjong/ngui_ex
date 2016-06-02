using UnityEngine;

/*
 * 빈 배경이 없도록 하기 위한 스크립트.
 * 16:9를 기준으로
 * 가로가 더 긴 화면은, UI Sprite/aspect를 based on with로 지정하고,
 * 세로가 더 긴 화면은, UI Sprite/aspect를 based on heigh로 지정한다.
*/

#if UNITY_EDITOR
[ExecuteInEditMode]
public class BackgroundImage : MonoBehaviour {
	private float ratio;
	void Awake() {//Update () {
		ratio = (float)Screen.width / (float)Screen.height;
		//Debug.Log (ratio);
		if (ratio > 1.777778) {
			gameObject.GetComponent<UISprite>().keepAspectRatio = UIWidget.AspectRatioSource.BasedOnWidth;
		} else {
			gameObject.GetComponent<UISprite>().keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;
		}
		/*
		if (Camera.main.aspect > 1.777778) {
			gameObject.GetComponent<UISprite>().keepAspectRatio = UIWidget.AspectRatioSource.BasedOnWidth;
		} else {
			gameObject.GetComponent<UISprite>().keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;
		}
		*/
	}
}
#else
public class BackgroundImage : MonoBehaviour {
	void Awake() {
		ratio = (float)Screen.width / (float)Screen.height;
		if (ratio > 1.777778) {
			gameObject.GetComponent<UISprite>().keepAspectRatio = UIWidget.AspectRatioSource.BasedOnWidth;
		} else {
			gameObject.GetComponent<UISprite>().keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;
		}
	}
}
#endif