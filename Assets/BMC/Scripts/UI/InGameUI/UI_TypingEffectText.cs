using System.Collections;
using UnityEngine;
using TMPro;

// TextMeshPro UI 타이핑 효과
public class UI_TypingEffectText : MonoBehaviour
{
    TextMeshProUGUI _text;
    string _textContent;

    void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _textContent = _text.text;
        _text.text = "";
    }

    // 타이핑 효과 코루틴 시작
    public void StartTypingEffect(string text = "")
    {
        // 특정 문구가 파라미터로 있는 경우
        if (!string.IsNullOrEmpty(text))
        {
            Debug.LogWarning(text);
            _textContent = text;
        }

        StartCoroutine(TypingEffectCoroutine());
    }

    // 타이핑 효과 코루틴
    IEnumerator TypingEffectCoroutine()
    {
        for(int i=0; i < _textContent.Length; i++)
        {
            _text.text = _textContent[..i];
            yield return new WaitForSeconds(0.1f);
        }
    }
}