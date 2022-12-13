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
        audioClips.Add(Resources.Load<AudioClip>("Audio/BattleAlert"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/ButtonClick"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/OverWorld"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/Battle"));
    }

    public void PlaySoundFX(Sound sound, Channel channel)
    {
        channels[(int)channel].clip = audioClips[(int)sound];
        if (channel == Channel.MUSIC)
        {
            channels[(int)channel].volume = Volume;
            channels[(int)channel].loop = true;
        }
        channels[(int)channel].Play();
    }
}
