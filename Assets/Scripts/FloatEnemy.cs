using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class FloatEnemy : MonoBehaviour
{
    private bool _isFiring;
    private PlayerManager _player;
    [SerializeField]
    private float _lookTime;

    [SerializeField]
    private GameObject _myProj;

    [SerializeField]
    private Transform _firePoint;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            _player = player;
            if (!_isFiring)
            {
                _isFiring = true;
                MoveTowardsPlayer(_lookTime);
            }
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
        _isFiring = false;
        Debug.Log("Task finished");
    }
}
