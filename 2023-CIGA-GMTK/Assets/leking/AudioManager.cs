using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instants;
    private AudioSource _soundEffectsAudioSource;
    private AudioSource _backGroundMusicAudioSource;
    public List<AudioClip> audioClips;
    [Range(0, 1)] public float musicVolume = 1;
    [Range(0, 1)] public float soundEffectVolume = 1;

    private void Awake()
    {
        if (_instants is null)
        {
            _instants = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _soundEffectsAudioSource = transform.Find("SoundEffects").GetComponent<AudioSource>();
        _backGroundMusicAudioSource = transform.Find("BackGroundMusic").GetComponent<AudioSource>();
        PlayMusic("BGM");
    }

    public static void PlayMusic(string musicName)
    {
        if(_instants._soundEffectsAudioSource is null) return;
        foreach (var clip in _instants.audioClips.Where(clip => clip.name == musicName))
        {
            _instants._soundEffectsAudioSource.PlayOneShot(clip,_instants.musicVolume);
            return;
        }
        Debug.LogWarning($"Can not find the AudioClip Which name of {musicName}");
    }
    public static void PlayClip(string clipName)
    {
        if(_instants._soundEffectsAudioSource is null) return;
        foreach (var clip in _instants.audioClips.Where(clip => clip.name == clipName))
        {
            _instants._soundEffectsAudioSource.PlayOneShot(clip,_instants.soundEffectVolume);
            return;
        }
        Debug.LogWarning($"Can not find the AudioClip Which name of {clipName}");
    }
}