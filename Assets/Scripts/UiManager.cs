using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static event Action DoneFading;

    [SerializeField]
    private TextMeshProUGUI _interactUi;

    [SerializeField]
    private TextMeshProUGUI _scoreUi;

    [SerializeField]
    private TextMeshProUGUI _STEPUi;

    [SerializeField]
    private Image _blackOut;

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
        PlayerManager.OnPlayerMoved += fadeOut;
        PlayerManager.OnDeathBox += fadeIn;
        ScoreCollect.OnScoreCollect += addScore;
        ScoreCollect.OnSTEPCollect += updateSTEPUi;
    }
    private void OnDisable()
    {
        PlayerManager.OnInteractionHit -= changeInteractionUi;
        PlayerManager.OnInteractionNotHit -= clearInteractUi;
        PlayerManager.OnDeathBox -= fadeIn;
        PlayerManager.OnPlayerMoved -= fadeOut;
        ScoreCollect.OnScoreCollect -= addScore;
        ScoreCollect.OnSTEPCollect -= updateSTEPUi;
        PlayerManager.OnSpecialInteractionHit -= changeSpecialInteractionUi;
    }

    private void changeInteractionUi(IInteractable interaction)
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
        if (_interactUi.text == "")
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

    private void fadeIn()
    {
        StartCoroutine(fadeToBlack());
    }

    private void fadeOut()
    {
        StartCoroutine(fadeToClear());
    }
    IEnumerator fadeToBlack()
    {
        var alpha = _blackOut.color;
        Debug.Log(alpha.a);
        if (alpha.a < 1)
        {
            alpha.a += .05f;
            _blackOut.color = alpha;
            yield return new WaitForSeconds(.05f);
            StartCoroutine(fadeToBlack());
        }
        else
        {
            DoneFading?.Invoke();
        }
    }
    IEnumerator fadeToClear()
    {
        var alpha = _blackOut.color;
        if(alpha.a > .97f)
        {
            Debug.Log("waiting");
            yield return new WaitForSeconds(1);
        }
        if (alpha.a > 0)
        {
            alpha.a -= .05f;
            _blackOut.color = alpha;
            yield return new WaitForSeconds(.05f);
            StartCoroutine(fadeToClear());
        }
        else
        {
            StopAllCoroutines();
        }
    }
}
