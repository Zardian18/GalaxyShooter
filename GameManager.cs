using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour
{
    public float TIME;
    
    private bool _isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("coop")==0)
        {

            if (_isGameOver == true && (Input.GetKey(KeyCode.R)||CrossPlatformInputManager.GetButtonDown("Restart")))
            {
                SceneManager.LoadScene(1);
                _isGameOver = false;
                TIME = 0;
            }
            else if (_isGameOver == true && Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
                _isGameOver = false;
                TIME = 0;
            }
        }
        else
        {
            if (_isGameOver == true && Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(3);
                _isGameOver = false;
                TIME = 0;
            }
            else if (_isGameOver == true && Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
                _isGameOver = false;
                TIME = 0;
            }
        }
        
    }
    public void GameOverChange()
    {
        _isGameOver = true;
    }
    public bool GameOverValue()
    {
        return _isGameOver;
    }
}
