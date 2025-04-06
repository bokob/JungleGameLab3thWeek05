using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_GameClearCanvas : MonoBehaviour
{
    Canvas _gameClearCanvas;
    UI_TypingEffectText _typingEffectText;

    void Start()
    {
        Manager.UI.toggleGameClearCanvasAction += ToggleGameClearCanvas;
        _gameClearCanvas = GetComponent<Canvas>();
        _typingEffectText = GetComponentInChildren<UI_TypingEffectText>();
    }

    // 게임 클리어 캔버스 토글
    public void ToggleGameClearCanvas()
    {    
        _gameClearCanvas.enabled = !_gameClearCanvas.enabled;
        if(_gameClearCanvas.enabled)
        {
            _typingEffectText.StartTypingEffect();
        }
    }
}