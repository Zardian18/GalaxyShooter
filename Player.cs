using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isPlayer1 = false;
    public bool isPlayer2 = false;


    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject _laserprefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    [SerializeField]
    private float _canFire = 0f;
    [SerializeField]
    private float _canFire2 = 0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    //[SerializeField]
    private int _isTripleShotInt = 0;
    [SerializeField]
    private GameObject _tripleShotprefab;
    //[SerializeField]
    private int _isSpeedUpInt = 0;
    //[SerializeField]
    private int _isSheildsInt = 0;
    [SerializeField]
    private GameObject _shieldVisuals;
    [SerializeField]
    private int _scoreText=0;
    
    private int _scoreModifier;
    private UIManager _uIManager;
    private GameManager _gameManager;
    [SerializeField]
    private GameObject _thruster;
    [SerializeField]
    private GameObject[] _hurt;
    private Difficulty _diff;
    private int x;
    [SerializeField]
    private AudioClip _laserClip;
    private AudioSource _aud;
    private AudioSource _bgm;
    [SerializeField]
    private GameObject _explosionprefab;
    [SerializeField]
    private GameObject _tripleShotIconPrefab;
    [SerializeField]
    private GameObject _shieldIconPrefab;
    [SerializeField]
    private GameObject _speedUpIconPrefab;
    private Animator _turnAnim;
    

    void Start()
    {
        if (PlayerPrefs.GetInt("coop") != 1)
        {
            transform.position = new Vector3(0, -2, 0);
        }
        
        
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("No spawner");
        }
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uIManager == null)
        {
            Debug.LogError("No UI Manager");
        }
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("error");
        }
        _diff = GameObject.Find("Difficulty").GetComponent<Difficulty>();
        if (_diff == null)
        {
            Debug.LogError("no difficulty");
        }
        _aud = GetComponent<AudioSource>();
        if (_aud != null)
        {
            _aud.clip = _laserClip;
        }
        _bgm = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        if(_bgm!= null)
        {
            _bgm.Play();
        }
        _turnAnim = GetComponent<Animator>();
        if (_turnAnim == null)
        {
            Debug.LogError("Animation missing");
        }
        _thruster.SetActive(false);
        _hurt[0].SetActive(false);
        _hurt[1].SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        _gameManager.TIME = Time.timeSinceLevelLoad;
        Movement();
        Shooting();
        _scoreModifier = (int)(3*_gameManager.TIME);
        //Debug.Log(Time.time);
        //_scoreText = (int)scoreModifier;
        
    }

    void Movement()
    {
        

        if (isPlayer1 == true)
        {
            if (_lives > 0)
            {
                if (_isSpeedUpInt == 0)
                {
                    _speed = 6.5f * (1 + (_gameManager.TIME) / 100);
                    if (Input.GetKey(KeyCode.LeftShift)|| CrossPlatformInputManager.GetButtonDown("SpeedUpButton"))
                    {
                        _thruster.SetActive(true);
                        _speed = 8.5f * (1 + (_gameManager.TIME) / 100);
                    }
                    else if (Input.GetKeyUp(KeyCode.LeftShift) || CrossPlatformInputManager.GetButtonUp("SpeedUpButton"))
                    {
                        _thruster.SetActive(false);
                        _speed = 6.5f * (1 + (_gameManager.TIME) / 100);
                    }
                }
                else
                {
                    _thruster.SetActive(true);
                    _speed = 6.5f * (1 + (_gameManager.TIME) / 100) + 5;
                    if (Input.GetKey(KeyCode.LeftShift) || CrossPlatformInputManager.GetButtonDown("SpeedUpButton"))
                    {

                        _speed = 8.5f * (1 + (_gameManager.TIME) / 100) + 5;
                    }
                    else if (Input.GetKeyUp(KeyCode.LeftShift) || CrossPlatformInputManager.GetButtonUp("SpeedUpButton"))
                    {
                        _speed = 6.5f * (1 + (_gameManager.TIME) / 100) + 5;
                    }
                }
            }
            else if (_lives < 1)
            {
                _speed = 0;
            }
            float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");   //Input.GetAxis("Horizontal");
            float verticalInput = CrossPlatformInputManager.GetAxis("Vertical");    //Input.GetAxis("Vertical");
            if (horizontalInput <-0.7f)
            {
                _turnAnim.Play("leftTurn_anim");
            }
            else if (horizontalInput >0.7f)
            {
                _turnAnim.Play("RighTurn_anim");
            }
            transform.Translate(new Vector3(1 * horizontalInput * _speed * Time.deltaTime, 0, 0));
            transform.Translate(new Vector3(0, 1 * verticalInput * _speed * Time.deltaTime, 0));
        }
        else
        {
            if (_lives > 0)
            {
                if (_isSpeedUpInt == 0)
                {
                    _speed = 6.5f * (1 + (_gameManager.TIME) / 100);
                    if (Input.GetKey(KeyCode.Keypad0))
                    {
                        _thruster.SetActive(true);
                        _speed = 8.5f * (1 + (_gameManager.TIME) / 100);
                    }
                    else if (Input.GetKeyUp(KeyCode.Keypad0))
                    {
                        _thruster.SetActive(false);
                        _speed = 6.5f * (1 + (_gameManager.TIME) / 100);
                    }
                }
                else
                {
                    _thruster.SetActive(true);
                    _speed = 6.5f * (1 + (_gameManager.TIME) / 100) + 5;
                    if (Input.GetKey(KeyCode.Keypad0))
                    {

                        _speed = 8.5f * (1 + (_gameManager.TIME) / 100) + 5;
                    }
                    else if (Input.GetKeyUp(KeyCode.Keypad0))
                    {
                        _speed = 6.5f * (1 + (_gameManager.TIME) / 100) + 5;
                    }
                }
            }
            else if (_lives < 1)
            {
                _speed = 0;
            }
            float horizontalInput2 = CrossPlatformInputManager.GetAxis("Horizontal2");   //Input.GetAxis("Horizontal");
            float verticalInput2 = CrossPlatformInputManager.GetAxis("Vertical2");    //Input.GetAxis("Vertical");

            if (horizontalInput2 <-0.7f)
            {
                _turnAnim.Play("leftTurn_anim");
            }
            else if (horizontalInput2 >0.7f)
            {
                _turnAnim.Play("RighTurn_anim");
            }

            transform.Translate(new Vector3(1 * horizontalInput2 * _speed * Time.deltaTime, 0, 0));
            transform.Translate(new Vector3(0, 1 * verticalInput2 * _speed * Time.deltaTime, 0));
        }
        
        // or
        // Vector3 direction= new Vector3(horizontalInput, verticalInput, 0);
        //transform.Translate( direction*_speed* Time.deltaTime);

        //if y>=0, then y=1
        // if |x|>=12, x =12........or if x>=13----> -13 and vv
        if (transform.position.y >= 1)
        {
            transform.position = new Vector3(transform.position.x, 1, 0);

        }
        else if (transform.position.y <= -4)
        {
            transform.position = new Vector3(transform.position.x, -4, 0);
        }
       
        // for x
        if (transform.position.x >= 10)
        {
            transform.position = new Vector3(10, transform.position.y, 0);
        }

        else if (transform.position.x <= -10)
        {
            transform.position = new Vector3(-10, transform.position.y, 0);
        }

        //or
        //transform.position= new Vector3(Mathf.Clamp(transform.position.x,-12,12), Mathf.Clamp(transform.position.y,-4,1),0);
    }

    void Shooting()
    {
        float y = transform.position.y;
        if (isPlayer1 == true)
        {
            PlayerPrefs.SetInt("p", 1);
            
            if ((Input.GetKeyDown(KeyCode.Space)|| CrossPlatformInputManager.GetButtonDown("Fire")) && Time.time > _canFire)
            // instantiate laser from 0.8 units above the ship
            {
                _canFire = Time.time + _fireRate;
                float xt, yt;
                xt = transform.position.x;
                yt = transform.position.y;
                if (_isTripleShotInt > 0)
                {
                    Instantiate(_tripleShotprefab, new Vector3(xt, yt + 0.15f, 0), Quaternion.identity);

                }

                else
                {
                    Instantiate(_laserprefab, new Vector3(transform.position.x, y + 0.8f, 0), Quaternion.identity);
                }
                _aud.Play();

            }
           // PlayerPrefs.SetInt("p", 0);
            
        }
        else
        {
            PlayerPrefs.SetInt("p", 2);

            if (Input.GetKeyDown(KeyCode.RightControl) && Time.time > _canFire2)
            // instantiate laser from 0.8 units above the ship
            {
                _canFire2 = Time.time + _fireRate;
                float xt, yt;
                xt = transform.position.x;
                yt = transform.position.y;
                if (_isTripleShotInt > 0)
                {
                    Instantiate(_tripleShotprefab, new Vector3(xt, yt + 0.15f, 0), Quaternion.identity);

                }

                else
                {
                    Instantiate(_laserprefab, new Vector3(transform.position.x, y + 0.8f, 0), Quaternion.identity);
                }
                _aud.Play();
                //PlayerPrefs.SetInt("p", 0);
            }
        }

    }

    public void Damage()
    {
        if (_isSheildsInt >0)
        {
            
            return;
        }
        _speed -= _diff.DiffChange() + 1f;
        _lives--;
        int r = Random.Range(0, 2);
        
        if (_lives == 2)
        {
            _hurt[r].SetActive(true);
            
            x = r;
        }
        else if (_lives == 1)
        {
            
            if (x == 0)
            {
                _hurt[1].SetActive(true);
                x = 1;
            }
            else if (x == 1)
            {
                _hurt[0].SetActive(true);
                x = 0;
            }
        }
        

        
        if (_uIManager != null)
        {
            if (isPlayer1 == true)
            {
                _uIManager.UpdateLives(_lives);
            }
            else
            {
                _uIManager.UpdateLives2(_lives);
            }
        }
        if (_lives < 1)
        {
            if (PlayerPrefs.GetInt("coop") == 0 )
            {
                _bgm.Stop();
                _uIManager.GameOver();
                HighScoreCheck();
            }
            else if (PlayerPrefs.GetInt("coop") == 1)
            {
                if (isPlayer1 == true)
                {

                    _uIManager.deaths++;
                    //Debug.Log("Deaths: " + _uIManager.deaths);
                    
                }
                else if (isPlayer2 == true)
                {
                    _uIManager.deaths++;
                    //Debug.Log("Deaths: " + _uIManager.deaths);
                    
                }
            }
            if (_uIManager.deaths == 2)
            {
                _bgm.Stop();
                _uIManager.GameOver();
            }

            //_spawnManager.Death();

            //transform.parent.position = new Vector3(transform.position.x, transform.position.y, 0);
            Instantiate(_explosionprefab, transform.parent, true);
            
            
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        
        _isTripleShotInt++;
        //StartCoroutine(SetTripleShot());
        if (isPlayer1 == true)
        {
            GameObject _tsi = Instantiate(_tripleShotIconPrefab, new Vector3(-8.6f, 4f, 0f), Quaternion.identity);
            Destroy(_tsi, 7f);
        }
        else
        {
            GameObject _tsi = Instantiate(_tripleShotIconPrefab, new Vector3(8.6f, 4f, 0f), Quaternion.identity);
            Destroy(_tsi, 7f);
        }

        
        //_uIManager.TripleShotIcon();

        StartCoroutine(Trip(7));
    }
    
    IEnumerator Trip(float timer)
    {
        do
        {
            yield return new WaitForSeconds(timer);
            _isTripleShotInt--;
        }

        while (_isTripleShotInt > 1);
        if (_isTripleShotInt < 0)
        {
            _isTripleShotInt = 0;
        }
    }

    public void SpeedUpActive()
    {
        _isSpeedUpInt++;
        
        if (isPlayer2 == true)
        {
            GameObject _sui = Instantiate(_speedUpIconPrefab, new Vector3(6.8f, 4f, 0f), Quaternion.identity);
            Destroy(_sui, 5f);
        }
        else
        {
            GameObject _sui = Instantiate(_speedUpIconPrefab, new Vector3(-6.8f, 4f, 0f), Quaternion.identity);
            Destroy(_sui, 5f);
        }

        

        //_uIManager.SpeedUpIcon();
        StartCoroutine(SetSpeedUp(5));
    }
    IEnumerator SetSpeedUp(float timer)
    {
        do
        {
            yield return new WaitForSeconds(timer);
            _isSpeedUpInt--;
        }

        while (_isSpeedUpInt > 1);
        if (_isSpeedUpInt < 0)
        {
            _isSpeedUpInt = 0;
        }
        else if (_isSpeedUpInt == 0)
        {
            _thruster.SetActive(false);
        }

        


    }
    public void ShieldActive()
    {
        _isSheildsInt++;
        _shieldVisuals.SetActive(true);
        
        if (isPlayer2 == true)
        {
            GameObject _si = Instantiate(_shieldIconPrefab, new Vector3(7.7f, 4f, 0f), Quaternion.identity);
            Destroy(_si, 4f);
        }
        else
        {
            GameObject _si = Instantiate(_shieldIconPrefab, new Vector3(-7.7f, 4f, 0f), Quaternion.identity);
            Destroy(_si, 4f);
        }

        
        //_uIManager.ShieldIcon();
        StartCoroutine(SetShield(4));
    }
    IEnumerator SetShield(float timer)
    {
        
        //transform.GetChild(0).transform.gameObject.SetActive(true);
        
        do
        {
            yield return new WaitForSeconds(timer);
            _isSheildsInt--;
        }
        while (_isSheildsInt > 1);
        if (_isSheildsInt == 0 )
        {
            _shieldVisuals.SetActive(false);

        }
        else if (_isSheildsInt < 0)
        {
            _isSheildsInt = 0;
        }
    }
    public void HealthPowerup()
    {
        if (_lives < 3)
        {
            _lives++;
            _uIManager.UpdateLives(_lives);
            _speed += _diff.DiffChange() - 1;
            if (x == 0)
            {
                _hurt[0].SetActive(false);
                x = 1;
                
            }
            else if (x == 1)
            {
                _hurt[1].SetActive(false);
                x = 0;
            }
            
        }
    }
    public int ScoreBoard(int add)
    {
        _scoreText += add;
        return _scoreText;
    }
    
    public int DisplayScore()
    {
        //Debug.Log(_scoreText);
        
        return _scoreText+ _scoreModifier;
    }

    public void HighScoreCheck()
    {
        if (_scoreText + _scoreModifier > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", _scoreText + _scoreModifier);
            _uIManager.HighScore();
        }
    }
    
}

