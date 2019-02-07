using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    inMenu,
    inFirstEscene,
    inGame,
    inMiniGame,
    inGameOver,
    inWin,
    inCredits
}
public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.inMenu;
    public static GameManager sharedInstance;

    public void ReloadLevel()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        Time.timeScale = 1.0f;
    }

    void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.D))||(Input.GetKeyDown(KeyCode.A)))
        {
            StartGame();

        }
    }
    public void inFirstEscene()
    {
        SetGameState(GameState.inFirstEscene);

    }
    public void StartGame()
    {
        SetGameState(GameState.inGame);
    }
    public void GoToMiniGame()
    {
        SetGameState(GameState.inMiniGame);

    }
    public void GameOver()
    {
        SetGameState(GameState.inGameOver);

    }
    public void BackToMenu()
    {
        SetGameState(GameState.inMenu);

    }
    public void Win()
    {
        SetGameState(GameState.inWin);
    }
    private void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.inMenu)
        {
            //TODO: colocar la logica del menu
        }
        else if(newGameState == GameState.inFirstEscene)
        {
            //TODO: HAy que preparar la escena del juego
        }
        else if (newGameState == GameState.inGame)
        {
            //TODO: HAy que preparar la escena del juego
        }
        else if (newGameState == GameState.inMiniGame)
        {
            //TODO: HAy que preparar la escena del juego
        }
        else if (newGameState == GameState.inGameOver)
        {
            //TODO: HAy que preparar la escena del juego
        }
        else if (newGameState == GameState.inCredits)
        {
            //TODO: HAy que preparar la escena del juego
        }
        else if (newGameState == GameState.inWin)
        {
            //TODO: HAy que preparar la escena del juego
        }

        this.currentGameState = newGameState;

    }

}
