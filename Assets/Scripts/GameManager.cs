using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance { get; private set; }
    public List<GameObject> puzzles;
    private int currentPuzzleIndex = 0;
    public string mainSceneName = "MainScene";
    public string puzzleSceneName = "PuzzleScene";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartPuzzle()
    {
        if (currentPuzzleIndex < puzzles.Count)
        {
            SceneManager.LoadScene(puzzleSceneName);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == puzzleSceneName)
        {
            DeactivateAllPuzzles();
            LoadCurrentPuzzle();
        }
    }

    private void DeactivateAllPuzzles()
    {
        foreach (var puzzle in puzzles)
        {
            puzzle.SetActive(false);
        }
    }

    private void LoadCurrentPuzzle()
    {
        if (currentPuzzleIndex < puzzles.Count)
        {
            puzzles[currentPuzzleIndex].SetActive(true);
        }
    }

    public void OnPuzzleComplete()
    {
        puzzles[currentPuzzleIndex].SetActive(false);
        currentPuzzleIndex++;
        SceneManager.LoadScene(mainSceneName);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}