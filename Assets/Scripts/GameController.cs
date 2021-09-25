using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject rockUp;
    public GameObject rockDown;
    private bool spawningRockUp;
    private Vector2 rockSpawnStartPointPos;
    public ReactiveProperty<bool> GameEnded;
    public ReactiveProperty<bool> GameStarted;
    public static GameController Instance { get; private set; }
    public ReactiveProperty<int> Score;

    private void Awake()
    {
        Instance = this;
        GameEnded = new ReactiveProperty<bool>(false);
        GameStarted = new ReactiveProperty<bool>(false);
        Score = new ReactiveProperty<int>(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        var screenRightX = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.width)).x;
        spawningRockUp = true;
    }

    public void StartGame()
    {
        GameStarted.Value = true;
        StartCoroutine(StartSpawningRocks());
    }

    private IEnumerator StartSpawningRocks()
    {
        while (!GameController.Instance.GameEnded.Value)
        {
            var rockToSpawn = spawningRockUp ? rockUp : rockDown;
            spawningRockUp = !spawningRockUp;
            var newRock = Instantiate(rockToSpawn);
            newRock.AddComponent<Rock>();
            var seconds = Random.Range(0, 2f);
            yield return new WaitForSeconds(seconds);
        }
    }
}
