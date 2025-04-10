using UnityEngine;

public class UI_RoundStartCanvas : MonoBehaviour
{
    Canvas _roundStartCanvas;
    UI_TypingEffectText _typingEffectText;
    string _textContent;
    float _disappearTime = 2f;

    void Awake()
    {
        _roundStartCanvas = GetComponent<Canvas>();
    }

    void Start()
    {
        _typingEffectText = GetComponentInChildren<UI_TypingEffectText>();
        Manager.UI.toggleRoundStartCanvasAction += ToggleRoundStartCanvas;
    }

    // 라운드 시작 캔버스 토글
    void ToggleRoundStartCanvas()
    {
        _roundStartCanvas.enabled = !_roundStartCanvas.enabled;
        if(_roundStartCanvas.enabled)
        {
            string text = (Manager.Game.CurrentRound == 2) ? $"Hidden Round \n START " : $"Game \n START ";
            _textContent = text;

            Debug.LogWarning(Manager.Game.CurrentRound);
            _typingEffectText.StartTypingEffect(_textContent);
            StartCoroutine(Util.WaitTimeAfterPlayAction(_disappearTime, () => { 
                _roundStartCanvas.enabled = false;
            }));
        }
    }
}