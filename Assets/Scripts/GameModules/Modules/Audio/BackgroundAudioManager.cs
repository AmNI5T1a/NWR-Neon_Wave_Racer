using UnityEngine;
using UnityEngine.Audio;

namespace NWR
{
    public class BackgroundAudioManager : MonoBehaviour
    {
        [SerializeField] public Sound[] lobbyMusic;

        [HideInInspector] private AudioSource _audioSource;

        void Awake()
        {
            byte inStartGeneratedMusic = (byte)Random.Range(0, lobbyMusic.Length);

            _audioSource = this.gameObject.GetComponent<AudioSource>();
            _audioSource.clip = lobbyMusic[inStartGeneratedMusic].clip;
            _audioSource.pitch = lobbyMusic[inStartGeneratedMusic].pitch;
            _audioSource.Play();
        }

        void Update()
        {
            if (!_audioSource.isPlaying)
            {
                byte newClipNumber = (byte)Random.Range(0f, lobbyMusic.Length);

                _audioSource.clip = lobbyMusic[newClipNumber].clip;
                _audioSource.pitch = lobbyMusic[newClipNumber].pitch;
                _audioSource.Play();
            }

            if (VirtualInputManager.Instance.C)
            {
                NextClip();
            }
        }

        void NextClip()
        {
            byte newClipNumber = (byte)Random.Range(0f, lobbyMusic.Length);

            _audioSource.clip = lobbyMusic[newClipNumber].clip;
            _audioSource.pitch = lobbyMusic[newClipNumber].pitch;
            _audioSource.Play();
        }
    }
}
