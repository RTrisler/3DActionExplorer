using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class FloatEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Slider healthSlider;

    private bool _isFiring;
    private bool _inRange;
    private PlayerManager _player;
    [SerializeField]
    private float _lookTime;

    [SerializeField]
    private GameObject _myProj;

    [SerializeField]
    private Transform _firePoint;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value += damage;
        // play hurt animation
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died");
        // Die Animation
        //Disable the enemy
        GetComponent<SphereCollider>().enabled = false;
        gameObject.SetActive(false);
        this.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            _inRange = true;
            _player = player;
            if (!_isFiring)
            {
                _isFiring = true;
                MoveTowardsPlayer(_lookTime);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            _inRange = false;
            _player = player;
        }
    }

    public async void MoveTowardsPlayer(float duration)
    {
        var end = Time.time + duration;
        var move = 4f * Time.deltaTime;
        while (Time.time < end)
        {
            var targetDirection = _player.collectPoint.transform.position - this.transform.position;
            var newLookDirection = Vector3.RotateTowards(this.transform.forward, targetDirection, move, 0f);
            this.transform.rotation = Quaternion.LookRotation(newLookDirection);
            await Task.Yield();
        }
        Instantiate(_myProj, _firePoint.transform.position, _firePoint.transform.rotation);
        if (_inRange)
        {
            MoveTowardsPlayer(duration);
        }
        _isFiring = false;
        Debug.Log("Task finished");
    }
}
