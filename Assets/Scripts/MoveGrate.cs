using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGrate : MonoBehaviour
{
    [SerializeField]
    private Animator _gateAnimation;

    private void Start()
    {
        _gateAnimation = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        CollectPoint.OnEnterCollectPoint += changeBool;
    }
    private void OnDisable()
    {
        CollectPoint.OnEnterCollectPoint -= changeBool;
    }

    void changeBool()
    {
        _gateAnimation.SetBool("openGate", true);
    }
}
