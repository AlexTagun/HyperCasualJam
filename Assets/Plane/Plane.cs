using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Plane : MonoBehaviour {
    [SerializeField] private PlaneComponent wingUp;
    [SerializeField] private PlaneComponent wingDown;
    [SerializeField] private PlaneComponent[] enginesUp;
    [SerializeField] private PlaneComponent[] enginesDown;
    [SerializeField] private Image heightImage;
    [SerializeField] private GameObject loseWindow;
    
    public static Action<PlaneComponent> OnComponentStartMoving;
    public static Action<PlaneComponent> OnComponentStopMoving;
    private PlaneComponent[] _allPlaneComponents;

    private float _startHeightValue = 8;
    private float _curHeightValue;

    private float CurHeightValue {
        set {
            if(value <= 0) loseWindow.SetActive(true);
            var newScale = heightImage.transform.localScale;
            newScale.y = value / _startHeightValue;
            newScale.y = Mathf.Clamp01(newScale.y);
            heightImage.transform.localScale = newScale;
            _curHeightValue = value;
        }
        get => _curHeightValue;
    }

    private void Start() {
        wingUp.CanMove = false;
        wingDown.CanMove = false;

        _allPlaneComponents = GetComponentsInChildren<PlaneComponent>();

        _curHeightValue = _startHeightValue;
        OnComponentStartMoving += (obj) => {
            CurHeightValue -= 1;
        };
        
        OnComponentStopMoving += (obj) => {
            CurHeightValue += 0.75f;
        };
    }

    private void Update() {
        wingUp.CanMove = enginesUp.All(engine => engine.IsMoving);
        wingDown.CanMove = enginesDown.All(engine => engine.IsMoving);
        
        foreach (var engine in enginesUp) engine.CanPut = !wingUp.IsMoving;
        foreach (var engine in enginesDown) engine.CanPut = !wingDown.IsMoving;
        
    }
}