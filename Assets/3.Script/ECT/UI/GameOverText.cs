using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldTime;

public class GameOverText : MonoBehaviour
{
    [SerializeField] WorldTimeScript worldTimeScript;
    [SerializeField] private Text gameOverText;
    private TimeSpan _currentTime;
    private float dayTime;
    private float timeText = 1;

    private void Update()
    {
        SurTimeText();
    }

    private void SurTimeText()
    {
        gameOverText.text = $"����� {timeText}�� ���� �����Ͽ����ϴ�.";
    }
}
