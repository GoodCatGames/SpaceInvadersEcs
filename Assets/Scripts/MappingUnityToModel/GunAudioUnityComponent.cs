using UnityEngine;

namespace SpaceInvadersLeoEcs.MappingUnityToModel
{
    public class GunAudioUnityComponent : MonoBehaviour
    {
        [SerializeField] private AudioClip shot = default;
        [SerializeField] private AudioClip reload = default;

        private AudioSource _audioSourceShot = default;
        private AudioSource _audioSourceReload = default;
        
        private void Awake()
        {
            _audioSourceShot = gameObject.AddComponent<AudioSource>();
            _audioSourceShot.clip = shot;
            _audioSourceReload = CreateReloadAudioSource();
        }

        public void Pause()
        {
            _audioSourceShot.Pause();
            _audioSourceReload.Pause();
        }

        public void UnPause()
        {
            _audioSourceShot.UnPause();
            _audioSourceReload.UnPause();
        }
        
        public void PlayShoot() => _audioSourceShot.PlayOneShot(shot, 0.25f);

        public void StartPlayReload() => _audioSourceReload.Play();
        public void StopPlayReload() => _audioSourceReload.Stop();

        private AudioSource CreateReloadAudioSource()
        {
            var audioSourceReload = gameObject.AddComponent<AudioSource>();
            audioSourceReload.clip = reload;
            audioSourceReload.loop = true;
            return audioSourceReload;
        }
    }
}