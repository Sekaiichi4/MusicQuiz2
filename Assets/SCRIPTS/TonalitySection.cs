using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TonalitySection : MonoBehaviour
{
    public GameObject togglesParent;
    public ToggleUI[] toggles;
    // Start is called before the first frame update
    void Start()
    {
        toggles = togglesParent.GetComponentsInChildren<ToggleUI>();
    }

    public void ToggleAll()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].ToggleCheckMark();
        }
    }
}
