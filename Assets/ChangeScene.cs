using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
     public void testfn()
    {
        Debug.Log("test fn clicked");
    }
    public void MoveToScene(int sceneID)
    {
        Debug.Log("Button clicked");
        SceneManager.LoadScene(sceneID);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
