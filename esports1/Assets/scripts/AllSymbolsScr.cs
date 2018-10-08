using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;


public class AllSymbolsScr : MonoBehaviour, IPointerDownHandler
{


    int state = 0;
    gamescreen gs;
    public Vector3 startPos;
    CurrCells selfCell;

    private void Awake()
    {
        gs = FindObjectOfType<gamescreen>();
        startPos = transform.localPosition;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {

        if (gs.CanPress)
        {
            if (!gs.IsStarted)
                gs.IsStarted = true;

            if (state == 0)
            {
                if (gs.CurrSymbols.Find(x => x.GetComponent<CurrCells>().selfChar == null))
                {
                    CurrCells tmpCell = gs.CurrSymbols.Find(x => x.GetComponent<CurrCells>().selfChar == null).GetComponent<CurrCells>();
                    transform.DOMove(tmpCell.transform.position, .25f);
                    tmpCell.selfChar = gameObject;
                    selfCell = tmpCell;
                    state = 1;
                    CheckForWin();
                }
            }
            else           
                ReturnToHome();
                            
                }       
    }
    
    void CheckForWin()

    {
        if (!gs.CurrSymbols.Exists(x => x.GetComponent<CurrCells>().selfChar == null))
        {
            string tmpTeamName = "";

            foreach(GameObject go in gs.CurrSymbols)
            {
                if (go.GetComponent<CurrCells>().selfChar.GetComponent<Text>().text != "_")
                    tmpTeamName += go.GetComponent<CurrCells>().selfChar.GetComponent<Text>().text;
                else
                    tmpTeamName += " ";
            }

            if (tmpTeamName == gs.currTeam.Name.ToUpper())
                gs.NextTeam();
        }
    }

    public void ReturnToHome()
    {
        transform.DOLocalMove(startPos, .25f);
        selfCell.selfChar = null;
        state = 0;
    }

}