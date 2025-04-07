using UnityEngine;
using UnityEngine.UI;

public class UI_HelpPanelBackBtn : MonoBehaviour
{
    Button _backBtn;

    void Start()
    {
        _backBtn = GetComponent<Button>();
        _backBtn.onClick.AddListener(() =>
        {
            Manager.UI.toggleHelpPanel.Invoke();
            Manager.UI.toggleTitleBtns.Invoke();
        });
    }
}