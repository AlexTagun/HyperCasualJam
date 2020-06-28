using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainBack : MonoBehaviour {
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private float speed = 0f;

    private float _curValue = 0f;
    
    private void Start() {
        _curValue = 0;
    }

    private void Update() {
        _curValue += speed;
        spriteRenderer.material.mainTextureOffset = new Vector2(_curValue, 0);
    }
}
