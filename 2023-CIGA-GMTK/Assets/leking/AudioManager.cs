using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instants;
    private AudioSource _audioSource;
    public List<AudioClip> audioClips;

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
        _audioSource = GetComponent<AudioSource>();
    }

    public static void Play(string clipName)
    {
        if(_instants._audioSource is null) return;
        foreach (var clip in _instants.audioClips.Where(clip => clip.name == clipName))
        {
            _instants._audioSource.PlayOneShot(clip);
            return;
        }
        Debug.LogWarning($"Can not find the AudioClip Which name of {clipName}");
    }
}