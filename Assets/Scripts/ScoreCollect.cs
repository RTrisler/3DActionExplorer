using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCollect : MonoBehaviour, IPickupable
{
    private bool _inRange;
    private PlayerManager _player;
    private Rigidbody _collectBody;

    [SerializeField]
    private ParticleSystem _myParticles;

    [SerializeField]
    private int _scoreValue;

    [SerializeField]
    private AudioClip _collectAudio;

    [SerializeField]
    private float _audioVolume;
    public void collect(PlayerManager playerManager)
    {
        SoundManager.Instance.playSoundQuick(_collectAudio, _audioVolume);
        Instantiate(_myParticles, transform.position, transform.rotation);
        playerManager.score += _scoreValue;
        Destroy(gameObject);
    }

    void Start()
    {
        _collectBody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            _inRange = true;
            _player = player;
            _collectBody.useGravity = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerManager>(out PlayerManager player))
        {
            _inRange = false;
            _player = null;
            _collectBody.useGravity = true;
        }
    }

    void Update()
    {
        if (_inRange)
        {
            var move = 4f * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(transform.position, _player.collectPoint.transform.position, move);
        }
    }
}
