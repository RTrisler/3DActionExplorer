using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UiManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _interactUi;

    [SerializeField]
    private TextMeshProUGUI _scoreUi;

    [SerializeField]
    private TextMeshProUGUI _STEPUi;

    private string S = "";
    private string T = "";
    private string E = "";
    private string P = "";

    private void OnEnable()
    {
        _interactUi.text = "Score: 0";
        _scoreUi.text = "0";
        _STEPUi.text = "";
        PlayerManager.OnInteractionHit += changeInteractionUi;
        PlayerManager.OnInteractionNotHit += clearInteractUi;
        PlayerManager.OnSpecialInteractionHit += changeSpecialInteractionUi;
        ScoreCollect.OnScoreCollect += addScore;
        ScoreCollect.OnSTEPCollect += updateSTEPUi;
    }
    private void OnDisable()
    {
        PlayerManager.OnInteractionHit -= changeInteractionUi;
        PlayerManager.OnInteractionNotHit -= clearInteractUi;
        ScoreCollect.OnScoreCollect -= addScore;
        ScoreCollect.OnSTEPCollect -= updateSTEPUi;
        PlayerManager.OnSpecialInteractionHit -= changeSpecialInteractionUi;
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
    private void changeSpecialInteractionUi(SpecialDoor interaction)
    {
        if (_interactUi.text == "")
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

    private void addScore(int score)
    {
        _scoreUi.text = "Score: " + score.ToString();
    }

    private void updateSTEPUi(STEP stepState)
    {
        switch (stepState)
        {
            case STEP.S:
                S = "S";
                break;
            case STEP.T:
                T = "T";
                break;
            case STEP.E:
                E = "E";
                break;
            case STEP.P:
                P = "P";
                break;
        }
        _STEPUi.text = S + T + E + P;
    }
}
