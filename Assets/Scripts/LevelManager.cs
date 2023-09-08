using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    float sceneLoadDelay = 2f;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        scoreKeeper.ResetScore();
        SceneManager.LoadScene(1);

    }
    
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad(2, sceneLoadDelay));
    }

    public void QuitGame()
    {
        Debug.Log("I win bye bye");
        Application.Quit();
    }
    
    IEnumerator WaitAndLoad(int sceneIndex, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(sceneIndex);
    }
}
