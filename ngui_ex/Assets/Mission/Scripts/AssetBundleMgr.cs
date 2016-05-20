using UnityEngine;
using System;
using System.Text;

public class AssetBundleMgr : MonoBehaviour {

    private static AssetBundleMgr inst;
    private static DateTime devicesPatchDateTime = Convert.ToDateTime("5/17/2016 4:43:00 PM"); //TODO: device에 저장되어 있어야함.
    private static DateTime recentPatchDateTime;

    private int downloadedCount;
    private string[] patchList;

    public GameObject loadingHelper;
    private LoadingHelper helper;

    void Awake() {
        if (!inst) inst = this;
    }

    private AssetBundleMgr() { }

    public static AssetBundleMgr GetInst() {
        return inst;
    }

    public void CheckIsPatched() {
        HttpReqMgr.GetInst().Req2("info/patchdate", CheckPatchDateTime_imsi);
    }

    private void CheckPatchDateTime_imsi(string responseJson)
    {
        PatchDateTimeResponse response = JsonParser.GetResponseJsonClassObject<PatchDateTimeResponse>(responseJson);
        recentPatchDateTime = Convert.ToDateTime(response.recent_patch_date);

        if (devicesPatchDateTime.CompareTo(recentPatchDateTime) < 0)
        {
            GuiMgr.GetInst().ShowPatchUI();
            TryPatch();
            return;
        }
        else
        {
            Debug.Log("Already patched");
            GameStateMgr.GetInst().ForwardState(GAME_STATE.LobbyState);
        }
    }

    public void TryPatch() {
        HttpReqMgr.GetInst().Req2("info/patchdate", CheckPatchDateTime);
    }

    private void CheckPatchDateTime(string responseJson) {
        PatchDateTimeResponse response = JsonParser.GetResponseJsonClassObject<PatchDateTimeResponse>(responseJson);
        recentPatchDateTime = Convert.ToDateTime(response.recent_patch_date);

        if (devicesPatchDateTime.CompareTo(recentPatchDateTime) < 0) {
            HttpReqMgr.GetInst().Req2("info/patchlist", DownloadAssetBundles);
            return;
        }
        else {
            Debug.Log("Already patched");
            GameStateMgr.GetInst().ForwardState(GAME_STATE.LobbyState);
        }
    }

    private void DownloadAssetBundles(string responseJson) {
        PatchListResponse patchListResponse = JsonParser.GetResponseJsonClassObject<PatchListResponse>(responseJson);

        patchList = patchListResponse.patch_list;
        downloadedCount = 0;

        helper = Instantiate(loadingHelper).GetComponent<LoadingHelper>();
        helper.SetDownloadList(patchListResponse.patch_list);

        if (patchList.Length != 0)
            HttpReqMgr.GetInst().AsyncDownloadAssetBundle(patchList[downloadedCount]);

    }

    public void OnProgressChanged(int percent, string downloadTarget) {
        helper.Repo(percent, downloadTarget);
    }

    public void OnDownloaded(string downloadTarget) {
        helper.ReportDownloaded(downloadTarget);
    }

    public void Down() {
        downloadedCount++;

        try {
            HttpReqMgr.GetInst().AsyncDownloadAssetBundle(patchList[downloadedCount]);
        } catch (Exception e) {
            Debug.Log("에러" + downloadedCount + "/" + patchList.Length);
            Debug.Log(e.Message);
        }
    }

    public void Complete() {
        patchList = null;
        devicesPatchDateTime = recentPatchDateTime;
        GuiMgr.GetInst().OnPatchCompleted();
        Debug.Log("Download Complete");
    }
    
    //note: http://docs.unity3d.com/ScriptReference/AssetBundle.LoadFromFile.html
    public GameObject LoadAsset(string name) {
        StringBuilder sb = new StringBuilder(Application.persistentDataPath);
        sb.Append('/');
        sb.Append(name);
        sb.Append(".unity3d");
        AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromFile(sb.ToString());

        if (myLoadedAssetBundle == null) {
            Debug.Log("Failed to load AssetBundle!");
            return null;
        }
        GameObject prefab = myLoadedAssetBundle.LoadAsset<GameObject>(name + ".prefab");
        
        myLoadedAssetBundle.Unload(false);
        return prefab;
    }

}
