using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyprefab;
    [SerializeField]
    private GameObject _enemy2prefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerup;
    [SerializeField]
    private GameObject _powerupContainer;
    private GameManager _gameManager;
    private Difficulty _diff;
    
    private bool _isAlive = true;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitBeforeSpawining(5f));
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("no game manager");
        }
        _diff = GameObject.Find("Difficulty").GetComponent<Difficulty>();
        if (_diff == null)
        {
            Debug.LogError("No difficulty settings");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_diff.DiffChange());

    }
    IEnumerator WaitBeforeSpawining(float t)
    {
        
        yield return new WaitForSeconds(t);
        StartCoroutine(Spawn1());
        StartCoroutine(Spawn2());
        StartCoroutine(PowerUp());
    }

    IEnumerator Spawn1()
    {
        while (_isAlive)
        {
            //float var = 50 / (Time.time + 1);
            float timer1 = Random.Range(1.2f, 12f);
            float x=Random.Range(-8f, 8f);
            float y = Random.Range(6f, 5f);
            GameObject newEnemy= Instantiate(_enemyprefab, new Vector3(x, y, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(timer1);

        }
    }

    IEnumerator Spawn2()
    {
        
        while (_isAlive)
        {
            float var=1/(_gameManager.TIME + 50);
            float timer2 = 200f * var;//Random.Range(5f, 10f);
            float x = Random.Range(-8f, 8f);
            float y = Random.Range(6f, 5f);
            GameObject newEnemy=  Instantiate(_enemy2prefab, new Vector3(x, y, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(timer2);


        }
       
    }
    IEnumerator PowerUp()
    {

        while (_isAlive)
        {
            float timerpt=Random.Range(0f,4f);
            float x = Random.Range(-8f, 8f);
            int r = Random.Range(0, 4);
            GameObject powerup1 = Instantiate(powerup[r], new Vector3(x, 7, 0), Quaternion.identity);
            powerup1.transform.parent = _powerupContainer.transform;
            yield return new WaitForSeconds(_diff.DiffChange()+timerpt*_diff.DiffChange()/1.5f);
        }
    }
    

    public void Death()
    {

        _isAlive = false;
        
    }
    public bool IsAliveCheck()
    {

        return _isAlive;
    }
}
