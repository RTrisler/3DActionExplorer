using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectPoint : MonoBehaviour
{
    public static event Action OnEnterCollectPoint;
    [SerializeField]
    private Transform _moveBallPoint;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PuzzleBall>(out PuzzleBall ball))
        {
            ball.transform.position = _moveBallPoint.position;
            ball._collectCollider.enabled = true;
            OnEnterCollectPoint?.Invoke();
        }
    }
}
