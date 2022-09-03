using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    public float speed = 10f;
    private PlayerControl playerControlScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControlScript = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControlScript.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
}
