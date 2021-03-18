using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NWR
{
    public class UI_Inventory : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] public LobbyManager _lobbyManager;
        [Space(10)]
        [Header("RoadMenuUI Elements: ")]
        [SerializeField] private GameObject _roadSlotContainer;
        [SerializeField] private GameObject _roadSlotTemplate;
        [SerializeField] private GameObject _roadBoughtButton;
        [Space(10)]
        [Header("GameStylesUI Elements")]
        [SerializeField] private GameObject _gameStyleSlotContainer;
        [SerializeField] private GameObject _gameStyleSlotTemplate;
        [SerializeField] private GameObject _gameStylesBoughtButton;

        [Header("In game settings: ")]
        [SerializeField] private List<GameObject> _listOfInstanciatedUIElements;

        void Awake()
        {
            _listOfInstanciatedUIElements = new List<GameObject>();
        }

        public void RefreshInventory()
        {
            foreach (Item item in _lobbyManager.inventory.GetInventory())
            {
                if (item.itemType == Item.ItemType.Road)
                {
                    GameObject slot = Instantiate(_roadSlotTemplate, _roadSlotContainer.transform);
                    slot.SetActive(true);
                    _listOfInstanciatedUIElements.Add(slot);
                    Debug.Log("UI shows +1 road");
                }
                else if (item.itemType == Item.ItemType.GameStyle)
                {
                    GameObject slot = Instantiate(_gameStyleSlotTemplate, _gameStyleSlotContainer.transform);
                    slot.SetActive(true);
                    _listOfInstanciatedUIElements.Add(slot);

                    Debug.Log("UI shows +1 gameStyle");
                }
            }
        }
    }
}
