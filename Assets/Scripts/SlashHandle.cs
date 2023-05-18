using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashHandle : MonoBehaviour
{
    public GameObject slash;
    

    public void EnableSlash()
    {
        slash.SetActive(true);
    }

    public void DisableSlash()
    {
        slash.SetActive(false);
    }
}
