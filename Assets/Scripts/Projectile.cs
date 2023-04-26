using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour, IDamageAble
{
    public static event Action OnPlayerHit;
    private Rigidbody _myBody;
    [SerializeField]
    private float _projSpeed;
    [SerializeField]
    private int _damageAmount;

    public void Damage(PlayerManager playerManager)
    {
        playerManager.health -= _damageAmount;
        OnPlayerHit?.Invoke();
        Debug.Log(playerManager.health);
        Destroy(gameObject);
    }

    void Start()
    {
        _myBody = GetComponent<Rigidbody>();
        _myBody.velocity = transform.forward * _projSpeed;
    }

}
