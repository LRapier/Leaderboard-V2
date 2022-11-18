using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rig;
    private string cardDirec;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if(transform.position.x > 5)
        {
            rig.velocity = new Vector3(-3, 0, 0);
            cardDirec = "E";
        }
        else if (transform.position.x < -5)
        {
            rig.velocity = new Vector3(3, 0, 0);
            cardDirec = "W";
        }
        else if (transform.position.z > 5)
        {
            rig.velocity = new Vector3(0, 0, -3);
            cardDirec = "N";
        }
        else if (transform.position.z < -5)
        {
            rig.velocity = new Vector3(0, 0, 3);
            cardDirec = "S";
        }
    }

    void Update()
    {
        if(Equals(cardDirec, "N"))
        {
            if (transform.position.z < -5)
                Destroy(this.gameObject);
        }
        else if (Equals(cardDirec, "S"))
        {
            if (transform.position.z > 5)
                Destroy(this.gameObject);
        }
        else if (Equals(cardDirec, "E"))
        {
            if (transform.position.x < -5)
                Destroy(this.gameObject);
        }
        else if (Equals(cardDirec, "W"))
        {
            if (transform.position.x > 5)
                Destroy(this.gameObject);
        }
    }
}
