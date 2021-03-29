using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    [Space(5)]

    [Range(0f, 1f)]
    public float volume;

    [Space(1)]

    [Range(0f, 1f)]
    public float pitch;
}
