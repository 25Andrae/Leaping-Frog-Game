using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public float depth = 1;
    PlayerScript player;
   


    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
     
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float realVelocity = player.velocity.x / depth;
        Vector2 pos = transform.position;

        pos.x -= realVelocity * Time.fixedDeltaTime;

        if (pos.x <= -181 )
            pos.x = -7;
        transform.position = pos;
    }
}
