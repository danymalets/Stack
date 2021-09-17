using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FpsController : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private float _elapsedTime = 0f;
    private int _frameCount = 0;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > 1f)
        {
            _text.text = $"fps: {_frameCount}";
            _elapsedTime = 0f;
            _frameCount = 1;
        }
        else
        {
            _frameCount++;
        }
    }
}
