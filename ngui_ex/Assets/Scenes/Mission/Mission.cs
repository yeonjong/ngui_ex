using UnityEngine;
using System.Text;

public class Mission : MonoBehaviour {

	private int m_n_cnt = 0;
    
    void OnGUI()
    {
		GUILayout.BeginVertical ();

		//
		if (GUILayout.Button ("Show Notice")) 
		{
			StringBuilder sb = new StringBuilder ("Hello World ");
			sb.Append (m_n_cnt.ToString ());
            m_n_cnt++;

            NoticeMgr.GetInst().ShowNotice(sb.ToString());
		}

		GUILayout.EndVertical ();
    }

}