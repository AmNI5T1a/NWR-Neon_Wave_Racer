using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace NWR
{
    public class UI_Inventory : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] public LobbyManager _lobbyManager;
        [SerializeField] private GameObject _player;

        [Space(10)]

        [Header("RoadMenuUI Elements: ")]
        [SerializeField] private GameObject _selectRoadMenu;
        [SerializeField] private GameObject _roadSlotContainer;
        [SerializeField] private GameObject _roadSlotTemplate;

        [Space(10)]
        [Header("GameStylesUI Elements")]
        [SerializeField] private GameObject _selectGameModeMenu;
        [SerializeField] private GameObject _gameStyleSlotContainer;
        [SerializeField] private GameObject _gameStyleSlotTemplate;

        [Space(10)]

        [Header("CarsMenuUI Elements")]
        [SerializeField] private GameObject _carsMenuButtton;
        [SerializeField] private GameObject _carsMenuSlotContainer;
        [SerializeField] private GameObject _carsMenuSlotTemplate;

        [Space(10)]

        [Header("PlayerStatsUI Elements")]
        [SerializeField] private GameObject _playersStatsMenu;

        [Header("In game settings: ")]
        [SerializeField] private List<GameObject> _listOfInstanciatedUIElements;
        [SerializeField] private Button _button;


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
                    // Create slot template
                    GameObject slot = Instantiate(_roadSlotTemplate, _roadSlotContainer.transform);

                    // SetPrice
                    if (item.boughtStatus == false)
                    {
                        slot.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Text>().text = item.price.ToString();

                        _button = slot.transform.GetChild(4).GetChild(0).GetComponent<Button>();

                        _button.AddEventListener(item, BuyButtonClicked);
                    }
                    else if (item.boughtStatus == true)
                    {
                        slot.transform.GetChild(4).gameObject.SetActive(false);
                        slot.transform.GetChild(5).gameObject.SetActive(true);

                        _button = slot.transform.GetChild(5).GetChild(0).GetComponent<Button>();
                        _button.AddEventListener(item, ChooseButtonClicked);
                    }

                    // Set name of the road
                    slot.transform.GetChild(1).GetComponent<Text>().text = item.posNumber.ToString();

                    // Set number of the road
                    slot.transform.GetChild(3).GetComponent<Text>().text = item.name;

                    slot.SetActive(true);

                    _listOfInstanciatedUIElements.Add(slot);
                }
                else if (item.itemType == Item.ItemType.GameStyle)
                {
                    GameObject slot = Instantiate(_gameStyleSlotTemplate, _gameStyleSlotContainer.transform);
                    slot.SetActive(true);

                    // Set play mode number
                    slot.transform.GetChild(1).GetComponent<Text>().text = item.posNumber.ToString();

                    // Set play mode name
                    slot.transform.GetChild(3).GetComponent<Text>().text = item.name;

                    // Button
                    _button = slot.transform.GetChild(4).GetChild(0).GetComponent<Button>();
                    _button.AddEventListener(item, ChooseButtonClicked);


                    _listOfInstanciatedUIElements.Add(slot);
                }
                else if (item.itemType == Item.ItemType.Car)
                {
                    GameObject slot = Instantiate(_carsMenuSlotTemplate, _carsMenuSlotContainer.transform);

                    slot.SetActive(true);
                }
            }
            UpdateMoneyScore();
        }

        void OnShopItemButtonClicked(Item item)
        {
            switch (item.itemType)
            {
                case Item.ItemType.Road:

                    break;
                case Item.ItemType.GameStyle:

                    break;
                case Item.ItemType.Car:

                    break;
            }
        }

        void ChooseButtonClicked(Item item)
        {
            string itemName = item.name;

            switch (item.itemType)
            {
                case Item.ItemType.Road:
                    _lobbyManager.SetRoad(item);
                    _selectRoadMenu.transform.GetChild(2).GetComponent<Text>().text = itemName;
                    _lobbyManager.OpenOrCloseSelectRoadMenu();
                    break;
                case Item.ItemType.GameStyle:
                    _lobbyManager.SetGameMode(item);
                    _selectGameModeMenu.transform.GetChild(2).GetComponent<Text>().text = itemName;
                    _lobbyManager.OpenOrCloseSelectGameStyle();
                    break;
                case Item.ItemType.Car:
                    Debug.LogWarning("Work in progress");
                    break;
            }
        }

        void BuyButtonClicked(Item item)
        {
            Debug.Log("BuyButtonClicked");
        }
        private void UpdateMoneyScore()
        {
            _playersStatsMenu.transform.GetChild(1).GetComponent<Text>().text = _player.GetComponent<PlayerSettings>().playerMoney.ToString();
        }
    }
}
