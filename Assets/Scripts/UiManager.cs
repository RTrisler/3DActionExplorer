using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UiManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _interactUi;

    private void OnEnable()
    {
        _interactUi.text = "";
        PlayerManager.OnInteractionHit += changeInteractionUi;
        PlayerManager.OnInteractionNotHit += clearInteractUi;
    }
    private void OnDisable()
    {
        PlayerManager.OnInteractionHit -= changeInteractionUi;
        PlayerManager.OnInteractionNotHit -= clearInteractUi;
    }

    private void changeInteractionUi(IInteractable interaction)
    {
        if(_interactUi.text == "")
        {
            _interactUi.text = interaction.SendInteractUi();
        }
        else
        {
            return;
        }
    }

    private void clearInteractUi()
    {
        if(_interactUi.text == "")
        {
            return;
        }
        else
        {
            _interactUi.text = "";
        }
    }
}
