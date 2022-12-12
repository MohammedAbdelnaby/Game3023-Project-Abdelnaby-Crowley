using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SoundManager : MonoBehaviour
{
    private List<AudioSource> channels;
    private List<AudioClip> audioClips;

    [Range(0.0f, 1.0f)]
    public float Volume;
    // Start is called before the first frame update
    void Awake()
    {
        channels = GetComponents<AudioSource>().ToList();
        audioClips = new List<AudioClip>();
        InitializeSoundSFX();
    }
    
    private void InitializeSoundSFX()
    {
        audioClips.Add(Resources.Load<AudioClip>(""));
    }

    public void PlaySoundFX(Sound sound, Channel channel)
    {
        channels[(int)channel].clip = audioClips[(int)sound];
        channels[(int)channel].Play();
    }

    public void PlayMusic(Sound music)
    {
        channels[(int)Channel.MUSIC].clip = audioClips[(int)music];
        channels[(int)Channel.MUSIC].volume = Volume;
        channels[(int)Channel.MUSIC].loop = true;
        channels[(int)Channel.MUSIC].Play();
    }
}
