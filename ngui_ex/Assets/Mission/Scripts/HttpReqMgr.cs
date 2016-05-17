using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Text;

public class HttpReqMgr : MonoBehaviour {

    private static HttpReqMgr inst;

    private const string ROOT_URI = "http://127.0.0.1:9192/";
    private StringBuilder uri;

    private HttpReqMgr() { }

    public static HttpReqMgr GetInst() {
        return inst;
    }
    
    void Awake() {
        if (!inst) { inst = this; }
    }
 
    public void GetJsonData(string method) {
        string responseJson = null;

        uri = new StringBuilder(ROOT_URI);
        uri.Append(method);

        try {
            WebClient webClient = new WebClient();
            Stream stream = webClient.OpenRead(uri.ToString());
            responseJson = new StreamReader(stream).ReadToEnd();
        } catch (Exception e) {
            Debug.Log(e.Message);
            return;
        } finally { }
    }

    public string PostJsonData(string method, string requestJson) {
        string responseJson = null;

        uri = new StringBuilder(ROOT_URI);
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

    public void Req(string method, string body, System.Action<string> act_on_complete) {
        string responseJson = PostJsonData(method, body);
        act_on_complete.Invoke(responseJson); //act_on_complete(responseJson); //이렇게 사용해도 된다.
    }

}
