using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject endGameUI;
    public GameObject startGameUI;
    public GameObject midGameUI;
    public GameObject pausedGameUI;

    public enum GameState
    {
        MAINMENU,
        START,
        END,
        PAUSED
    };

    public static GameManager instance = null;
    public GameState currentState;

	public static int Points = 0;

	// Use this for initialization
	void Start () {
        currentState = GameState.MAINMENU;
        instance = this;
    }

    void Update()
    {
        if(currentState == GameState.START && Input.GetKeyDown("p"))
        {
            ChangeState(GameState.PAUSED);
        }
        else if (currentState == GameState.PAUSED && Input.GetKeyDown("p"))
        {
            ChangeState(GameState.START);
        }
    }
	
	public void ChangeState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.MAINMENU:
                startGameUI.SetActive(true);
                midGameUI.SetActive(false);
                endGameUI.SetActive(false);
                pausedGameUI.SetActive(false);

                Time.timeScale = 0;
                //UI ELEMENTS WILL BE SHOWN HERE (by activating their transform) / ALSO RESET ALL THE ACTIVES/NON-ACTIVE ELEMENTS
                break;
            case GameState.START:
                startGameUI.SetActive(false);
                midGameUI.SetActive(true);
                endGameUI.SetActive(false);
                pausedGameUI.SetActive(false);

                Time.timeScale = 1;
                //ALL UI ELEMENTS SHOULD DISAPEAR AND LEAVE ONLY THE ONES NEEDED FOR THE RUNNING GAME
                break;
            case GameState.PAUSED:
                startGameUI.SetActive(false);
                midGameUI.SetActive(true);
                endGameUI.SetActive(false);
                pausedGameUI.SetActive(true);

                Time.timeScale = 0;
                //MAYBE ACTIVATE A ELEMENT THAT JUST SAYS PAUSED
                break;
            case GameState.END:

                startGameUI.SetActive(false);
                midGameUI.SetActive(false);
                endGameUI.SetActive(true);
                pausedGameUI.SetActive(false);

                GameObject[] towers;
                towers = GameObject.FindGameObjectsWithTag("Tower");

                foreach (GameObject tower in towers)
                {
                    Destroy(tower);
                }
                GameObject[] prizes;
                prizes = GameObject.FindGameObjectsWithTag("Prize");

                foreach (GameObject prize in prizes)
                {
                    Destroy(prize);
                }

                Time.timeScale = 0.05f;
                
                //ACTIVATE ELENTS TO SHOW SCORE AND SHIT LIKE THAT
                break;
        }
    }

	public static bool TakeCoins(int Amount) {
		GameManager.Points -= Amount;
		return true;
	}

	public static bool GiveCoins(int Amount) {
		GameManager.Points += Amount;
		return true;
	}
}
