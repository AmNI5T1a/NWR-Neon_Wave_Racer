using UnityEngine;

namespace NWR.Modules
{
    [CreateAssetMenu(fileName = "GameMode", menuName = "NWR/Items/GameMode")]
    public class GameMode : Item
    {
        [SerializeField] private Sprite image;
    }
}
