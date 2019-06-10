using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
	public GameObject MainMenu, ContentSelectionMenu, MajorSection, MinorSection, MixedSection;

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
			case 0:
			MajorSection.SetActive(true);
			MinorSection.SetActive(false);
			MixedSection.SetActive(false);
			break;

			case 1:
			MajorSection.SetActive(false);
			MinorSection.SetActive(true);
			MixedSection.SetActive(false);
			break;

			case 2:
			MajorSection.SetActive(false);
			MinorSection.SetActive(false);
			MixedSection.SetActive(true);
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
        PlayerPrefs.SetInt("MajorOrMinor", i);
    }

    public void ToggleNote(int _index, bool _isMajor)
    {
        if (_isMajor)
        {
            majorList[_index] = !majorList[_index];
        }
		else
		{
			minorList[_index] = !minorList[_index];
		}
    }
}



