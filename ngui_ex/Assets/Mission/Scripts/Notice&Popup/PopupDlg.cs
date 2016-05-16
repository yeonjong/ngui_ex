using UnityEngine;
using System.Collections;

public class PopupDlg : MonoBehaviour {

    private GameObject[] btns;
    [SerializeField]
    private UILabel popupMsg;
    private TweenScale tweenScale;
    private TweenAlpha tweenAlpha;

    private void Awake() {
        btns = new GameObject[3];
        btns[0] = transform.FindChild("YesBtn").gameObject;
        btns[1] = transform.FindChild("NoBtn").gameObject;
        btns[2] = transform.FindChild("OKBtn").gameObject;

        popupMsg = transform.FindChild("PopupMsg/Label").GetComponent<UILabel>();

        tweenScale = GetComponent<TweenScale>();
        tweenAlpha = GetComponent<TweenAlpha>();
    }

    public void Init(PopupInfo popupInfo) {
        popupMsg.text = popupInfo.GetPopupMsg();
        SetPopupType(popupInfo.GetPopupType());

        tweenScale.enabled = true;
        tweenAlpha.enabled = true;
    }

    private void SetPopupType(PopupType popupType) {
        if (popupType == PopupType.twoBtn) {
            btns[0].SetActive(true);
            btns[1].SetActive(true);
        } else {
            btns[2].SetActive(true);
        }
    }

    public void ClickYesBtn()
    {
        PopupDlgMgr.GetInst().ClickYesBtn();
    }

    public void ClickNoBtn()
    {
        PopupDlgMgr.GetInst().ClickNoBtn();
    }

    public void ClickOKBtn()
    {
        PopupDlgMgr.GetInst().ClickOKBtn();
    }
}