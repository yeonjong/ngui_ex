using UnityEngine;

public class IntroPanel : MonoBehaviour {

    private string id;
    private string pw;

    public void SetID(string id) {
        this.id = id;
    }

    public void SetPW(string pw) {
        this.pw = pw;
    }

    public void ShowLoginPanel() {
        transform.FindChild("pnl_login").gameObject.SetActive(true);
        transform.FindChild("pnl_welcome").gameObject.SetActive(false);
    }

    public void ShowWelcomePanel(string welcomeMsg) {
        transform.FindChild("pnl_login").gameObject.SetActive(false);
        transform.FindChild("pnl_welcome").gameObject.SetActive(true);
        transform.FindChild("pnl_welcome/btn_welcome/Label").GetComponent<UILabel>().text = welcomeMsg;
    }

    public void InitLoginInputFields() {
        transform.FindChild("pnl_login/input_field_id/Label").GetComponent<UILabel>().text = null;
        transform.FindChild("pnl_login/input_field_pw/Label").GetComponent<UILabel>().text = null;
    }

    public void ClickLoginBtn() {
        GuiMgr.GetInst().TryLogin(id, pw);

        // TODO:
        // GameStateMgr.inst.ForwardState(GAME_STATE.LobbyState);
    }

    public void ClickWelcomeBtn() {
        GameStateMgr.GetInst().ForwardState(GAME_STATE.LobbyState);
    }

}