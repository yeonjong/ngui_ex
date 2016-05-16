using UnityEngine;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;

public class HttpReqMgr : MonoBehaviour {

    private static HttpReqMgr inst;

    private HttpReqMgr() { }

    public static HttpReqMgr GetInst() {
        return inst;
    }

    void Awake() {
        if (!inst) { inst = this; }
    }

    public void GetJsonData() {
        string uri = "http://127.0.0.1:9092/login";
        WebClient webClient = new WebClient();
        Stream stream = webClient.OpenRead(uri);
        string responseJson = new StreamReader(stream).ReadToEnd();
    }

    public void PostJsonData() {
        string uri = "http://...";
        string requestJson = "{ 'id' : 'hello', 'pw' : 'hello'}";
        WebClient webClient = new WebClient();
        webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
        webClient.Encoding = UTF8Encoding.UTF8;
        string responseJson = webClient.UploadString(uri, requestJson);
        // 또는 http://j07051.tistory.com/556
        //https://www.packtpub.com/books/content/using-rest-api-unity-part-1-what-rest-and-basic-queries

    }

    // HttpReqMgr.inst.Req("login", "{ "id" : "hello", "pw" : "hello"}", System.Action act_on_complete)
    // 로그인되면 {"result" : "yes"}
    // 로그인실패 {"result" : "no"}
    // 
    public void Req(string method, string body, System.Action<string> act_on_complete) {
    }

}
