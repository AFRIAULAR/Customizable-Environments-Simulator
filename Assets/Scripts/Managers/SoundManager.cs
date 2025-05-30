using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SoundType
{
    AMBIENT,
    BUTTON_CLOSE,
    SLIDER_MOVE
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private SoundList[] soundList;
    [SerializeField] private AudioClip[] musicList;

    public AudioSource audSource;     // Para efectos
    public AudioSource musicSource;   // Para música/ambiente

    private bool isMuted = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length >= 2)
        {
            audSource = sources[0];
            musicSource = sources[1];
        }
        else
        {
            Debug.LogError("SoundManager necesita dos AudioSource en el mismo GameObject.");
        }

        musicSource.loop = true;
    }

    private void Start()
    {
        // Música de ambiente al iniciar
        if (musicList.Length > 0)
        {
            musicSource.clip = musicList[0]; // Usamos el primer clip
            musicSource.Play();
        }
    }

    public void PlaySound(SoundType sound, float vol = 0.5f)
    {
        if (isMuted) return;

        AudioClip[] clips = soundList[(int)sound].Sounds;
        if (clips == null || clips.Length == 0) return;

        AudioClip rndClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        audSource.PlayOneShot(rndClip, vol);
    }

    public void StopSound()
    {
        audSource.Stop();
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        audSource.mute = isMuted;
        musicSource.mute = isMuted;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }
#endif
}

[Serializable]
public struct SoundList
{
    public string name;
    [SerializeField] private AudioClip[] sounds;
    public AudioClip[] Sounds => sounds;
}
