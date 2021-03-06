﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaneLoseWindow : MonoBehaviour {
    [SerializeField] private Button tryAgainButton = null;
    [SerializeField] private Button mainMenuButton = null;
    [SerializeField] private TextMeshProUGUI yourTimeText = null;
    [SerializeField] private TextMeshProUGUI bestTimeText = null;
    [SerializeField] private CanvasGroup _canvasGroup = null;
    [SerializeField] private AudioSource mainSource = null;
    [SerializeField] private AudioClip winSong = null;
    [SerializeField] private AudioClip loseSong = null;
    public static PlaneLoseWindow Instance = null;

    private bool _isNewRecord = false;

    private void Awake() {
        Instance = this;
        tryAgainButton.onClick.AddListener(() => SceneManager.LoadScene("Plane"));
        mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        
        gameObject.SetActive(false);
    }

    private TimeSpan GetRecordTime() {
        return new TimeSpan(long.Parse(PlayerPrefs.GetString("PlaneRecord", "-1")));
    }
    
    private void SetRecordTime(TimeSpan timeSpan) {
        PlayerPrefs.SetString("PlaneRecord", timeSpan.Ticks.ToString());
        PlayerPrefs.Save();
    }


    public void OnGameEndHandler(TimeSpan timeSpan) {
        _isNewRecord = (timeSpan.Minutes * 60 + timeSpan.Seconds) > (GetRecordTime().Minutes * 60 + GetRecordTime().Seconds);
        if(_isNewRecord) SetRecordTime(timeSpan);
        mainSource.Stop();
        mainSource.PlayOneShot(_isNewRecord ? winSong : loseSong);
        gameObject.SetActive(true);
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, 1.5f);
        yourTimeText.text = $"Your time: {timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
        bestTimeText.text = $"Best time: {GetRecordTime().Minutes:00}:{GetRecordTime().Seconds:00}";
        // PlayerPrefs.DeleteAll();
    }
}