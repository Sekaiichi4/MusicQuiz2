using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    public GameObject checkMark;
    public SceneSwitcher sSwitcher;
    public int tonality;
    // Start is called before the first frame update
    public void ToggleCheckMark()
    {
        checkMark.SetActive(!checkMark.activeSelf);
    }

    public void ToggleNote(int _index)
    {
        if (_index != -1)
        {
            sSwitcher.ToggleNote(_index, tonality);
        }
        else
        {
            sSwitcher.ToggleAllNotes(tonality);
        }
    }
}
