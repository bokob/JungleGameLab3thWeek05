using UnityEditor.Rendering;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    Status _status;

    void Start()
    {
        _status = GetComponent<Status>();
        _status.HitAction += () => { 
            Manager.UI.setHealthBarAction?.Invoke(_status.HP);
            Manager.Game.CameraController.ShakeCamera();
        };
        _status.DieAction += Die;
    }

    // 플레이어 사망
    // 사망 시 UI를 GameOver로 변경
    void Die()
    {
        Manager.UI.toggleGameOverCanvasAction.Invoke();
    }
}