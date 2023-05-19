using System;
using UnityEngine;

namespace Gameplay
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource AudioSource;
        public AudioClip   ExplosionAudioClip;

        public static Action            PlayExplosionSound;
        public static Action<AudioClip> PlaySound;


        private void OnEnable()
        {
            PlayExplosionSound += OnPlayExplosionSoundCalled;
            PlaySound          += OnPlaySoundCalled;
        }

        private void OnPlayExplosionSoundCalled()
        {
            AudioSource.PlayOneShot(ExplosionAudioClip);
        }

        private void OnPlaySoundCalled(AudioClip audioClip)
        {
            AudioSource.PlayOneShot(audioClip);
        }
    }
}