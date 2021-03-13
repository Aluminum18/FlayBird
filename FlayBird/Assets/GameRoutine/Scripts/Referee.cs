using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField]
    private IntegerVariable _currentScore;
    [SerializeField]
    private IntegerVariable _scorePerObstacle;
    [SerializeField]
    private IntegerVariable _speedUpAfterScores;
    [SerializeField]
    private FloatVariable _accelaration;
    [SerializeField]
    private FloatVariable _obstacleSpeed;

    [Header("Events in")]
    [SerializeField]
    private GameEvent _onBirdHit;
    [SerializeField]
    private GameEvent _onBirdScored;

    [Header("Events out")]
    [SerializeField]
    private GameEvent _onPreGame;
    [SerializeField]
    private GameEvent _onGameStart;
    [SerializeField]
    private GameEvent _onGameEnd;
    [SerializeField]
    private GameEvent _onRestartGame;

    public void PreGame()
    {
        _currentScore.Value = 0;
        _obstacleSpeed.ResetToDefault();

        _onPreGame.Raise();
    }

    public void StartGame()
    {
        _onGameStart.Raise();
    }

    public void RestartGame()
    {
        _onRestartGame.Raise();
        StartCoroutine(IE_FromRestartToPreGame());
    }

    private IEnumerator IE_FromRestartToPreGame()
    {
        yield return null;
        yield return null; // 2 frames for cleaning up

        PreGame();
    }

    private void EndAGame(params object[] args)
    {
        _onGameEnd.Raise();
    }

    private void Score(params object[] args)
    {
        _currentScore.Value += _scorePerObstacle.Value;

        if (_currentScore.Value < _speedUpAfterScores.Value)
        {
            return;
        }

        if (_currentScore.Value % _speedUpAfterScores.Value == 0)
        {
            _obstacleSpeed.Value += _accelaration.Value;
        }
    }

    private void Awake()
    {
        _onBirdHit.Subcribe(EndAGame);
        _onBirdScored.Subcribe(Score);
    }

    private void Start()
    {
        PreGame();
    }

    private void OnDestroy()
    {
        _onBirdHit.Unsubcribe(EndAGame);
        _onBirdScored.Unsubcribe(Score);
    }
}
