using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField]
    private FloatVariable _currentObstacleSpeed;
    [SerializeField]
    private Camera _mainCam;

    [Header("Events in")]
    [SerializeField]
    private GameEvent _onStartGame;
    [SerializeField]
    private GameEvent _onEndGame;

    [Header("Config")]
    [SerializeField]
    private float _initialSpawnDelay;
    [SerializeField]
    private float _verticalSpace;
    [SerializeField]
    private float _horizonSpace;
    [SerializeField]
    private ObjectPool _pool;

    private bool _spawnFlag = false;
    private float _spawnInterval;
    private float _waitForNextObstacle;

    private Vector3 _obstaclePos = Vector3.zero;
    private float _totalVerticalSpace;

    private void StartSpawningObstacles(object[] args)
    {
        _spawnFlag = true;
        StartCoroutine(IE_SpawnObstacles());
    }

    private IEnumerator IE_SpawnObstacles()
    {
        _waitForNextObstacle = _initialSpawnDelay;
        while (_spawnFlag)
        {
            if (_waitForNextObstacle <= 0f)
            {
                SpawnAnObstaclePair();
                _waitForNextObstacle = _spawnInterval;
            }
            yield return null;
            _waitForNextObstacle -= Time.deltaTime;
        }
    }

    private void SpawnAnObstaclePair()
    {      
        var obstacleTop = _pool.GetObject();
        var obstacleBot = _pool.GetObject();

        _obstaclePos.x = obstacleTop.transform.position.x;
        _obstaclePos.y = Random.Range(_verticalSpace, _totalVerticalSpace);

        obstacleTop.transform.position = _obstaclePos;
        obstacleBot.transform.position = _obstaclePos + Vector3.down * (_verticalSpace + _totalVerticalSpace);

        var topObstacleMoving = obstacleTop.GetComponent<ObstacleMoving>();
        topObstacleMoving.Moving = true;

        var botObstacleMoving = obstacleBot.GetComponent<ObstacleMoving>();
        botObstacleMoving.Moving = true;
    }

    private void StopSpawningObstacles(object[] args)
    {
        _spawnFlag = false;
    }

    private void UpdateSpawnIntervalAndWaitTime(float newObstacleSpeed)
    {
        float prevObstacleSpeed = newObstacleSpeed - _currentObstacleSpeed.LastChange;
        _waitForNextObstacle *= prevObstacleSpeed / newObstacleSpeed;

        _spawnInterval = _horizonSpace / newObstacleSpeed;
    }

    private void RegisterEvents()
    {
        _onStartGame.Subcribe(StartSpawningObstacles);
        _onEndGame.Subcribe(StopSpawningObstacles);

        _currentObstacleSpeed.OnValueChange += UpdateSpawnIntervalAndWaitTime;
    }

    private void UnregisterEvents()
    {
        _onStartGame.Unsubcribe(StartSpawningObstacles);
        _onEndGame.Unsubcribe(StopSpawningObstacles);

        _currentObstacleSpeed.OnValueChange -= UpdateSpawnIntervalAndWaitTime;
    }

    private void Awake()
    {
        RegisterEvents();

        _totalVerticalSpace = _mainCam.orthographicSize * 2f;

        _spawnInterval = _horizonSpace / _currentObstacleSpeed.Value;
    }

    private void OnDestroy()
    {
        UnregisterEvents();
    }
}
