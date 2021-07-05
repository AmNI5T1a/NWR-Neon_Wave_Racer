using System;
using System.Collections.Generic;
using UnityEngine;

namespace NWR.Modules
{
    public enum SongStyle
    {
        Play,
        Lobby
    }



    [CreateAssetMenu(fileName = "List of Music", menuName = "NWR/Music/List of music")]
    public class ListOfMusic : ScriptableObject
    {
        public SongStyle style;
        public List<Song> listOfMusic;

        public Song GetRandomSongFromList()
        {
            System.Random rnd = new System.Random();

            return listOfMusic[rnd.Next(0, listOfMusic.Count)];
        }
    }
}
