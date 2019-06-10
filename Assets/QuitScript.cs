using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitScript : MonoBehaviour
{
    public GameObject eventSystem;

    private void Start()
    {
        eventSystem = GameObject.FindGameObjectWithTag("SceneSwitcher");
    }

    public void SwitchScene(string _name)
    {
        GameObject.Destroy(eventSystem);
        SceneManager.LoadScene(_name);

    }
}
