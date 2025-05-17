using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] pickSounds;
    [SerializeField] private AudioClip[] successSounds;
    [SerializeField] private AudioClip[] failureSounds;
    
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomSound(SoundType type)
    {
        AudioClip clip;
        switch (type)
        {
            case SoundType.Pick:
                clip = pickSounds[Random.Range(0, pickSounds.Length)];
                break;
            case SoundType.Success:
                clip = successSounds[Random.Range(0, successSounds.Length)];
                break;
            case SoundType.Failure:
                clip = failureSounds[Random.Range(0, failureSounds.Length)];
                break;
            default:
                clip = pickSounds[0];
                break;
        }
        _audioSource.PlayOneShot(clip);
    }
}

public enum SoundType
{
    Pick,
    Success,
    Failure
}