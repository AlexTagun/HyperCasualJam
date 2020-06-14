using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image imageEndGame = null;

    //[SerializeField] private GameObject _endPanel;
    // Start is called before the first frame update
    void Start()
    {
        imageEndGame.DOFade(0, 0f);
        //EventManager.OnEndGame += ShowFinalWindow;
        //_endPanel.SetActive(false);
    }

}
