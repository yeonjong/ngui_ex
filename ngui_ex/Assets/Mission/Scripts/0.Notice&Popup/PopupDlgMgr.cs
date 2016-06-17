using UnityEngine;
using System.Collections;

public enum PopupType {
    twoBtn,
    oneBtn
}

public class PopupInfo {

    private string popupMsg;
    private PopupType popupType;

    public PopupInfo(string popupMsg, PopupType popupType)
    {
        this.popupMsg = popupMsg;
        this.popupType = popupType;
    }

    public string GetPopupMsg()
    {
        return popupMsg;
    }

    public PopupType GetPopupType()
    {
        return popupType;
    }

}

public class PopupDlgMgr : MonoBehaviour {

    //imsi
    public UILabel imsiCounter;
    public int imsiCount = 0;

    private static PopupDlgMgr inst;


    private Stack popupStack;
    private PopupInfo popupInfo;
    private GameObject popupObject;

    [SerializeField]
    private GameObject targetRoot;
    [SerializeField]
    private GameObject popupPrefab;

    private void Awake() {
        if (!inst) { inst = this; }

        popupStack = new Stack();
    }
    
    private PopupDlgMgr() { }

    public static PopupDlgMgr GetInst() { return inst; }
    
    // TODO: 
    public void ShowPopupYesNo(string msg) {
        TryShow(new PopupInfo(msg, PopupType.twoBtn));
    }

    public void ShowPopupOk(string msg) {
        TryShow(new PopupInfo(msg, PopupType.oneBtn));
    }

    private void TryShow(PopupInfo popupInfo) {
        if (popupObject != null) {
            popupStack.Push(this.popupInfo);
            Destroy(popupObject);
        }
        this.popupInfo = popupInfo; imsiCounter.text = (++imsiCount).ToString();
        popupObject = targetRoot.AddChild(popupPrefab);
        popupObject.GetComponent<PopupDlg>().Init(popupInfo);
    }
    
    private void CheckPopupStack() {
        Destroy(popupObject); imsiCounter.text = (--imsiCount).ToString();

        if (popupStack.Count > 0) {
            popupObject = targetRoot.AddChild(popupPrefab);
            popupInfo = popupStack.Pop() as PopupInfo;
            popupObject.GetComponent<PopupDlg>().Init(popupInfo);
        }
    }

    public void ClickYesBtn() {
        //yes event.
        CheckPopupStack();
    }

    public void ClickNoBtn() {
        //yes event.
        CheckPopupStack();
    }

    public void ClickOKBtn() {
        //yes event.
        CheckPopupStack();
    }

}
