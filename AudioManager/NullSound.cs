using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullSound : ISound
{
    public float GetLength()
    {
        return 0f;
    }

    public void Play()
    {
        return;
    }

    public void SetSource(AudioSource source)
    {
        return;
    }
}
