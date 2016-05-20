using UnityEngine;
using System;

//TODO: MiniJSON 안썼나벼 0ㅇ0;;

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

[Serializable]
public class PatchDateTimeResponse
{
    public string recent_patch_date;
}

[Serializable]
public class PatchListResponse
{
    public string[] patch_list;
}

public class JsonParser {

    public static string MakeJson<T>(T jsonClassObject) {
        return JsonUtility.ToJson(jsonClassObject);
    }

    public static T GetResponseJsonClassObject<T>(string responseJson) {
        return JsonUtility.FromJson<T>(responseJson);
    }

}
