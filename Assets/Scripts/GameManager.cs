using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState State { get; private set; } = GameState.Waiting;

    [SerializeField] private BoardManager boardManager;

    [SerializeField] private ActionBarManager actionBarManager;

    [SerializeField] private AudioManager audioManager;

    [Space] [SerializeField] private GameObject winScreen;
    [SerializeField] private ParticleSystem winParticles;
    [SerializeField] private GameObject defeatScreen;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        State = GameState.Playing;
        boardManager.StartLevel();
    }

    public void WinGame()
    {
        State = GameState.Win;
        ShowEndgameScreen(true);
    }

    public void LoseGame()
    {
        State = GameState.Lose;
        ShowEndgameScreen(false);
    }

    private void ShowEndgameScreen(bool isWin)
    {
        if (isWin)
        {
            winScreen.SetActive(true);
            winParticles.Play();
        }
        else
        {
            defeatScreen.SetActive(true);
        }
    }

    public void RestartGame()
    {
        boardManager.ResetBoard();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public ActionBarManager GetActionBarManager()
    {
        return actionBarManager;
    }
    
    public AudioManager GetAudioManager()
    {
        return audioManager;
    }
}