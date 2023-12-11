using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    private Player _player;
    private Rigidbody _rb;
    private Vector2 _input;
    private Vector3 _mousePos;

    [SerializeField] private int _maxhp = 100, _hp, _ammoReserve, _maxAmmoReserve = 30, _maxAmmo = 6, _ammo, _arrowSpeed = 5;
    [SerializeField] private GameObject _cursor, _arrow, _bow;

    void Awake()
    {
        _player = new Player();
        _rb = GetComponent<Rigidbody>();
        if (_rb == null) Debug.LogError("Rigidbody not found");
        _hp = _maxhp;
        _ammo = _maxAmmo;
        _ammoReserve = _maxAmmoReserve;
        _cursor = GameObject.FindGameObjectWithTag("Cursor");
    }

    private void OnEnable()
    {
        _player.Overworld.Enable();
    }

    private void OnDisable()
    {
        _player.Overworld.Disable();
    }

    private void FixedUpdate()
    {
        _input = _player.Overworld.Movement.ReadValue<Vector2>();
        _rb.velocity = new Vector3(_input.x, 0, _input.y) * _speed;

        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _cursor.transform.position = _mousePos;
        _bow.transform.forward = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnShoot()
    {
        Debug.Log("bang!");

        if(_ammo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        GameObject arrow = Instantiate(_arrow, _bow.transform.position, _bow.transform.rotation);
        arrow.GetComponent<Rigidbody>().AddForce(_bow.transform.up * _arrowSpeed, ForceMode.Impulse);
        
    }

    public IEnumerator Reload()
    {
        if(_ammoReserve == 0) Debug.Log("No ammo");

        yield return new WaitForSeconds(3);

        if(_ammoReserve < _maxAmmo)
        {
            _ammo = _ammoReserve;
            _ammoReserve = 0;
        }
        else
        {
            _ammo = _maxAmmo;
            _ammoReserve = _ammoReserve - _maxAmmo;
        }
    }

    public void TakeDamage(int damage)
    {
        _hp = _hp - damage;
        if (_hp<=0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
