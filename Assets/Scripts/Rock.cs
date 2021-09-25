using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    void Update()
    {
        if (!GameController.Instance.GameEnded.Value)
        {
            transform.Translate(Vector2.left * Time.deltaTime * GameController.ForegroundSpeed);
        }
    }
}
