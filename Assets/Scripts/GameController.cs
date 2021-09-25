using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject firstRock;
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
        spawningRockUp = true;
    }

    public void StartGame()
    {
        GameStarted.Value = true;
        StartCoroutine(StartSpawningRocks());
    }

    private IEnumerator StartSpawningRocks()
    {
        while (!GameEnded.Value)
        {
            var newRock = Instantiate(firstRock);
            newRock.AddComponent<Rock>();
            var seconds = Random.Range(1, 2);
            yield return new WaitForSeconds(seconds);
        }
    }
}
