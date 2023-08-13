using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour

{

    [SerializeField]
    private float _lspeed = 12.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Des();
    }
    void Movement()
    {
        //transform.Translate(Vector3.up * _lspeed*Time.deltaTime);
        transform.Translate(new Vector3(0,_lspeed * Time.deltaTime, 0));

    }
    void Des()
    {
        if (transform.position.y >= 8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }


    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("bullet"))
        {
            Destroy(gameObject);
        }
    }
    
}
