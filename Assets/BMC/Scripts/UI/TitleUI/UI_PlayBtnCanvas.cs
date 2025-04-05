using UnityEngine;
using UnityEngine.UI;

public class UI_PlayBtnCanvas : MonoBehaviour
{
    Canvas _playBtnCanvas;
    Button _playBtn;

    void Start()
    {
        Manager.UI.toggleTitleBtns += TogglePlayBtnCanvas;

        _playBtnCanvas = GetComponent<Canvas>();

        // 플레이 버튼
        _playBtn = GetComponentInChildren<Button>();
        _playBtn.onClick.AddListener(() => { Manager.Scene.SwitchScene(Define.SceneType.InGameScene); });
    }

    // 플레이 버튼 캔버스 토글
    public void TogglePlayBtnCanvas()
    {
        _playBtnCanvas.enabled = !_playBtnCanvas.enabled;
    }
}