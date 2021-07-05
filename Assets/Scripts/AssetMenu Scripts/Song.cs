using System.Collections.Generic;
using UnityEngine;

namespace NWR.Modules
{
    [CreateAssetMenu(fileName = "Song", menuName = "NWR/Music/Song")]
    public class Song : ScriptableObject
    {
        public ushort Id;
        public AudioClip clip;
        public string description;
    }
}
