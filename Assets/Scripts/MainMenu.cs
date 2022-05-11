using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public static bool isPaused;
    [SerializeField] public Image winScreen;
    [SerializeField] public Image loseScreen;
    public static MainMenu Instance { get; private set; } // static singleton

    void Awake() {
        // Singleton Stuff
        if (Instance == null) { 
            Instance = this;
        }else { 
            Destroy(gameObject);
        }
        isPaused = true;
    }

    public void StartGame() {
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Movement.currFishEaten = 0;
    }

    public void ResetGame() {
        SceneManager.LoadScene(0);
    }

    public void Pause() {
        MainMenu.isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Movement.currFishEaten = 0;
    }
}
