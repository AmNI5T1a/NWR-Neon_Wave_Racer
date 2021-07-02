using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;



using UnityEngine;

namespace NWR.Modules
{
    public class Boombox : MonoBehaviour
    {
        public static Boombox instance { get; private set; }

        [Header("References: ")]
        [SerializeField] private List<ListOfMusic> StackOfMusic;

        private AudioSource _source;
        public static SongStyle currentSongStyle = SongStyle.Lobby;
        public static Song lastPlayedSong;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            CheckAndIfNeedsGetAudioSource();
            StartCoroutine(AutoPlayMusic());
        }

        private void CheckAndIfNeedsGetAudioSource()
        {
            if (_source == null)
            {
                _source = this.gameObject.GetComponent<AudioSource>();
            }
        }

        private IEnumerator AutoPlayMusic()
        {
            while (true)
            {
                yield return new WaitUntil(() => _source.isPlaying == false);

                StartNewSong(currentSongStyle);
            }
        }

        public static void SwitchMusicStyle()
        {
            if (currentSongStyle == SongStyle.Play)
            {
                currentSongStyle = SongStyle.Lobby;
            }
            else
            {
                currentSongStyle = SongStyle.Play;
            }
        }

        public void StartNewSong(SongStyle style = SongStyle.Lobby)
        {
            foreach (ListOfMusic list in StackOfMusic)
            {
                if (list.style == style)
                {
                    Song song = list.GetRandomSongFromList();

                    lastPlayedSong = song;
                    _source.clip = song.clip;
                    _source.Play();
                }
            }
        }
    }
}
