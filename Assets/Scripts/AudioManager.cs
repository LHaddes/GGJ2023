using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private bool isPotHover = false;

    void Awake () {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void TogglePotHoverSound (bool hover) 
    {
        if (hover)
            StartCoroutine(PlayPotHover());
        else
            StopPotHover();
    }

    public IEnumerator PlayPotHover ()
    {
        isPotHover = true;
        Sound s = Array.Find(sounds, sound => sound.name == "FuseHoverBuild");
        Play("FuseHoverBuild");
        yield return new WaitUntil(() => !s.source.isPlaying);
        if (isPotHover)
            Play("FuseHoverLoop");
    }

    public void StopPotHover () 
    {
        isPotHover = false;
        Stop("FuseHoverBuild");
        Stop("FuseHoverLoop");
    }

}
