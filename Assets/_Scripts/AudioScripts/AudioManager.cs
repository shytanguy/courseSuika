using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;


    [SerializeField] private AudioSource musicSource, sfxSource;

    public float MusicVolume, SfxVolume;

    [SerializeField] private float _TimeBetweenSameSounds;

    private AudioClip _lastSound;

    private float _initPitch;
    private void Start()
    {
        if (audioManager == null)
        {
            audioManager = this;

            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        ChangeVolumeMusic(PlayerPrefs.GetFloat("music volume", 0.5f));

        ChangeVolumeSfx(PlayerPrefs.GetFloat("sfx volume", 0.5f));

        _initPitch = sfxSource.pitch;
    }


    public void ChangeMusic(AudioClip clip)
    {
        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
        musicSource.loop = true;
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != _lastSound)
        {
            sfxSource.PlayOneShot(clip);
            _lastSound = clip;
            StartCoroutine(SoundPlayingCoolDown());
        }
    }

    private IEnumerator SoundPlayingCoolDown()
    {
        yield return new WaitForSeconds(_TimeBetweenSameSounds);
        _lastSound = null;
    }
    public void PlaySound(AudioClip clip,float VolumeMulti)
    {
      
        if (clip != _lastSound)
        {
            sfxSource.pitch = _initPitch;
            sfxSource.PlayOneShot(clip, VolumeMulti * SfxVolume);
            _lastSound = clip;
            StartCoroutine(SoundPlayingCoolDown());
        }
    }

    public void PlaySoundPitch(AudioClip clip,float pitch)
    {
        sfxSource.pitch = pitch;
        PlaySound(clip);

       
    }
    public void PlaySoundDelay(AudioClip clip, float delay)
    {

        StartCoroutine(DelaySound(clip, delay));
    }
    private IEnumerator DelaySound(AudioClip clip,float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        if (clip != _lastSound)
        {
            sfxSource.PlayOneShot(clip);
            _lastSound = clip;
            StartCoroutine(SoundPlayingCoolDown());
        }
    }
    public void ChangeVolumeSfx(float volume)
    {
        SfxVolume = volume;

        sfxSource.volume = SfxVolume;
    }

    public void ChangeVolumeMusic(float volume)
    {
        MusicVolume = volume;

        musicSource.volume = MusicVolume;
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("music volume", MusicVolume);

        PlayerPrefs.SetFloat("sfx volume", SfxVolume);

        PlayerPrefs.Save();
    }
}
