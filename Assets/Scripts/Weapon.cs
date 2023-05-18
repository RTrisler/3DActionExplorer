using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;

    Collider triggerBox;

    private void Awake()
    {
        triggerBox = GetComponent<Collider>();
        triggerBox.gameObject.SetActive(true);
        triggerBox.isTrigger = true;
        triggerBox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("WeaponTrigger");
        if (other.tag == "Enemy")
        {
            var enemy = other.gameObject.GetComponent<FloatEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    public void EnableTriggerBox()
    {
        triggerBox.enabled = true;
    }

    public void DisableTriggerBox()
    {
        triggerBox.enabled = false;
    }
}
