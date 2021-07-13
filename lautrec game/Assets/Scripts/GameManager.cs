using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    

    // Start is called before the first frame update
     [Header("Managers")]
    public GUIManager guiManager;
    public AudioManager audioManager;

    [Space]
    public bool pause;
    [Space]
    
    [Header("Game")]
    public Texture2D dialogMouse;
    public GameObject Player;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Time.timeScale = 1;
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    

    // Start is called before the first frame update
    void Start()
    {
       SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        guiManager.Pause();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
