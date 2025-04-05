using UnityEngine;
using UnityEngine.UI;

public class UI_LogoCanvas : MonoBehaviour
{
    Canvas _logoCanvas;

    void Start()
    {
        Manager.UI.toggleTitleBtns += ToggleLogoCanvas;

        _logoCanvas = GetComponent<Canvas>();
    }

    // 로고 캔버스 토글
    public void ToggleLogoCanvas()
    {
        //_playBtnCanvas.enabled = !_playBtnCanvas.enabled;
    }
}