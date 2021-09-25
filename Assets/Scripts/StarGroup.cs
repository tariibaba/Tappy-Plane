using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGroup : MonoBehaviour
{
    public GameObject starPrefab;
    private const int StarsToSpawn = 6;

    private void Update()
    {
        if (!GameController.Instance.GameEnded.Value)
        {
            transform.Translate(Vector2.left * Time.deltaTime * GameController.ForegroundSpeed);
        }
    }

    public void SpawnStars()
    {
        float angularDiff = 180 / (StarsToSpawn - 1);
        for (int i = 0; i < StarsToSpawn; i++)
        {
            var newStar = Instantiate(starPrefab, transform);
            newStar.transform.Translate(Vector2.left * 2.5f);
            newStar.transform.RotateAround(transform.position, Vector3.forward, -i * angularDiff);
        }
    }
}
