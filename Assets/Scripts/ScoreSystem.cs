using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{

    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _highScoreText;
    [SerializeField] TMP_Text _multiplierText;
    [SerializeField] FloatingScoreText floatingTextPrefab;
    [SerializeField] Canvas floatingScoreCanvas;
    int _score;
    int _highScore;
    private float _scoreMultiplierExpiration;
    private int _killMultiplier;
    void Start()
    {
        Enemy.Died += Enemy_Died;
        _highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreText.SetText("High Score : " + _highScore);

    }
    void OnDestroy()
    {
        Enemy.Died -= Enemy_Died;
    }
    void Enemy_Died(Enemy enemy)
    {
        UpdateKillMultiplier();

        _score += _killMultiplier;

        if (_score > _highScore)
        {
            _highScore = _score;
            _highScoreText.SetText("High Score : " + _highScore);
            PlayerPrefs.SetInt("HighScore", _highScore);
        }

        _scoreText.SetText("Score: " + _score.ToString());

        var floatingText = Instantiate(floatingTextPrefab,
        enemy.transform.position,
        floatingScoreCanvas.transform.rotation,
        floatingScoreCanvas.transform);

        floatingText.SetScoreValue(_killMultiplier);
    }
    private void UpdateKillMultiplier()
    {
        if (Time.time <= _scoreMultiplierExpiration)
        {
            _killMultiplier++;

            _score += _killMultiplier;
        }
        else
        {
            _killMultiplier = 1;
        }

        _scoreMultiplierExpiration = Time.time + 2f;

        _multiplierText.SetText("x " + _killMultiplier);

        if (_killMultiplier < 3)
        {
            _multiplierText.color = Color.white;
        }
        else if (_killMultiplier < 10)
        {
            _multiplierText.color = Color.green;
        }
        else if (_killMultiplier < 20)
        {
            _multiplierText.color = Color.yellow;
        }
        else if (_killMultiplier < 30)
        {
            _multiplierText.color = Color.red;
        }
        else if (_killMultiplier < 50)
        {
            _multiplierText.color = Color.magenta;
        }
    }
}
