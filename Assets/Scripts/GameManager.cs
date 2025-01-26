using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance { get; private set; }
    public List<GameObject> puzzles;
    private int currentPuzzleIndex = 0;
    public Camera mainCamera;
    public Camera puzzleCamera;
    


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            puzzleCamera.gameObject.SetActive(false);
            DeactivateAllPuzzles();
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
            mainCamera.gameObject.SetActive(false);
            puzzleCamera.gameObject.SetActive(true);
            puzzles[currentPuzzleIndex].SetActive(true);
            EnableCursor();
        }
    }

    public void ReturnToMain()
    {
        DeactivateAllPuzzles();
        puzzleCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
    }

    private void DeactivateAllPuzzles()
    {
        foreach (var puzzle in puzzles)
        {
            puzzle.SetActive(false);
        }
    }

    private void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnPuzzleComplete()
    {
        puzzles[currentPuzzleIndex].SetActive(false);
        currentPuzzleIndex++;
        ReturnToMain();
    }
}