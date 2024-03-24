using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider _volumeSlider;

    [SerializeField] private AudioClip _testClip;

    private void OnEnable()
    {
        _volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }
    private void Awake()
    {
        _volumeSlider = GetComponent<Slider>();
    }
    private void Start()
    {
        _volumeSlider.value = AudioManager.audioManager.SfxVolume;


    }

    private void OnDisable()
    {
        _volumeSlider.onValueChanged.RemoveListener(ChangeVolume);
    }
    private void ChangeVolume(float volume)
    {
        AudioManager.audioManager.ChangeVolumeSfx(volume);
        AudioManager.audioManager.PlaySound(_testClip);
        AudioManager.audioManager.SaveVolume();
    }
}
