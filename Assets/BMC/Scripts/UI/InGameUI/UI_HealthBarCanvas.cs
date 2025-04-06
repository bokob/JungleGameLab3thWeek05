using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBarCanvas : MonoBehaviour
{
    Canvas _healthBarCanvas;
    Slider _healthBarSlider;

    void Start()
    {
        Manager.UI.setHealthBarAction += SetHealthBar;
        _healthBarCanvas = GetComponent<Canvas>();
        _healthBarCanvas.worldCamera = Camera.main;
        _healthBarSlider = GetComponentInChildren<Slider>();
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void SetHealthBar(float health)
    {
        health /= 100f;
        _healthBarSlider.value = health;
    }
}