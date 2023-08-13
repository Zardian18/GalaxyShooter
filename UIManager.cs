using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;


public class UIManager : MonoBehaviour
{
    public int deaths=0;

    [SerializeField]
    private Text _score1;
    [SerializeField]
    private Text _highScore;
    [SerializeField]
    private Text _score2;
    private Player _player1;
    private Player _player2;
    [SerializeField]
    private Sprite[] _lives;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Image _livesImg2;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _restart;
    [SerializeField]
    private GameObject _pauseMenu;
    private bool _isGamePaused = false;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private SpawnManager _spawnManager;
    private AudioSource _bgm;
    private Animator _pauseMenuAnim;
    
    
    //private bool _isDead = false;


    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("no spawn Manager");
        }
        if (PlayerPrefs.GetInt("coop") == 1)
        {
            _player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
            if (_player1 == null)
            {
                //error
            }
            _player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();
            if (_player2 == null)
            {
                Debug.LogError("player dead");
            }
            _score1.text = "Score: " + _player1.DisplayScore();
            _score2.text = "Score: " + _player2.DisplayScore();
        }
        else
        {
            _player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
            if (_player1 == null)
            {
                //error
            }
            _score2.text = "Score: " + _player1.DisplayScore();
        }
        
        _gameOver.gameObject.SetActive(false);
        _restart.gameObject.SetActive(false);
        _pauseMenu.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("error");
        }
        _bgm = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        if (_bgm == null)
        {
            Debug.LogError("No background music");
        }
        _pauseMenuAnim = GetComponent<Animator>();
        if (_pauseMenuAnim == null)
        {
            Debug.LogError("pause menu animation missing");
        }
        _pauseMenuAnim.updateMode = AnimatorUpdateMode.UnscaledTime;
        if (PlayerPrefs.GetInt("coop") == 0)
        {
            _highScore.text = "Best: " + PlayerPrefs.GetInt("highscore");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("coop") == 1)
        {
            _score1.text = "Score:" + _player1.DisplayScore();
            _score2.text = "Score:" + _player2.DisplayScore();
        }
        else
        {
            _score1.text = "Score:" + _player1.DisplayScore();
        }
        Pause();
        
    }

    public void UpdateLives(int currentLives)
    {
        if(currentLives < 0)
        {
            //Debug.Log("Player dead");
        }
        else
        {
            _livesImg.sprite = _lives[currentLives];
        }
        
    }
    public void UpdateLives2(int currentLives)
    {
        if (currentLives < 0)
        {
           // Debug.Log("Player dead");
        }
        else
        {
            _livesImg2.sprite = _lives[currentLives];
        }

    }
    public void Pause()
    {
        if(_gameManager.GameOverValue()==false && (Input.GetKeyDown(KeyCode.Escape)||CrossPlatformInputManager.GetButtonDown("PauseButton")))
        {
            _pauseMenu.gameObject.gameObject.SetActive(!_pauseMenu.gameObject.activeSelf);
            if (_isGamePaused == false)
            {
                _pauseMenuAnim.SetBool("IsPaused",true);
                _isGamePaused = true;
                Time.timeScale = 0;
                _bgm.Pause();
            }
            else if (_isGamePaused)
            {
                _pauseMenuAnim.SetBool("IsPaused", false);
                
                _isGamePaused = false;
                Time.timeScale = 1;
                _bgm.Play();
            }
            
        }
    }
    
    public void GameOver()
    {
        _spawnManager.Death();
        _gameOver.gameObject.SetActive(true);

        _gameManager.GameOverChange();
        _restart.gameObject.SetActive(true);
        StartCoroutine(Flicker());

    }

    IEnumerator Flicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            _gameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            _gameOver.gameObject.SetActive(true);
        }
        
        
    }

    public void Resume()
    {
        _pauseMenuAnim.SetBool("IsPaused", false);
        Time.timeScale = 1f;
        _isGamePaused = false;
        _pauseMenu.SetActive(false);
        _bgm.Play();
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void RestartCoop()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void HighScore()
    {
        _highScore.text = "Best: " + PlayerPrefs.GetInt("highscore",0);
        
    }
}
    
