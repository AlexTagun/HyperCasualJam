using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameEndManager : MonoBehaviour
{
    [SerializeField] private WinPointsManager _winPointsManager = null;
    [SerializeField] private UIManager _UIManager = null;


    private void Update()
    {
        if (_winPointsManager.points <= 0)
        {
            _UIManager.imageEndGame.DOFade(1, 1.5f).OnComplete(() => SceneManager.LoadScene("HammerEnd"));
        }
    }

}
