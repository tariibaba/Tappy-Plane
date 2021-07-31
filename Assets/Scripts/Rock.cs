using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 5;

    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.GameEnded.Value)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
    }
}
