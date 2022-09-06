using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    private float speed = 10.0f;
    private float speedMultiplier = 1;
    private PlayerControl playerControlScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControlScript = GameObject.Find("Player").GetComponent<PlayerControl>();
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKey(KeyCode.V) && playerControlScript.isOnGround)
        {
            speedMultiplier = 2;
        }
        else
        {
            speedMultiplier = 1;
        }

        if (!playerControlScript.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed * speedMultiplier);
        }
    }
}
