using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float _maxHealth = 100f;
    private float _currentHealth;

    public delegate void OnHealthChanged(float _health);
    public event OnHealthChanged _onHealthChanged;

    // Start is called before the first frame update
    private void Start()
    {
        _currentHealth = _maxHealth;
        _onHealthChanged?.Invoke(_currentHealth);
    }

    public void TakeDamage(float _damage)
    {
        Debug.Log("take damage");
        _currentHealth -= _damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        _onHealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        Destroy(gameObject);
    }
}
