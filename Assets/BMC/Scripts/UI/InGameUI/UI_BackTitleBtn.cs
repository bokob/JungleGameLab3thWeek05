using UnityEngine;
using UnityEngine.UI;

public class UI_BackTitleBtn : MonoBehaviour
{
    Button _backTitleBtn;

    void Start()
    {
        _backTitleBtn = GetComponent<Button>();
        _backTitleBtn.onClick.AddListener(() =>
        {
            Manager.Scene.SwitchScene(Define.SceneType.TitleScene);
        });
    }
}