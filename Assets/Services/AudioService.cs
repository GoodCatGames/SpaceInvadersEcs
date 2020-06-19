using UnityEngine;

namespace SpaceInvadersLeoEcs.Services
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private AudioClip shot = default;
        [SerializeField] private AudioClip reload = default;

        private AudioSource _audioSourceShot = default;

        private AudioSource _audioSourceReloadPlayer1 = default;
        private AudioSource _audioSourceReloadPlayer2 = default;

        private void Awake()
        {
            _audioSourceShot = gameObject.AddComponent<AudioSource>();
            _audioSourceShot.clip = shot;

            _audioSourceReloadPlayer1 = CreateReloadAudioSource();
            _audioSourceReloadPlayer2 = CreateReloadAudioSource();
        }

        public void Pause()
        {
            _audioSourceShot.Pause();
            _audioSourceReloadPlayer1.Pause();
            _audioSourceReloadPlayer2.Pause();
        }

        public void UnPause()
        {
            _audioSourceShot.UnPause();
            _audioSourceReloadPlayer1.UnPause();
            _audioSourceReloadPlayer2.UnPause();
        }
        
        public void PlayShoot() => _audioSourceShot.PlayOneShot(shot, 0.25f);

        public void StartPlayReloadPlayer(in int numberPlayer)
        {
            if (numberPlayer == 1) StartPlayReloadPlayer1();
            if (numberPlayer == 2) StartPlayReloadPlayer2();
        }

        public void StopPlayReload(in int numberPlayer)
        {
            if (numberPlayer == 1) StopPlayReloadPlayer1();
            if (numberPlayer == 2) StopPlayReloadPlayer2();
        }

        private void StartPlayReloadPlayer1() => _audioSourceReloadPlayer1.Play();
        private void StartPlayReloadPlayer2() => _audioSourceReloadPlayer2.Play();
        private void StopPlayReloadPlayer1() => _audioSourceReloadPlayer1.Stop();
        private void StopPlayReloadPlayer2() => _audioSourceReloadPlayer2.Stop();

        private AudioSource CreateReloadAudioSource()
        {
            var audioSourceReload = gameObject.AddComponent<AudioSource>();
            audioSourceReload.clip = reload;
            audioSourceReload.loop = true;
            return audioSourceReload;
        }
    }
}