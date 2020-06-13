using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plane : MonoBehaviour {
    [SerializeField] private PlaneComponent wingUp;
    [SerializeField] private PlaneComponent wingDown;
    [SerializeField] private PlaneComponent[] enginesUp;
    [SerializeField] private PlaneComponent[] enginesDown;

    private void Start() {
        wingUp.CanMove = false;
        wingDown.CanMove = false;
    }

    private void Update() {
        var isUpMoving = AllEnginesAreMoving(enginesUp);
        var isDownMoving = AllEnginesAreMoving(enginesDown);
        if(isUpMoving) wingUp.CanMove = isUpMoving;
        if(isDownMoving) wingDown.CanMove = isDownMoving;
    }

    private bool AllEnginesAreMoving(PlaneComponent[] engines) {
        return engines.All(engine => engine.IsMoving);
    }
}