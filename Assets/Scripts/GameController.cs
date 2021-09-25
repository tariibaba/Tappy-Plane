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
    public ReactiveProperty<int> Stars;
    private int rocksTillNextStarSpawn;
    public StarGroup starGroupPrefab;
    public const float ForegroundSpeed = 5f;

    private void Awake()
    {
        Instance = this;
        GameEnded = new ReactiveProperty<bool>(false);
        GameStarted = new ReactiveProperty<bool>(false);
        Score = new ReactiveProperty<int>(0);
        Stars = new ReactiveProperty<int>(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        spawningRockUp = true;
    }

    public void StartGame()
    {
        GameStarted.Value = true;
        rocksTillNextStarSpawn = Random.Range(5, 10);
        StartCoroutine(StartSpawningRocks());
    }

    private IEnumerator StartSpawningRocks()
    {
        while (!GameEnded.Value)
        {
            var newRock = Instantiate(firstRock);
            newRock.AddComponent<Rock>();
            newRock.transform.Translate(new Vector3(0, Random.Range(-1.5f, 1.5f)));
            var seconds = Random.Range(1f, 2f);
            yield return new WaitForSeconds(seconds);

            if (--rocksTillNextStarSpawn == 0)
            {
                rocksTillNextStarSpawn = Random.Range(5, 10);
                SpawnStars();
            }
        }
    }

    private void SpawnStars()
    {
        var starGroup = Instantiate(starGroupPrefab);
        starGroup.SpawnStars();
    }
}
