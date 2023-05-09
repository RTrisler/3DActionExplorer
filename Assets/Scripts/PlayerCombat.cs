using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator anim;
    AnimatorManager animatorManager;

    [SerializeField]
    private AudioClip _swordAudio;
    [SerializeField]
    Weapon weapon;

    public List<AttackSO> combo;
    float lastClickedTime;
    float lastComboEnd;
    int comboCounter = 0;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage = 40;


    // Update is called once per frame
    private void Awake()
    {
        // Play attack animation
        animatorManager = GetComponent<AnimatorManager>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        ExitAttack();
    }

    public void HandleAttack()
    {
        Attack();
    }

    private void Attack()
    {

        if (Time.time - lastComboEnd > 0.5f && comboCounter <= combo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.5f)
            {
                anim.runtimeAnimatorController = combo[comboCounter].animatorOV;
                anim.Play("Attack",0,0);
                SoundManager.Instance.playSoundQuick(_swordAudio);
                weapon.damage = combo[comboCounter].damage;
                // Detect enemies in range of the attack
                Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

                // Damage Enemy
                foreach(Collider enemy in hitEnemies)
                {
                    Debug.Log("Hit enemy: " + enemy.name);
                    if (enemy.GetComponent<FloatEnemy>() != null)
                        enemy.GetComponent<FloatEnemy>().TakeDamage(weapon.damage);
                }
                comboCounter++;
                lastClickedTime = Time.time;

                if (comboCounter + 1 > combo.Count)
                {
                    comboCounter = 0;
                }
            }
        }

    }

    public void ExitAttack()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9 && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Debug.Log("Exit Attack");
            comboCounter = 0;
            Invoke("EndCombo", 1);
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }

    void OnDrawGizmosSelected() {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

