using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene()
    {
        Debug.Log("Load Scene!");
        SceneManager.LoadScene("level_keven");
    }
}
