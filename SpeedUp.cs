using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class SpeedUp : MonoBehaviour

{
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is dead");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpeedButton()
    {
        if (CrossPlatformInputManager.GetButtonDown("SpeedUpButton"))
        {

        }
    }
}
