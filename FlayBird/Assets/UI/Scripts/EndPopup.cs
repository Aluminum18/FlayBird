using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndPopup : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField]
    private IntegerVariable _currentScore;
    [SerializeField]
    private IntegerVariable _bronze;
    [SerializeField]
    private IntegerVariable _silver;
    [SerializeField]
    private IntegerVariable _gold;

    [Header("Events in")]
    [SerializeField]
    private GameEvent _onEndGame;

    [Header("Config")]
    [SerializeField]
    private Image _medal;
    [SerializeField]
    private TMP_Text _bestScoreText;
    [SerializeField]
    private List<Sprite> _medals;

    private UIPanel _panel;

    private void UpdateResult(params object[] args)
    {
        UpdateMedal();
        UpdateBestScore();

        _panel.Open();
    }

    private void UpdateMedal()
    {
        if (_currentScore.Value >= _gold.Value)
        {
            _medal.sprite = _medals[0];
            return;
        }

        if (_currentScore.Value >= _silver.Value)
        {
            _medal.sprite = _medals[1];
            return;
        }

        _medal.sprite = _medals[2];

    }

    private void UpdateBestScore()
    {
        int currentBest = PlayerPrefs.GetInt("BestScore", 0);

        if (_currentScore.Value > currentBest)
        {
            _bestScoreText.text = _currentScore.Value.ToString();
            PlayerPrefs.SetInt("BestScore", _currentScore.Value);
            return;
        }
        _bestScoreText.text = currentBest.ToString();
    }

    private void Awake()
    {
        _panel = GetComponent<UIPanel>();
    }

    private void OnEnable()
    {
        _onEndGame.Subcribe(UpdateResult);
    }

    private void OnDisable()
    {
        _onEndGame.Unsubcribe(UpdateResult);
    }
}
