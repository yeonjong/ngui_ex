using UnityEngine;
using System;

public class JsonParser {

    [Serializable]
    public class UserInfo
    {
        public string id;
        public string pw;

        public UserInfo(string id, string pw)
        {
            this.id = id;
            this.pw = pw;
        }
    }

    [Serializable]
    public class LoginResponse
    {
        public string result;
    }

    public static string MakeLoginJson(string id, string pw)
    {
        UserInfo userInfo = new UserInfo(id, pw);
        string json = JsonUtility.ToJson(userInfo);
        return json;
    }

    public static string GetLoginJsonsResult(string json)
    {
        LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(json);
        return loginResponse.result;
    }

}
