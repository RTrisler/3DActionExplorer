using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IDamageAble
{
    private Rigidbody _myBody;
    [SerializeField]
    private float _projSpeed;
    [SerializeField]
    private int _damageAmount;

    public void Damage(PlayerManager playerManager)
    {
        playerManager.health -= _damageAmount;
        Debug.Log(playerManager.health);
        Destroy(gameObject);
    }

    void Start()
    {
        _myBody = GetComponent<Rigidbody>();
        _myBody.velocity = transform.forward * _projSpeed;
    }

}
