using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace NWR
{
    public class UI_Inventory : MonoBehaviour, IUIElementSwitcher, ICloseLastOpenedUIElement
    {
        [Header("References: ")]
        [SerializeField] public LobbyManager _lobbyManager;
        [SerializeField] private GameObject _player;
        [SerializeField] private Canvas _canvas;

        [SerializeField] private GameObject _closeLastOpenedUIElement;

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
        [SerializeField] private GameObject _buyACarButton;
        [SerializeField] private GameObject _carsMenuSlotContainer;
        [SerializeField] private GameObject _carsMenuSlotTemplate;

        [Space(10)]

        [Header("PlayerStatsUI Elements")]
        [SerializeField] private GameObject _playersStatsMenu;

        [Header("In game settings: ")]
        [SerializeField] public static List<GameObject> listOfOpenedUIElements;
        [SerializeField] private List<GameObject> _listOfInstanciatedUIElements;
        [HideInInspector] private Button _button;
        [SerializeField] private bool previewCarModeActive = false;
        [SerializeField] private GameObject _carForPreview;

        void Awake()
        {
            listOfOpenedUIElements = new List<GameObject>();
        }

        void Start()
        {

        }

        void Update()
        {
            ShowOrHideArrorUIElement();
        }

        private void ShowOrHideArrorUIElement()
        {
            if (listOfOpenedUIElements.Count >= 1)
            {
                _closeLastOpenedUIElement.SetActive(true);
            }
            else if (listOfOpenedUIElements.Count == 0)
            {
                _closeLastOpenedUIElement.SetActive(false);
            }
        }

        public void DestroyAllUIComponentsBeforeRefresh()
        {
            for (byte c = 0; c < _listOfInstanciatedUIElements.Count; c++)
            {
                Destroy(_listOfInstanciatedUIElements[c]);
            }

            _listOfInstanciatedUIElements.Clear();
        }

        public void RefreshInventory()
        {
            _listOfInstanciatedUIElements = new List<GameObject>();

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

                    slot.transform.GetChild(0).GetComponent<Image>().sprite = item.GetItemSprite();

                    _button = slot.transform.GetChild(1).GetComponent<Button>();
                    if (item.boughtStatus == true)
                    {
                        _button.AddEventListener(item, ChooseButtonClicked);
                    }
                    else if (item.boughtStatus == false)
                    {
                        _button.AddEventListener(item, PreviewCar);
                    }

                    slot.SetActive(true);

                    _listOfInstanciatedUIElements.Add(slot);
                }
            }
            UpdateMoneyScore();
        }

        private void ClosePreviewMode()
        {
            _carForPreview.SetActive(false);
            previewCarModeActive = false;
            Destroy(buyButton);
        }

        GameObject buyButton;

        void PreviewCar(Item item)
        {
            if (previewCarModeActive)
                ClosePreviewMode();

            _player.GetComponent<PlayerSettings>().playerCar.SetActive(false);

            _carForPreview = item.GetCarAsGameObject();
            _carForPreview.SetActive(true);

            buyButton = Instantiate(_buyACarButton, _buyACarButton.transform.position, _buyACarButton.transform.rotation);
            buyButton.transform.SetParent(_canvas.transform);
            buyButton.GetComponent<RectTransform>().localScale = buyButton.GetComponent<RectTransform>().localScale * 2;
            buyButton.GetComponent<Button>().AddEventListener(item, BuyButtonClicked);
            buyButton.transform.GetChild(1).GetComponent<Text>().text = item.price.ToString();
            buyButton.SetActive(true);

            previewCarModeActive = true;
        }

        void ChooseButtonClicked(Item item)
        {
            switch (item.itemType)
            {
                case Item.ItemType.Road:
                    _lobbyManager.SetRoad(item);
                    _selectRoadMenu.transform.GetChild(2).GetComponent<Text>().text = item.name;

                    GameObject listOfRoads = _selectRoadMenu.transform.parent.transform.GetChild(5).gameObject;
                    listOfRoads.SetActive(false);
                    listOfOpenedUIElements.Remove(listOfRoads);
                    break;
                case Item.ItemType.GameStyle:
                    _lobbyManager.SetGameMode(item);
                    _selectGameModeMenu.transform.GetChild(2).GetComponent<Text>().text = item.name;

                    GameObject listOfGameStyles = _selectGameModeMenu.transform.parent.transform.GetChild(6).gameObject;
                    listOfGameStyles.SetActive(false);
                    listOfOpenedUIElements.Remove(listOfGameStyles);
                    break;
                case Item.ItemType.Car:
                    if (previewCarModeActive)
                        ClosePreviewMode();

                    _player.GetComponent<PlayerSettings>().playerCar.SetActive(false);
                    _player.GetComponent<PlayerSettings>().UpdatePlayerCar(item);

                    _lobbyManager.SetCarName(item);
                    UpdateSelectedPlayerCarInUIComponent(item);
                    break;
            }
        }

        private void BuyButtonClicked(Item item)
        {
            switch (item.itemType)
            {
                case Item.ItemType.Road:
                    _lobbyManager.BuyItemFromShop(ref item);
                    UpdateMoneyScore();
                    break;
                case Item.ItemType.GameStyle:
                    _lobbyManager.BuyItemFromShop(ref item);
                    UpdateMoneyScore();
                    break;
                case Item.ItemType.Car:
                    bool transactionCompletedStatus = _lobbyManager.BuyItemFromShop(ref item);

                    if (transactionCompletedStatus)
                    {
                        _player.GetComponent<PlayerSettings>().playerCar = item.GetCarAsGameObject();

                        Destroy(buyButton);

                        GameObject carsMenu = _selectGameModeMenu.transform.parent.transform.GetChild(8).gameObject;
                        carsMenu.SetActive(false);
                        listOfOpenedUIElements.Remove(carsMenu);

                        ClosePreviewMode();

                        UpdateMoneyScore();

                        UpdateSelectedPlayerCarInUIComponent(item);
                        _lobbyManager.SetCarName(item);

                        _player.GetComponent<PlayerSettings>().playerCar.SetActive(true);
                        break;
                    }
                    else
                    {
                        Debug.Log("Not enough money");
                    }
                    break;
            }
            DestroyAllUIComponentsBeforeRefresh();
            RefreshInventory();
        }
        public void UpdateMoneyScore() => _playersStatsMenu.transform.GetChild(1).GetComponent<Text>().text = _player.GetComponent<PlayerSettings>().playerMoney.ToString();
        public void UpdateSelectedPlayerCarInUIComponent(in Item CarItem) =>
            _carsMenuButtton.transform.GetChild(2).GetComponent<Text>().text = CarItem.name.ToString();

        public void CloseOrOpenUIElement(GameObject gameObject)
        {
            if (gameObject.activeInHierarchy == false)
            {
                gameObject.SetActive(true);
                listOfOpenedUIElements.Add(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
                listOfOpenedUIElements.Remove(gameObject);
            }
        }

        public void CloseUIElement()
        {
            listOfOpenedUIElements[listOfOpenedUIElements.Count - 1].SetActive(false);
            listOfOpenedUIElements.Remove(listOfOpenedUIElements[listOfOpenedUIElements.Count - 1]);
        }
    }
}