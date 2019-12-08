using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    //public List<AudioClip> allAudio;
    public GameObject playingScreen;
    public GameObject playButton;
    public GameObject ratingScreen;
    public Text title;
    public GameObject breakScreen;
    public GameObject finishScreen;
    public bool isListening, isPractice, isMajor, isMinor, isKc, isKe, isKgsh;
    public int indexListening, indexPractice, index, fakeIndex, hadBreaks;

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
    public bool[] KcList = { false, false, false, false, false, false, false, };
    //Tonality 3
    public bool[] KeList = { false, false, false, false, false, false, false, };
    //Tonality 4
    public bool[] KgshList = { false, false, false, false, false, false, false, };

    private string[] majorNames = { "majorA","majorAf","majorAsf","majorAsh","majorAssh","majorB","majorBf","majorBsf","majorBsshCf","majorC","majorCsfBsh",
                                    "majorCsh","majorCssh","majorD","majorDf","majorDsf","majorDsh","majorDssh","majorE","majorEf","majorEsf","majorFsfEsh",
                                    "majorEsshFf","majorF","majorFsh","majorFssh","majorG","majorGf","majorGsf","majorGsh","majorGssh"};

    private string[] minorNames = { "minorA","minorAf","minorAsf","minorAsh","minorAssh","minorB","minorBf","minorBsf","minorBsshCf","minorC","minorCsfBsh",
                                    "minorCsh","minorCssh","minorD","minorDf","minorDsf","minorDsh","minorDssh","minorE","minorEf","minorEsf","minorFsfEsh",
                                    "minorEsshFf","minorF","minorFsh","minorFssh","minorG","minorGf","minorGsf","minorGsh","minorGssh"};

    private List<Song> currentList = new List<Song>();

    private int randomInt;

    private AudioSource audioSource;

    private CsvReadWrite csvWriter;

    public SceneSwitcher sSwitcher;


    void Start()
    {
        sSwitcher = GameObject.FindGameObjectWithTag("SceneSwitcher").GetComponent<SceneSwitcher>();
        majorList = sSwitcher.majorList;
        minorList = sSwitcher.minorList;
        KcList = sSwitcher.KcList;
        KeList = sSwitcher.KeList;
        KgshList = sSwitcher.KgshList;


        MajorOrMinor(PlayerPrefs.GetInt("MajorOrMinor"));

        audioSource = GetComponent<AudioSource>();
        initKeysDatabase(); //TODO CHECK DATABASE FOR TRUE VALUE BEFORE INITING
        InitDatabase();
        index = 0;
        fakeIndex = 0;
        indexListening = 0;
        indexPractice = 0;
        hadBreaks = 0;

        csvWriter = GetComponent<CsvReadWrite>();
        csvWriter.Save("Note", "Score");
        SetTitle();

        //todo: Let CSVWRITER print the first line through a method or something.
    }

    public void InitDatabase()
    {
        for (int j = 0; j < 31; j++)
        {
            for (int i = 1; i <= 31; i++)
            {
                AudioClip tune;

                if (isMajor)
                {
                    if (majorList[j])
                    {
                        tune = (AudioClip)Resources.Load("Sounds/major/" + majorNames
                        [j] + "/" + majorNames
                        [j] + i);
                        Song currentSong = new Song(tune, false, tune.name);
                        currentList.Add(currentSong);
                        Debug.Log("Added " + tune.name);
                    }
                }
                if (isMinor)
                {
                    if (minorList[j])
                    {
                        tune = (AudioClip)Resources.Load("Sounds/minor/" + minorNames
                        [j] + "/" + minorNames
                        [j] + i);
                        Song currentSong = new Song(tune, false, tune.name);
                        currentList.Add(currentSong);
                        Debug.Log("Added " + tune.name);
                    }
                }
            }
        }
    }

    public void initKeysDatabase()
    {
        for (int j = 1; j <= 7; j++)
        {
            for (int i = 1; i <= 31; i++)
            {
                AudioClip tune;

                if (isKc)
                {
                    if (KcList[j - 1])
                    {
                        tune = (AudioClip)Resources.Load("Sounds/Kc/KcT" +
                        j + "N" + i);
                        Song currentSong = new Song(tune, false, tune.name);
                        currentList.Add(currentSong);
                        Debug.Log("Added " + tune.name);
                    }
                }

                if (isKe)
                {
                    if (KeList[j - 1])
                    {
                        tune = (AudioClip)Resources.Load("Sounds/Ke/KeT" +
                        j + "N" + i);
                        Song currentSong = new Song(tune, false, tune.name);
                        currentList.Add(currentSong);
                        Debug.Log("Added " + tune.name);
                    }
                }

                if (isKgsh)
                {
                    if (KgshList[j - 1])
                    {
                        tune = (AudioClip)Resources.Load("Sounds/Kgsh/KgshT" +
                        j + "N" + i);
                        Song currentSong = new Song(tune, false, tune.name);
                        currentList.Add(currentSong);
                        Debug.Log("Added " + tune.name);
                    }
                }
            }
        }
    }

    public void PlaySong()
    {
        if (isListening)
        {
            playButton.SetActive(false);
            AudioClip currentTune = currentList[randomInt].audio;   //Get audio from list at random 

            if (indexListening == 9)
            {
                isListening = false;

                if (hadBreaks == 0)
                {
                    isPractice = true;
                }
            }
            Invoke("GetPlayingScreen", currentTune.length + 0.25f);

            indexListening++;

            audioSource.PlayOneShot(currentTune);
        }
        else if (isPractice)
        {
            playButton.SetActive(false);
            AudioClip currentTune = currentList[randomInt].audio;   //Get audio from list at random 

            Invoke("GetRatingScreen", currentTune.length + 0.25f);

            audioSource.PlayOneShot(currentTune);
        }
        else
        {
            randomInt = Random.Range(0, currentList.Count);

            while (currentList[randomInt].played)
            {
                randomInt = Random.Range(0, currentList.Count);
            }

            playButton.SetActive(false);
            AudioClip currentTune = currentList[randomInt].audio;   //Get audio from list at random 
            currentList[randomInt].played = true;                   //and enable the bool

            Invoke("GetRatingScreen", currentTune.length + 0.25f);

            audioSource.PlayOneShot(currentTune);
        }
    }

    public void SendRating(string _rating)
    {
        if (isPractice)
        {
            if (indexPractice == 9)
            {
                isPractice = false;
            }

            if (index != 0)
            {
                fakeIndex++;
            }

            indexPractice++;

            GetPlayingScreen();
        }
        else
        {
            csvWriter.Save(currentList[randomInt].name, _rating);
            index++;
            fakeIndex++;

            if (index == 192 || index == 192 * 2 || index == 192 * 3 || index == 192 * 4)
            {
                GetBreakScreen();
            }
            else if (index == currentList.Count)
            {
                GetFinishScreen();
            }
            else
            {
                GetPlayingScreen();
            }
        }
    }

    public void EndBreak()
    {
        breakScreen.SetActive(false);
        indexPractice = 7;
        isPractice = true;
        GetPlayingScreen();
        hadBreaks++;
    }

    void MajorOrMinor(int i)
    {

        isMinor = true;
        isMajor = true;
        isKc = true;
        isKe = true;
        isKgsh = true;

    }

    void SetTitle()
    {
        if (isListening)
        {
            title.text = "Listening " + (indexListening + 1).ToString() + "/10";
        }
        else if (isPractice)
        {
            if (index == 0)
            {
                title.text = "Practice " + (indexPractice + 1).ToString() + "/10";
            }
            else
            {
                title.text = "Trial " + (fakeIndex + 1).ToString();
            }
        }
        else
        {
            title.text = "Trial " + (fakeIndex + 1).ToString();
        }
    }

    void GetRatingScreen()
    {
        playingScreen.SetActive(false);
        ratingScreen.SetActive(true);
    }

    void GetPlayingScreen()
    {
        playButton.SetActive(true);
        NextMelody();
        ratingScreen.SetActive(false);
        SetTitle();
        playingScreen.SetActive(true);
    }

    void GetBreakScreen()
    {
        playingScreen.SetActive(false);
        ratingScreen.SetActive(false);
        breakScreen.SetActive(true);
    }

    void GetFinishScreen()
    {
        playingScreen.SetActive(false);
        ratingScreen.SetActive(false);
        breakScreen.SetActive(false);
        finishScreen.SetActive(true);
    }

    void NextMelody()
    {
        randomInt = Random.Range(0, currentList.Count);
    }

}
