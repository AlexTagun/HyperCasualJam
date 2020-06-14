using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainBack : MonoBehaviour {
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed;

    private float _curValue;
    
    private void Start() {
        _curValue = 0;
    }

    private void Update() {
        _curValue += speed;
        spriteRenderer.material.mainTextureOffset = new Vector2(_curValue, 0);
    }
}
