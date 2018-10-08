using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class gamescreen : MonoBehaviour
{
    public int Score=0;
    public Text TeamName, ScoreText;
    public Image TeamLogo;
    public GameObject CurrCellPref;
    public Transform CurrCellsGrid, CurrCellsGrid2;
    public bool CanPress = true, IsStarted = false, losed=false ;


    public List<eSportTeam> currTeams = new List<eSportTeam>();
    public List<GameObject> AllSymbols = new List<GameObject>();
    public List<GameObject> CurrSymbols = new List<GameObject>();

    public eSportTeam currTeam;

    public float GameTime,GamemaxTime;

    private void Awake()
    {
        GamemaxTime = 15;
    }

    public void RefreshTeam()
    {
        GameTime = GamemaxTime;
        FindObjectOfType<TimeBarScr>().RefreshColor();

        losed = false;

        TeamName.text = "???";
        ScoreText.text = Score.ToString();

        CanPress = true;
        currTeam = currTeams[0];
        TeamLogo.sprite = currTeam.Logo;
      
        foreach(GameObject go in AllSymbols)       
          go.GetComponent<Text>().text = "-";

        foreach (GameObject go in CurrSymbols)
        {
            if (go.GetComponent<CurrCells>().selfChar != null)
                go.GetComponent<CurrCells>().selfChar.GetComponent<AllSymbolsScr>().ReturnToHome();

            Destroy(go);
        }

        CurrSymbols.Clear();

        foreach (char ch in currTeam.Name.ToUpper().ToCharArray())
        {
            int rnd = Random.Range(0, AllSymbols.Count);

            while (AllSymbols[rnd].GetComponent<Text>().text != "-")
                rnd = Random.Range(0, AllSymbols.Count);

            if (ch != ' ')
                AllSymbols[rnd].GetComponent<Text>().text = ch.ToString();
            else
                AllSymbols[rnd].GetComponent<Text>().text = '_'.ToString();

        }

        foreach (GameObject go in AllSymbols)
        {
            if (go.GetComponent<Text>().text == "-")
                    go.GetComponent<Text>().text = ((char)Random.Range(65, 91)).ToString();
        }

            for (int i = 0; i < currTeam.Name.ToCharArray().Length; i++ )
       
        {
            GameObject tmpCell = Instantiate(CurrCellPref);

            if (i >= 7)
            tmpCell.transform.SetParent(CurrCellsGrid2, false);
            else
                tmpCell.transform.SetParent(CurrCellsGrid, false);

            CurrSymbols.Add(tmpCell);

        }
    }
    public void NextTeam()
    {
        CanPress = false;
        TeamName.text = currTeam.Name.ToUpper();
        
        StartCoroutine(LoadNextTeam());
    }

    IEnumerator LoadNextTeam()

    {
        Score++;
        ScoreText.text = Score.ToString();

        yield return new WaitForSeconds(.5f);
       

        currTeams.RemoveAt(0);
        if (currTeams.Count > 0)
            RefreshTeam();
        else
            Win();
    }

    void Win()
    {

    
    }

    void Lose()
    {
        losed = true;
        SaveHighscore();
        GameObject.Find("LosePanel").transform.DOMoveY(0, .25f);

        GameObject.Find("ScoreTxt").GetComponent<Text>().text += Score;
        GameObject.Find("HighscoreTxt").GetComponent<Text>().text += PlayerPrefs.GetInt("highscore");
    }

    private void Update()
    {
        if (GameTime > 0 && CanPress && IsStarted)
            GameTime -= Time.deltaTime;
        else if (GameTime < 0 && !losed)
            Lose();
    }
   void SaveHighscore()
    {
        if (Score > PlayerPrefs.GetInt ("highscore", 0))
        PlayerPrefs.SetInt("highscore", Score);
    }
}
