using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
    

public class PlayBtn : MonoBehaviour {

	
	
	
	void Update () {

        Vector3 rotVec = new Vector3(0, 0, Mathf.Sin(Time.time));
         transform.Rotate(rotVec);

        if (Input.GetKeyDown(KeyCode.Escape))
            BackToMenu();
	}

    public void StartTheGame()
    {
        GameObject.Find("GameScreen").transform.DOLocalMoveX(0, .25f);
        FindObjectOfType<gamecontroller>().StartGame();
    }

    void BackToMenu()
    {
        GameObject.Find("GameScreen").transform.DOLocalMoveX(1100, .25f);
    }
}
 