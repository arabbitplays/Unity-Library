using UnityEngine;
public interface ISound
{
    void SetSource(AudioSource source);
    void Play();
    float GetLength();
}
