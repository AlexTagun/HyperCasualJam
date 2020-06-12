using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject _endPanel;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnEndGame += ShowFinalWindow;
        _endPanel.SetActive(false);
    }


    private void ShowFinalWindow()
    {
        _endPanel.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
