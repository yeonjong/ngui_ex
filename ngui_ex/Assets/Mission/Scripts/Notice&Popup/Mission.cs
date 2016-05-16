using UnityEngine;
using System.Text;

public class Mission : MonoBehaviour {

	private int m_n_cnt = 0;
    private int m_n_cnt_notice = 0;
    
    void OnGUI()
    {
		GUILayout.BeginVertical ();

		//
		if (GUILayout.Button ("Show YesNo Popup")) 
		{
            StringBuilder sb = new StringBuilder("Hello Yes No Popup ");
            sb.Append(m_n_cnt++);
            PopupDlgMgr.GetInst().ShowPopupYesNo(sb.ToString());
		}
        else if (GUILayout.Button("Show Ok Popup"))
        {
            StringBuilder sb = new StringBuilder("Hello Ok Popup ");
            sb.Append(m_n_cnt++);
            PopupDlgMgr.GetInst().ShowPopupOk(sb.ToString());
        }
        else if (GUILayout.Button("Show Notice"))
        {
            StringBuilder sb = new StringBuilder("Hello Notice ");
            sb.Append(m_n_cnt_notice++);
            NoticeMgr.GetInst().ShowNotice(sb.ToString());
        }

        GUILayout.EndVertical ();
    }

    //imsi
    public void ShowYesNoPopup() {
        StringBuilder sb = new StringBuilder("Hello Yes No Popup ");
        sb.Append(m_n_cnt++);
        PopupDlgMgr.GetInst().ShowPopupYesNo(sb.ToString());
    }

    public void ShowOKPopup()
    {
        StringBuilder sb = new StringBuilder("Hello Ok Popup ");
        sb.Append(m_n_cnt++);
        PopupDlgMgr.GetInst().ShowPopupOk(sb.ToString());
    }

    public void ShowNotice()
    {
        StringBuilder sb = new StringBuilder("Hello Notice ");
        sb.Append(m_n_cnt_notice++);
        NoticeMgr.GetInst().ShowNotice(sb.ToString());
    }
}