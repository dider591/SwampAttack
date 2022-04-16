using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;

    private Weapon _currentWeapon;
    private int _curentWeaponNumber = 0;
    private int _currentHealth;
    private Animator _animator;

    public int Maney { get; private set; }

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> ManeyChanged;

    private void Start()
    {
        ChangeWeapon(_weapons[_curentWeaponNumber]);
        _currentHealth = _health;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentWeapon.Shoot(_shootPoint);
        }
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddManey(int maney)
    {
        Maney += maney;
        ManeyChanged?.Invoke(Maney);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Maney -= weapon.Price;
        ManeyChanged?.Invoke(Maney);
        _weapons.Add(weapon);
    }

    public void NextWeapon()
    {
        if (_curentWeaponNumber == _weapons.Count - 1)
        {
            _curentWeaponNumber = 0;
        }
        else
        {
            _curentWeaponNumber++;
        }

        ChangeWeapon(_weapons[_curentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        if (_curentWeaponNumber == 0)
        {
            _curentWeaponNumber = _weapons.Count - 1;
        }
        else
        {
            _curentWeaponNumber--;
        }

        ChangeWeapon(_weapons[_curentWeaponNumber]);
    }
   
    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }
}
