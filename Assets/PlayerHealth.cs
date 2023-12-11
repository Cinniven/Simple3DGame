using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Networking.Types;

public class PlayerHealth : MonoBehaviour
{

    private int _maxhp = 100, _hp;
    // Start is called before the first frame update
    void Start()
    {
        _hp = _maxhp;
    }

    private void TakeDamage(int damage)
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
