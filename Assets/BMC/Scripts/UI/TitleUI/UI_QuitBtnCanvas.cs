using UnityEngine;
using UnityEngine.UI;

public class UI_QuitBtnCanvas : MonoBehaviour
{
    Canvas _quitBtnCanvas;
    Button _quitBtn;

    void Start()
    {
        Manager.UI.toggleTitleBtns += ToggleQuitBtnCanvas;

        _quitBtnCanvas = GetComponent<Canvas>();

        // 종료 버튼
        _quitBtn = GetComponentInChildren<Button>();
        _quitBtn.onClick.AddListener(() => { Application.Quit(); });
    }

    // 종료 버튼 캔버스 토글
    public void ToggleQuitBtnCanvas()
    {
        _quitBtnCanvas.enabled = !_quitBtnCanvas.enabled;
    }
}