using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public struct Sound
{
    public AudioClip _audioClip;
    [Range(0, 1)] public float _volume;
}
public class SoundsManager : MonoBehaviour
{
    public static AudioSource spawnSoundPlayer(GameObject soundPlayer, Sound audioClip, Vector2 inPosition)
    {
        GameObject theNewSound = Instantiate(soundPlayer);
        if (theNewSound.GetComponent<AudioSource>() == null)
        {
            theNewSound.AddComponent<AudioSource>();
        }
        AudioSource _audioSource = theNewSound.GetComponent<AudioSource>();
        _audioSource.clip = audioClip._audioClip;
        _audioSource.volume = audioClip._volume;
        _audioSource.Play();
        theNewSound.transform.position = new Vector3(inPosition.x, inPosition.y, theNewSound.transform.position.z);
        theNewSound.AddComponent<SoundsAutoDestroyer>();
        return _audioSource;
    }
}
