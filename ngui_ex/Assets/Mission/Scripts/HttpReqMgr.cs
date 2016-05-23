using UnityEngine;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;

public class HttpReqMgr : MonoBehaviour {

    private static HttpReqMgr inst;

    private static string localRepository;

#if UNITY_EDITOR
    private const string ROOT_URI_DOWNLOADSERVER = "http://192.168.0.165:9192/";
    private const string ROOT_URI_WEBSERVER = "http://192.168.0.165:9292/";
#elif UNITY_ANDROID
    private const string ROOT_URI_DOWNLOADSERVER = "http://192.168.0.179:9192/";
    private const string ROOT_URI_WEBSERVER = "http://192.168.0.179:9292/";
#elif UNITY_IPHONE
    private const string ROOT_URI_DOWNLOADSERVER = "http://192.168.0.179:9192/";
    private const string ROOT_URI_WEBSERVER = "http://192.168.0.179:9292/";
#endif

    private StringBuilder uri;

    private HttpReqMgr() { }

    public static HttpReqMgr GetInst() {
        return inst;
    }
    
    void Awake() {
        if (!inst) { inst = this; }

        localRepository = new Uri(Application.persistentDataPath).LocalPath;
    }

    public string GetJsonData(string method) {
        string responseJson = null;

        uri = new StringBuilder(ROOT_URI_WEBSERVER);
        uri.Append(method);

        try {
            WebClient webClient = new WebClient();
            Stream stream = webClient.OpenRead(uri.ToString());
            responseJson = new StreamReader(stream).ReadToEnd();
        } catch (Exception e) {
            Debug.Log(e.Message);
            return null;
        } finally { }

        return responseJson;
    }

    public string PostJsonData(string method, string requestJson) {
        string responseJson = null;

        uri = new StringBuilder(ROOT_URI_WEBSERVER);
        uri.Append(method);

        try {
            WebClient webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
            webClient.Encoding = UTF8Encoding.UTF8;
            responseJson = webClient.UploadString(uri.ToString(), requestJson);
        } catch (Exception e) {
            Debug.Log(e.Message);
            return null;
        } finally { }

        return responseJson;
    }

    //for get
    public void Req2(string method, System.Action<string> act_on_complete)
    {
        string responseJson = GetJsonData(method);
        act_on_complete.Invoke(responseJson);
    }

    //for post
    public void Req(string method, string body, System.Action<string> act_on_complete) {
        string responseJson = PostJsonData(method, body);
        act_on_complete.Invoke(responseJson); //act_on_complete(responseJson); //이렇게 사용해도 된다.
    }

    public void AsyncDownloadAssetBundle(string method)//, UpdateProgress up, DoNextDownload nd)
    {
        uri = new StringBuilder(ROOT_URI_DOWNLOADSERVER);
        uri.Append(method);

#if UNITY_EDITOR
        string downloadRepositoryPath = String.Format("{0}\\{1}", localRepository, method);
#elif UNITY_ANDROID
        string downloadRepositoryPath = String.Format("{0}/{1}", localRepository, method);
#elif UNITY_IPHONE
        string downloadRepositoryPath = String.Format("{0}\\{1}", localRepository, method); //non checked
#endif

        /* Application.persistentDataPath로 경로를 지정하므로 필요없다.
        if (!System.IO.Directory.Exists(localRepository)) return;
            System.IO.Directory.CreateDirectory(localRepository);
        */

        try
        {
            WebClient webClient = new WebClient();
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler((sender, e) => ProgressChanged(sender, e, method));//, up));
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler((sender, e) => Completed(sender, e, method));
            webClient.DownloadFileAsync(new Uri(uri.ToString()), downloadRepositoryPath);
        }
        catch (Exception e)
        {
            Debug.Log("download asssetbundle was failed: " + method);
            Debug.Log(e.Message);
            return;
        }
        finally { }
    }

    private void Completed(object sender, AsyncCompletedEventArgs e, string downloadTarget) {
        Debug.Log("download " + downloadTarget);
        AssetBundleMgr.GetInst().OnDownloaded(downloadTarget);
    }
    private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e, string downlaodTarget) {//, UpdateProgress up) {
        //up(e.ProgressPercentage);
        AssetBundleMgr.GetInst().OnProgressChanged(e.ProgressPercentage, downlaodTarget);
    }

}
