using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpChargeBar : MonoBehaviour
{
    public Slider slider;

    public void SetCharge(float charge)
    {
        slider.value = charge;
    }

}
