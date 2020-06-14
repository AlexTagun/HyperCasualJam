using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsAutoDestroyer : MonoBehaviour
{
    private AudioSource _audioSource = null;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!_audioSource.isPlaying)
            Destroy(gameObject);
    }
}
