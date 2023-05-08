using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;

    BoxCollider triggerBox;

    private void Start()
    {
        triggerBox = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("TriggerEnter");
        var enemy = other.gameObject.GetComponent<FloatEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    private void EnableTriggerBox()
    {
        triggerBox.enabled = true;
    }

    private void DisableTriggerBox()
    {
        triggerBox.enabled = false;
    }
}
