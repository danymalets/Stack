using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private GameState _gameState;
    [SerializeField] private Tower _tower;
    [SerializeField] private TileCutter _tileCutter;

    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _bestScore;

    private void Awake()
    {
        _bestScore.text = PlayerPrefs.GetInt(Prefs.BestScore).ToString();
    }
    private void OnEnable()
    {
        _gameState.GameInit += OnGameInit;
        _tower.TileInstalled += OnTileInstalled;
        _tileCutter.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _gameState.GameInit -= OnGameInit;
        _tower.TileInstalled -= OnTileInstalled;
        _tileCutter.GameOver -= OnGameOver;
    }

    private void OnGameInit()
    {
        _score.text = "0";
    }

    private void OnTileInstalled(Transform tile)
    {
        _score.text = _tower.Score.ToString();
    }

    private void OnGameOver()
    {
        if (_tower.Score > PlayerPrefs.GetInt(Prefs.BestScore))
        {
            _bestScore.text = _tower.Score.ToString();
            PlayerPrefs.SetInt(Prefs.BestScore, _tower.Score);
        }
    }
}