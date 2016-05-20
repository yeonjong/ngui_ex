using UnityEngine;

public class PatchPanel : MonoBehaviour {

    private UISlider patchProgress;

    void Awake() {
        transform.FindChild("btn_forward_to_lobby").gameObject.SetActive(false);
        patchProgress = transform.FindChild("progress_bar_patch_persent").GetComponent<UISlider>();
    }

    public void ClickForwardToIntroBtn() {
        GameStateMgr.GetInst().ForwardState(GAME_STATE.IntroState);
    }

    public void ClickForwardToLobbyBtn() {
        GameStateMgr.GetInst().ForwardState(GAME_STATE.LobbyState);
    }

    public void SetPatchProgress(float percent) {
        patchProgress.value = percent;
    }

    public void ShowLobbyBtn() {
        transform.FindChild("btn_forward_to_lobby").gameObject.SetActive(true);
    }

}