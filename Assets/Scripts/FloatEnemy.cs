using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;

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
    private TextMeshProUGUI _scoreUi;

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
        _player.score += 5;
        addScore(_player.score);
        StopAllCoroutines();
        Destroy(gameObject);
        // Die Animation
        //Disable the enemy
        GetComponent<SphereCollider>().enabled = false;
        gameObject.SetActive(false);
        this.enabled = false;
    }

    private void addScore(int score)
    {
        _scoreUi.text = "Score: " + score.ToString();
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
                //MoveTowardsPlayer(_lookTime);
                var end = Time.time + 3f;
                StartCoroutine(MoveTowardsP(end));
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
    IEnumerator MoveTowardsP(float duration)
    {
        var move = 4f * Time.deltaTime;
        while (Time.time < duration)
        {
            var targetDirection = _player.collectPoint.transform.position - this.transform.position;
            var newLookDirection = Vector3.RotateTowards(this.transform.forward, targetDirection, move, 0f);
            this.transform.rotation = Quaternion.LookRotation(newLookDirection);
            yield return new WaitForSeconds(.01f);
        }
        Instantiate(_myProj, _firePoint.transform.position, _firePoint.transform.rotation);
        if (_inRange)
        {
            duration = Time.time + 3f;
            StartCoroutine(MoveTowardsP(duration));
        }
        _isFiring = false;
        StopCoroutine(MoveTowardsP(duration));
    }
}

