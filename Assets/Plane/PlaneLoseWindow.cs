using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaneLoseWindow : MonoBehaviour {
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI yourTimeText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private CanvasGroup _canvasGroup;
    public static Action<TimeSpan> OnGameEnd;

    private bool _isNewRecord;

    private void Awake() {
        tryAgainButton.onClick.AddListener(() => SceneManager.LoadScene("Plane"));
        mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        
        OnGameEnd += OnGameEndHandler;
        gameObject.SetActive(false);
    }

    private TimeSpan GetRecordTime() {
        return new TimeSpan(long.Parse(PlayerPrefs.GetString("PlaneRecord", "-1")));
    }
    
    private void SetRecordTime(TimeSpan timeSpan) {
        PlayerPrefs.SetString("PlaneRecord", timeSpan.Ticks.ToString());
        PlayerPrefs.Save();
    }


    private void OnGameEndHandler(TimeSpan timeSpan) {
        _isNewRecord = (timeSpan.Minutes * 60 + timeSpan.Seconds) > (GetRecordTime().Minutes * 60 + GetRecordTime().Seconds);
        if(_isNewRecord) SetRecordTime(timeSpan);
        gameObject.SetActive(true);
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, 1.5f);
        yourTimeText.text = $"Your time: {timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
        bestTimeText.text = $"Best time: {GetRecordTime().Minutes:00}:{GetRecordTime().Seconds:00}";
        // PlayerPrefs.DeleteAll();
    }
}