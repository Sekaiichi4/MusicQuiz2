using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject MainMenu, ContentSelectionMenu, MixedSectionParent;
    public TonalitySection MajorSection, MinorSection;
    public TonalitySection MixedMajorSection, MixedMinorSection, KcSection, KeSection, KgshSection;
    public int globalTonality;

    /// <summary>
    /// "majorA","majorAf","majorAsf","majorAsh","majorAssh","majorB","majorBf","majorBsf","majorBsshCf","majorC","majorCsfBsh","majorCsh","majorCssh","majorD","majorDf","majorDsf","majorDsh","majorDssh","majorE","majorEf","majorEsf","majorFsfEsh","majorEsshFf","majorF","majorFsh","majorFssh","majorG","majorGf","majorGsf","majorGsh","majorGssh"
    /// </summary>
    public bool[] majorList = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                                false, false, false, false, false, false, false, false, false, false, false, false, false};

    /// <summary>
    /// "minorA","minorAf","minorAsf","minorAsh","minorAssh","minorB","minorBf","minorBsf","minorBsshCf","minorC","minorCsfBsh","minorCsh","minorCssh","minorD","minorDf","minorDsf","minorDsh","minorDssh","minorE","minorEf","minorEsf","minorFsfEsh","minorEsshFf","minorF","minorFsh","minorFssh","minorG","minorGf","minorGsf","minorGsh","minorGssh"
    /// </summary>
    public bool[] minorList = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                                false, false, false, false, false, false, false, false, false, false, false, false, false};

    //Tonality 2
    public bool[] KcList = { false, false, false, false, false, false, false };
    //Tonality 3
    public bool[] KeList = { false, false, false, false, false, false, false };
    //Tonality 4
    public bool[] KgshList = { false, false, false, false, false, false, false };
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SwitchScreen(int _index)
    {
        MainMenu.SetActive(false);
        ContentSelectionMenu.SetActive(true);
        switch (_index)
        {
            case -1:
                MajorSection.gameObject.SetActive(false);
                MinorSection.gameObject.SetActive(false);
                MixedSectionParent.SetActive(true);
                break;
            case 0:
                MajorSection.gameObject.SetActive(true);
                MinorSection.gameObject.SetActive(false);
                MixedSectionParent.SetActive(false);
                break;

            case 1:
                MajorSection.gameObject.SetActive(false);
                MinorSection.gameObject.SetActive(true);
                MixedSectionParent.SetActive(false);
                break;
        }
    }

    public void SwitchScene(string _name)
    {
        Debug.Log("PRESSED");
        SceneManager.LoadScene(_name);
    }

    public void DecideStart(int i)
    {
        PlayerPrefs.SetInt("Tonality", i);
        globalTonality = i;
    }

    public void ToggleNote(int _index, int _tonality)
    {
        switch (_tonality)
        {
            case 0:
                //Major
                majorList[_index] = !majorList[_index];
                break;
            case 1:
                //Minor
                minorList[_index] = !minorList[_index];
                break;
            case 2:
                //Kc
                KcList[_index] = !KcList[_index];
                break;
            case 3:
                //Ke
                KeList[_index] = !KeList[_index];
                break;
            case 4:
                //Kgsh
                KgshList[_index] = !KgshList[_index];
                break;
        }
    }

    public void ToggleAllNotes(int _tonality)
    {
        switch (_tonality)
        {
            case 0:
                //Major
                for (int i = 0; i < majorList.Length; i++)
                {
                    majorList[i] = !majorList[i];
                }
                if (globalTonality == -1)
                {
                    MixedMajorSection.ToggleAll();
                }
                else
                {
                    // MajorSection.ToggleAll();
                }
                break;
            case 1:
                //Minor
                for (int i = 0; i < minorList.Length; i++)
                {
                    minorList[i] = !minorList[i];
                }
                if (globalTonality == -1)
                {
                    MixedMinorSection.ToggleAll();
                }
                else
                {
                    // MinorSection.ToggleAll();
                }
                break;
            case 2:
                //Kc
                for (int i = 0; i < KcList.Length; i++)
                {
                    KcList[i] = !KcList[i];
                }
                if (globalTonality == -1)
                {
                    KcSection.ToggleAll();
                }
                else
                {
                }
                break;
            case 3:
                //Kc
                for (int i = 0; i < KeList.Length; i++)
                {
                    KeList[i] = !KeList[i];
                }
                if (globalTonality == -1)
                {
                    KeSection.ToggleAll();
                }
                else
                {
                }
                break;
            case 4:
                //Kc
                for (int i = 0; i < KgshList.Length; i++)
                {
                    KgshList[i] = !KgshList[i];
                }
                if (globalTonality == -1)
                {
                    KgshSection.ToggleAll();
                }
                else
                {
                }
                break;
        }
    }
}



