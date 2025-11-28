using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider _healthbar;
    public Health _playerHealth;
    // Start is called before the first frame update
    private void Start()
    {
        _healthbar.maxValue = _playerHealth._maxHealth;
        _playerHealth._onHealthChanged += UpdateHealth;
        UpdateHealth(_playerHealth._maxHealth);
    }

    // Update is called once per frame
    void UpdateHealth(float _currentHealth)
    {
        _healthbar.value = _currentHealth;
    }
}
