using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    AnimatorManager animatorManager;

    [SerializeField]
    private AudioClip _doorAudio;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage = 40;


    // Update is called once per frame
    private void Awake()
    {
        // Play attack animation
        animatorManager = GetComponent<AnimatorManager>();
    }

    public void HandleAttack()
    {
        Attack();
    }

    private void Attack()
    {
        animatorManager.PlayTargetAnimation("PlayerAttack", true);
        SoundManager.Instance.playSoundQuick(_doorAudio);
        // Detect enemies in range of the attack
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // Damage Enemy
        foreach(Collider enemy in hitEnemies)
        {
            Debug.Log("Hit enemy: " + enemy.name);
            if (enemy.GetComponent<FloatEnemy>() != null)
                enemy.GetComponent<FloatEnemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected() {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

