using UnityEngine;

public class UI_GameOverCanvas : MonoBehaviour
{
    Canvas _gameOverCanvas;
    UI_TypingEffectText _typingEffectText;

    void Start()
    {
        Manager.UI.toggleGameOverCanvasAction += ToggleGameOverCanvas;
        _gameOverCanvas = GetComponent<Canvas>();
        _typingEffectText = GetComponentInChildren<UI_TypingEffectText>();
    }

    // 게임 클리어 캔버스 토글
    public void ToggleGameOverCanvas()
    {
        _gameOverCanvas.enabled = !_gameOverCanvas.enabled;
        if (_gameOverCanvas.enabled)
        {
            _typingEffectText.StartTypingEffect();
        }
    }
}