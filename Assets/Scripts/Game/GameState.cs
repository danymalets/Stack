using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class GameState : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private TileSpawner _tileSpawner;
    [SerializeField] private TileCutter _tileCutter;

    [SerializeField] private GameObject _tapToStart;
    [SerializeField] private GameObject _tapToRestart;
    
    [SerializeField] private GameObject _stars;

    public event Action GameInit;
    
    private void OnEnable()
    {
        _tileCutter.GameOver += OnGameOver;
    }
    
    private void OnDisable()
    {
        _tileCutter.GameOver -= OnGameOver;
    }
    
    private void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(WaitForStart());
    }

    private IEnumerator WaitForStart()
    {
        yield return WaitForMouseClick();
        _tapToStart.SetActive(false);
        _tileSpawner.StartGame();
    }

    private void OnGameOver()
    {
        _tapToRestart.SetActive(true);
        _stars.SetActive(false);
        StartCoroutine(WaitForRestart());
    }
    
    private IEnumerator WaitForRestart()
    {
        yield return WaitForMouseClick();

        _tower.Destroy();
        _tapToRestart.SetActive(false);
        _tapToStart.SetActive(true);
        _stars.SetActive(true);
        
        GameInit?.Invoke();
        
        yield return WaitForStart();
    }
    
    private static IEnumerator WaitForMouseClick()
    {
        while (!Input.GetMouseButtonDown(0)) yield return null;
        yield return null;
    }
}
