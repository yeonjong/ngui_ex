using System;
using System.Text.RegularExpressions;

using UnityEngine; //삭제.

public class LoginChecker {

    private static string userID = null;
    private static string candidateID = null;

    private string idPattern = @"^[a-zA-Z0-9]{4,20}$"; //정규식1: 영문 or 숫자인지 확인한다. 길이는 4~20자리로 제한한다. 덤으로 공백 체크도 된다. GOOD.
    private string pwPattern = @"^[a-zA-Z0-9가-힣!@#$%^&*()-_+=]{6,20}$"; //정규식2: 알파벳 or 숫자 or 한글 or ?특수문자 인지 확인한다. 길이는 6~20자리로 제한한다.

    // 정규식.
    //!정규식 자세하게 블로그에 정리. (생각보다 복잡하다.)
    //?시작문자가 알파벳인지 확인한다. (아이디)
    //?특수문자가 포함됬는지 확인한다. (비밀번호)
    //?알파벳, 숫자, 한글, 특수문자 중 2가지 이상의 조합인지 확인한다. (비밀번호)
    // a-z, 0-9, 특수문자 // 중 2가지 이상으로 구성되는지 확인.

    ///^(?=.*[a-zA-Z])(?=.*[!@#$%^*+=-])(?=.*[0-9]).{6,16}$/;
    //(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[!@#$%^&*]).{8,16}
    //조건식이 8~16자 영문/숫자/특수문자(!@$%^&* 만 허용) 중 3종류를 조합으로 사용하실 수 있습니다.
    //@"^[a-zA-Z]+[0-9]*$");
    //private string idPattern = @"^[a-zA-Z0-9]*$";

    // 테스트 정규식.
    //private string aPattern = @"^[a-zA-Z0-9]{4,20}$";   //영문&숫자로 제한, 길이 4~20으로 제한. -> idPattern
    private string bPattern = @"[a-zA-Z]";              //영문이 있는지.
    private string cPattern = @"[0-9]"; //@"[\d]";      //숫자가 있는지.
    //private string dPattern = @"[가-힣]";               //한글이 있는지.
    //private string ePattern = @"[!@#$%^&*()-_+=]";      //특수문자가 있는지.
    
    public void CheckLoginInfomation(string id, string pw) {
        if (id == null || pw == null) {
            GuiMgr.GetInst().FailLogin("Empty id or pw"); return;
        } else {
            if (!Regex.IsMatch(id, idPattern) || !Regex.IsMatch(id, bPattern) || !Regex.IsMatch(id, cPattern)) {
                GuiMgr.GetInst().FailLogin("아이디는 영문과 숫자 조합의 4자 이상 20자 이하만 가능합니다."); return;
            }
            if (!Regex.IsMatch(pw, pwPattern)) {
                GuiMgr.GetInst().FailLogin("wrong pw"); return;
            } else {
                userID = null;
                candidateID = id;

                string requestJson = JsonParser.MakeJson(new UserInfo(id, pw));
                HttpReqMgr.GetInst().ReqPost("login", requestJson, OnLoginComplete); return;
            }
        }
    }

    private void OnLoginComplete(string responseJson)
    {
        if (responseJson == null) {
            GuiMgr.GetInst().FailLogin("Server connection failed.. please retry"); //please recover
            //GuiMgr.GetInst().SuccessLogin("Login success! Wellcome TEST_ID"); //imsi
            return;
        }

        LoginResponse loginResponse = JsonParser.GetResponseJsonClassObject<LoginResponse>(responseJson);
        if ("ok".Equals(loginResponse.result)) {
            userID = candidateID;
            GuiMgr.GetInst().SuccessLogin("Login success! Wellcome " + userID); return;
        } else {
            GuiMgr.GetInst().FailLogin("Login denied.. please retry"); return;
        }
    }

    public static string GetUserID() {
        return userID;
    }

}