using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] _MergeSounds;

    [SerializeField] private AudioClip _CollisionSound;

    [SerializeField] private AudioClip _DropSound;

    private FruitBehaviourScript _fruitBehaviour;

  
    
    void Start()
    {
        _fruitBehaviour = GetComponent<FruitBehaviourScript>();

        _fruitBehaviour.MergeEvent += PlayMergeSound;

        _fruitBehaviour.Dropped += PlayDropSound;
    }
    private void PlayDropSound()
    {
        AudioManager.audioManager.PlaySound(_DropSound);
    }
    private void PlayMergeSound(FruitSO fruit)
    {
        if (fruit.GetMergeSound()==null)
        AudioManager.audioManager.PlaySoundPitch(_MergeSounds[Random.Range(0,_MergeSounds.Length)], Random.Range(0.8f, 1.5f));
        else
            AudioManager.audioManager.PlaySound(fruit.GetMergeSound());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayCollisionSound();
    }
    private void PlayCollisionSound()
    {
        
        AudioManager.audioManager.PlaySoundPitch(_CollisionSound,Random.Range(0.8f,1.5f));
    }
}
