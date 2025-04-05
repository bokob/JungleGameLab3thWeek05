using UnityEngine;
using UnityEngine.UI;

public class UI_HelpBtnCanvas : MonoBehaviour
{
    Canvas _helpBtnCanvas;
    Button _helpBtn;
    void Start()
    {
        Manager.UI.toggleTitleBtns += ToggleHelpBtnCanvas;

        _helpBtnCanvas = GetComponent<Canvas>();

        _helpBtn = GetComponentInChildren<Button>();
        _helpBtn.onClick.AddListener(() => {
            Manager.UI.toggleTitleBtns.Invoke();
            Manager.UI.toggleHelpPanel.Invoke(); });
    }

    // 도움말 버튼 캔버스 토글
    public void ToggleHelpBtnCanvas()
    {
        _helpBtnCanvas.enabled = !_helpBtnCanvas.enabled;
    }
}