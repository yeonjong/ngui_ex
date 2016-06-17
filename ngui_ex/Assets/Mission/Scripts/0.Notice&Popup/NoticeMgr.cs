using UnityEngine;
using System.Collections;

public class NoticeMgr : MonoBehaviour{

    private static NoticeMgr inst;

    private Queue noticeMsgQueue;

    [SerializeField]
    private GameObject noticeGrid;
    [SerializeField]
    private GameObject noticeMsgPrefab;

    public float destroyedTime = 5.0f;
    public int noticeLimit = 10;

    void Awake() {
        if (!inst) { inst = this; }

        noticeMsgQueue = new Queue();
    }

    private NoticeMgr() { }

    public static NoticeMgr GetInst() {
        return inst;
    }

    // TODO: msg를 가운데 뿅하고 나오게 한다.
    // 만약 이전 msg가 표시중이라면 이전 msg를 위로 올린다.
    // msg 유지기간은 5초로 하자.
    public void ShowNotice(string msg) {
        Show(msg);
    }

    private void Show(string msg) {
        if (!noticeGrid.activeSelf) {
            noticeGrid.SetActive(true);
            noticeGrid.GetComponent<TweenScale>().enabled = true;
            noticeGrid.GetComponent<TweenAlpha>().enabled = true;
        }

        GameObject mail = noticeGrid.AddChild(noticeMsgPrefab);
        mail.GetComponentInChildren<UILabel>().text = msg;
        noticeGrid.GetComponent<UIGrid>().Reposition();

        noticeMsgQueue.Enqueue(mail);
        StartCoroutine(DequeNoticeMsgQueue());
    }

    private IEnumerator DequeNoticeMsgQueue() {
        yield return new WaitForSeconds(destroyedTime);
        Destroy(noticeMsgQueue.Dequeue() as GameObject);

        if (noticeMsgQueue.Count == 0) noticeGrid.SetActive(false);
    }

}
