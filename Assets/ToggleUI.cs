using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    public GameObject checkMark;
    public SceneSwitcher sSwitcher;
    public bool isMajor;
    // Start is called before the first frame update
    public void ToggleCheckMark()
    {
        checkMark.SetActive(!checkMark.activeSelf);
    }

    public void ToggleNote(int _index)
	{
		sSwitcher.ToggleNote(_index, isMajor);
	}
}
