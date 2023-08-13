using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed=3f;
    [SerializeField]
    private int id;
    [SerializeField]
    private AudioClip _collect;
    

    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -4)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                switch (id)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedUpActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    case 3:
                        player.HealthPowerup();
                        break;
                    default:
                        break;
                }
                AudioSource.PlayClipAtPoint(_collect, transform.position);
                
                Destroy(this.gameObject);
            }
        }

        else if (other.CompareTag("Player2"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                switch (id)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedUpActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    case 3:
                        player.HealthPowerup();
                        break;
                    default:
                        break;
                }
                AudioSource.PlayClipAtPoint(_collect, transform.position);

                Destroy(this.gameObject);
            }
        }

    }
    
}
