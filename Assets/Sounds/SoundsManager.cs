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
    public static void spawnSoundPlayer(GameObject soundPlayer, Sound audioClip, Vector2 inPosition)
    {
        GameObject theNewSound = Instantiate(soundPlayer);
        if (theNewSound.GetComponent<AudioSource>() == null)
        {
            theNewSound.AddComponent<AudioSource>();
        }
        theNewSound.transform.position = new Vector3(inPosition.x, inPosition.y, theNewSound.transform.position.z);
        theNewSound.AddComponent<SoundsAutoDestroyer>();
    }
}
