using UnityEngine;
using System.Collections.Generic;

public class GameObjectPoolMgr : MonoBehaviour {

    private static GameObjectPoolMgr inst;

    private Dictionary<string, GameObject> prefabDictionary;
    private Dictionary<string, GameObject> gameObjectDictionary;

    public GameObject m_gui_pool;
    public Transform m_gui_root;

	/* imsi. please make some assetbundles. */
	public GameObject common_top_bar_prefab;
	public GameObject lobby_prefab;
	public GameObject chapter_map_prefab;
	public GameObject stage_entrance_prefab;
	public GameObject party_edit_prefab;
	public GameObject formation_edit_prefab;
	public GameObject battle_prefab;


    private GameObjectPoolMgr() { }

    public static GameObjectPoolMgr GetInst() {
        return inst;
    }

    void Awake() {
        if (!inst) { inst = this; }

        prefabDictionary = new Dictionary<string, GameObject>();
        gameObjectDictionary = new Dictionary<string, GameObject>();
    }

    // 0. 현재 해당 인스턴스가 있다면
    //   0. 리턴하고 끝
    // 1. 현재 해당 인스턴스가 없다면
    //   0. 프리팹을 로드한다. 그리고 리스트에 삽입한다.
    //   1. 인스턴스를 제작하고 리턴한다. 끝...
    public bool CheckObject(string prf_name)
    {
        switch (prf_name)
        {
            case "pnl_lobby":
            case "pnl_battle":
                if (gameObjectDictionary.ContainsKey(prf_name)) {
                    return true;
                } else if (prefabDictionary.ContainsKey(prf_name)) {
                    return true;
                }
                return false;

            default:
                return true;
        }
    }

    public void Reg(string name, GameObject pref) {
        if (pref == null) return;
        prefabDictionary.Add(name, pref);
    }

    public GameObject Load(string prf_name, Vector3 v3, Quaternion q) {
        GameObject temp;

        if (gameObjectDictionary.ContainsKey(prf_name)) {
            temp = gameObjectDictionary[prf_name];
        } else {
            if (!prefabDictionary.ContainsKey(prf_name)) {

                switch (prf_name) {
                case "pnl_intro":
                case "pnl_patch":
					temp = Resources.Load(prf_name) as GameObject;
                    break;
				
				/*
				case "pnl_common_top_bar":
					temp = common_top_bar_prefab;
					break;
				case "pnl_lobby":
					temp = lobby_prefab;
					break;
				case "pnl_chapter_map":
					temp = chapter_map_prefab;
					break;
				case "pnl_stage_entrance":
					temp = stage_entrance_prefab;
					break;
				case "pnl_party_edit":
					temp = party_edit_prefab;
					break;
				case "pnl_formation_edit":
					temp = formation_edit_prefab;
					break;
				case "pnl_battle":
					temp = battle_prefab;
					break;
				*/
                default:
					temp = AssetBundleMgr.GetInst().LoadAsset(prf_name);
                    break;
                }

                
                if (temp == null) {
                    return null; //prf_name에 해당하는 프리팹이 Resources 폴더에 없다.
                }
                prefabDictionary.Add(prf_name, temp);
            }
            temp = m_gui_pool.AddChild(prefabDictionary[prf_name]);
            gameObjectDictionary.Add(prf_name, temp);
        }
        
        if (prf_name.StartsWith("pnl_")) {
            temp.transform.SetParent(m_gui_root);
            temp.transform.position = v3;
            temp.transform.rotation = q;
        } else {
            temp.transform.position = v3;
            temp.transform.rotation = q;
        }

        return temp;
    }

    public bool ReleaseObjectReference(ref GameObject temp) {
        if (temp == null) return false;

        if (temp.name.StartsWith("pnl_")) {
            temp.transform.SetParent(m_gui_pool.transform);
            temp.transform.position = Vector3.zero;
            temp.transform.rotation = Quaternion.identity;
        } else {
            temp.transform.position = Vector3.zero;
            temp.transform.rotation = Quaternion.identity;
        }

        temp = null;
        return true;
    }

    public bool ReleaseGameObject(string prf_name) {
        if (gameObjectDictionary.ContainsKey(prf_name)) {
            gameObjectDictionary.Remove(prf_name);
            return true;
        }

        if (!prefabDictionary.ContainsKey(prf_name)) {
            Debug.Log("there are no prefab, " + prf_name);
            return false;
        }

        Debug.Log("there are no gameobject, " + prf_name);
        return false;
    }

}