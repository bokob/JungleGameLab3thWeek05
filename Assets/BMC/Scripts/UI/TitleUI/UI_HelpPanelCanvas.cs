using UnityEngine;

public class UI_HelpPanelCanvas : MonoBehaviour
{
    Canvas _helpPanelCanvas;

    void Start()
    {
        Manager.UI.toggleHelpPanel += ToggleHelpPanelCanvas;
        _helpPanelCanvas = GetComponent<Canvas>();
    }

    // 도움말 패널 캔버스 토글
    public void ToggleHelpPanelCanvas()
    {
        _helpPanelCanvas.enabled = !_helpPanelCanvas.enabled;
    }
}