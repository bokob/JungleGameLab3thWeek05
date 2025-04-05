using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBarCanvas : MonoBehaviour
{
    Slider _healthBarSlider;

    void Start()
    {
        Manager.UI.setHealthBarAction += SetHealthBar;
        _healthBarSlider = GetComponentInChildren<Slider>();
    }

    public void SetHealthBar(float health)
    {
        health /= 100f;
        _healthBarSlider.value = health;
    }
}