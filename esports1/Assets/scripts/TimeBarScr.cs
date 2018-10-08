using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TimeBarScr : MonoBehaviour {

    public Color StartColor, EndColor;
    public gamescreen GS;
	
	private void Start()
    {
        GetComponent<Image>().color = StartColor;
    }
	
	void Update () {
	
        if (GS.CanPress)
        {

            GetComponent<Image>().fillAmount = GS.GameTime / GS.GamemaxTime;

            if (GS.IsStarted)
            {
                Color currColor = GetComponent<Image>().color;
                GetComponent<Image>().color = Color.Lerp(currColor, EndColor, Time.deltaTime / 5);
            }
        }
	}

    public void RefreshColor()
    {
        GetComponent<Image>().color = StartColor;
       
    }
}
