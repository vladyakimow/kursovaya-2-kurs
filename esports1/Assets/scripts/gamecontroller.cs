using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class eSportTeam
{
    public string Name;
    public Sprite Logo;

    public eSportTeam(string name, Sprite spr)
    {
        Name = name;
        Logo = spr;
    }
}

public class gamecontroller : MonoBehaviour
{

    public List<eSportTeam> AllTeams = new List<eSportTeam>();
    public gamescreen gs;

    void Awake()
    {
        DOTween.Init();
        LoadData();

    }
    void Start()
    {
       
       


        StartGame();

    }


    public void StartGame()
    {
        
        List<eSportTeam> tempAllTeams = AllTeams.FindAll(x => x != null),
                         currAllTeams = new List<eSportTeam>();

        int tempCount = tempAllTeams.Count;
        for (int i = 0; i < tempCount; i++)
        {
            int rnd = Random.Range(0, tempAllTeams.Count);
            currAllTeams.Add(tempAllTeams[rnd]);
            tempAllTeams.RemoveAt(rnd);
        }
        gs.currTeams = currAllTeams;
        gs.RefreshTeam();
        gs.IsStarted = false;
    }

    void LoadData()
    {
        Sprite[] allSprites = Resources.LoadAll<Sprite>("Sprites/Teams");
        foreach (Sprite spr in allSprites)
            AllTeams.Add(new eSportTeam(spr.name, spr));
    }
}