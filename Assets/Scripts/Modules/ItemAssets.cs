using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NWR
{
    public class ItemAssets : MonoBehaviour
    {
        public static ItemAssets Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        // If item doesnt found will show unknown sprite
        [SerializeField] public Sprite unknownItemSprite;
        [Space(5)]
        [SerializeField] public Sprite golfPreviewSprite;
        [SerializeField] public Sprite subaruPreviewSprite;

        [Space(10)]
        [SerializeField] public List<GameObject> availableCarList;

        [Space(10)]
        [SerializeField] public List<AudioClip> lobbyBackgroundMusic;
        [SerializeField] public List<AudioClip> playModeMusic;
    }
}
