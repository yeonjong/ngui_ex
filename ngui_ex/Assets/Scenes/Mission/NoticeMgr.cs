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

        /*
        if (noticeMsgQueue.Count > noticeLimit) {
            //끝부터 바로 삭제. //앞선 코루틴을 취소해야되...
            //Destroy(noticeMsgQueue.Dequeue() as GameObject);
        }
        */
        Show(msg);
    }

    private void Show(string msg) {
        GameObject mail = noticeGrid.AddChild(noticeMsgPrefab);
        mail.GetComponentInChildren<UILabel>().text = msg;
        noticeGrid.GetComponent<UIGrid>().Reposition();

        noticeMsgQueue.Enqueue(mail);
        StartCoroutine(DequeNoticeMsgQueue());
    }

    private IEnumerator DequeNoticeMsgQueue() {
        yield return new WaitForSeconds(destroyedTime);
        Destroy(noticeMsgQueue.Dequeue() as GameObject);
    }

}
