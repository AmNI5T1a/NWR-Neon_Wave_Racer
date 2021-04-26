using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace NWR
{
    public class UI_Manager : MonoBehaviour, IUIElementSwitcher, ICloseLastOpenedUIElement
    {
        [Header("References: ")]
        [SerializeField] private LobbyManager _lobbyManager;
        [SerializeField] private GameObject _player;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Inventory _inventory;

        [SerializeField] private GameObject _closeLastOpenedUIElement;

        [Space(10)]

        [Header("RoadMenuUI Elements: ")]
        [SerializeField] private GameObject _listOfRoadsUIComponent;
        [SerializeField] private GameObject _selectRoadMenu;
        [SerializeField] private GameObject _roadSlotContainer;
        [SerializeField] private GameObject _roadSlotTemplate;

        [Space(10)]
        [Header("GameStylesUI Elements")]
        [SerializeField] private GameObject _listOfGameStylesUIComponent;
        [SerializeField] private GameObject _selectGameModeMenu;
        [SerializeField] private GameObject _gameStyleSlotContainer;
        [SerializeField] private GameObject _gameStyleSlotTemplate;

        [Space(10)]

        [Header("CarsMenuUI Elements")]
        [SerializeField] private GameObject _listOfCarsUIComponent;
        [SerializeField] private GameObject _carsMenuButtton;
        [SerializeField] private GameObject _buyACarButton;
        [SerializeField] private GameObject _carsMenuSlotContainer;
        [SerializeField] private GameObject _carsMenuSlotTemplate;

        [Space(10)]

        [Header("PlayerStatsUI Elements")]
        [SerializeField] private GameObject _playersStatsMenu;

        [Header("In game settings: ")]
        [HideInInspector] public static List<GameObject> listOfOpenedUIElements;
        [HideInInspector] private Button _button;
        [HideInInspector] private GameObject _buyButton;
        [SerializeField] private bool previewCarModeActive = false;
        [HideInInspector] private GameObject _carForPreview;

        [SerializeField] public List<GameObject> listOfInstanciatedUIElements;
        [HideInInspector] private List<GameObject> _listOfInstanciatedCarsAsUIElements;
        [HideInInspector] private List<GameObject> _listOfInstanciatedRoadsAsUIElements;
        [HideInInspector] private List<GameObject> _listOfInstanciatedGameStylesAsUIElements;

        void Awake()
        {
            listOfOpenedUIElements = new List<GameObject>();
        }

        void Start()
        {
            _listOfCarsUIComponent.SetActive(false);
            _listOfGameStylesUIComponent.SetActive(false);
            _listOfRoadsUIComponent.SetActive(false);

            RefreshCarsMenu();
            RefreshGameStylesMenu();
            RefreshRoadsMenu();
            UpdateMoneyScore();
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
        public void DestroyAllUIElements()
        {
            foreach (GameObject slot in listOfInstanciatedUIElements)
            {
                Destroy(slot);
            }

            listOfInstanciatedUIElements.Clear();
        }

        public void DestroyAllCarsUIElements()
        {
            foreach (GameObject obj in _listOfInstanciatedCarsAsUIElements)
            {
                Destroy(obj);
            }
            _listOfInstanciatedCarsAsUIElements.Clear();
        }
        public void DestroyAllRoadsUIElements()
        {
            foreach (GameObject obj in _listOfInstanciatedRoadsAsUIElements)
            {
                Destroy(obj);
            }

            _listOfInstanciatedRoadsAsUIElements.Clear();
        }

        public void DestroyAllGameStylesUIElements()
        {
            foreach (GameObject obj in _listOfInstanciatedGameStylesAsUIElements)
            {
                Destroy(obj);
            }

            _listOfInstanciatedGameStylesAsUIElements.Clear();
        }

        public void RefreshCarsMenu()
        {
            _listOfInstanciatedCarsAsUIElements = new List<GameObject>();

            foreach (Car car in _inventory.GetListOfCars())
            {
                GameObject slot = Instantiate(_carsMenuSlotTemplate, _carsMenuSlotContainer.transform);

                slot.transform.GetChild(0).GetComponent<Image>().sprite = car.GetSprite();

                _button = slot.transform.GetChild(1).GetComponent<Button>();

                if (car.WhetherItemWasPuchasedOrNot())
                    _button.AddEventListener(car, ChooseButtonClicked);
                else
                    _button.AddEventListener(car, PreviewCar);

                slot.SetActive(true);

                listOfInstanciatedUIElements.Add(slot);
                _listOfInstanciatedCarsAsUIElements.Add(slot);
            }
        }
        public void RefreshRoadsMenu()
        {
            _listOfInstanciatedRoadsAsUIElements = new List<GameObject>();

            foreach (Road road in _inventory.GetListOfRoads())
            {
                GameObject slot = Instantiate(_roadSlotTemplate, _roadSlotContainer.transform);

                if (!road.WhetherItemWasPuchasedOrNot())
                {
                    slot.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Text>().text = road.GetPrice().ToString();

                    _button = slot.transform.GetChild(4).GetChild(0).GetComponent<Button>();

                    _button.AddEventListener(road, BuyButtonClicked);
                }
                else
                {
                    slot.transform.GetChild(4).gameObject.SetActive(false);
                    slot.transform.GetChild(5).gameObject.SetActive(true);

                    _button = slot.transform.GetChild(5).GetChild(0).GetComponent<Button>();
                    _button.AddEventListener(road, ChooseButtonClicked);
                }


                slot.transform.GetChild(1).GetComponent<Text>().text = road.GetPositionNumber().ToString();


                slot.transform.GetChild(3).GetComponent<Text>().text = road.GetName().ToString();

                slot.SetActive(true);

                listOfInstanciatedUIElements.Add(slot);
                _listOfInstanciatedRoadsAsUIElements.Add(slot);
            }
        }
        public void RefreshGameStylesMenu()
        {
            _listOfInstanciatedGameStylesAsUIElements = new List<GameObject>();

            foreach (GameStyle gameStyle in _inventory.GetListOfGameStyles())
            {
                GameObject slot = Instantiate(_gameStyleSlotTemplate, _gameStyleSlotContainer.transform);

                slot.transform.GetChild(1).GetComponent<Text>().text = gameStyle.GetPositionNumber().ToString();

                slot.transform.GetChild(3).GetComponent<Text>().text = gameStyle.GetName().ToString();


                _button = slot.transform.GetChild(4).GetChild(0).GetComponent<Button>();
                _button.AddEventListener(gameStyle, ChooseButtonClicked);

                slot.SetActive(true);

                listOfInstanciatedUIElements.Add(slot);
                _listOfInstanciatedGameStylesAsUIElements.Add(slot);
            }
        }

        private void ClosePreviewMode()
        {
            _carForPreview.SetActive(false);
            previewCarModeActive = false;
            Destroy(_buyButton);

            _player.GetComponent<PlayerSettings>().playerCar.SetActive(true);
        }

        void PreviewCar(Car car)
        {
            if (previewCarModeActive)
                ClosePreviewMode();

            _player.GetComponent<PlayerSettings>().playerCar.SetActive(false);

            _carForPreview = car.GetCarAsGameObject();
            _carForPreview.SetActive(true);

            _buyButton = Instantiate(_buyACarButton, _buyACarButton.transform.position, _buyACarButton.transform.rotation);
            _buyButton.transform.SetParent(_canvas.transform);
            _buyButton.GetComponent<RectTransform>().localScale = _buyButton.GetComponent<RectTransform>().localScale * 2;
            _buyButton.GetComponent<Button>().AddEventListener(car, BuyButtonClicked);
            _buyButton.transform.GetChild(1).GetComponent<Text>().text = car.GetPrice().ToString();
            _buyButton.SetActive(true);

            previewCarModeActive = true;
        }

        void ChooseButtonClicked(Road road)
        {
            _lobbyManager.SetRoad(road);
            _selectRoadMenu.transform.GetChild(2).GetComponent<Text>().text = road.GetName().ToString();

            GameObject listOfRoads = _selectRoadMenu.transform.parent.transform.GetChild(5).gameObject;
            listOfRoads.SetActive(false);
            listOfOpenedUIElements.Remove(listOfRoads);
        }

        void ChooseButtonClicked(GameStyle gameStyle)
        {
            _lobbyManager.SetGameMode(gameStyle);
            _selectGameModeMenu.transform.GetChild(2).GetComponent<Text>().text = gameStyle.GetName();

            GameObject listOfGameStyles = _selectGameModeMenu.transform.parent.transform.GetChild(6).gameObject;
            listOfGameStyles.SetActive(false);
            listOfOpenedUIElements.Remove(listOfGameStyles);
        }

        void ChooseButtonClicked(Car car)
        {
            if (previewCarModeActive)
                ClosePreviewMode();

            _player.GetComponent<PlayerSettings>().playerCar.SetActive(false);

            //TODO: check this stroke with Unit tests
            _player.GetComponent<PlayerSettings>().UpdatePlayerCar(car);

            _lobbyManager.SetCarName(car);
            UpdateSelectedPlayerCarInUIComponent(car);
        }

        void BuyButtonClicked(Road road)
        {
            bool transactionCompletedStatus = _lobbyManager.BuyItemFromShop(road.GetPrice());

            if (transactionCompletedStatus)
            {
                road.PurchaseRoad();

                DestroyAllRoadsUIElements();
                RefreshRoadsMenu();
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
        void BuyButtonClicked(Car car)
        {
            bool transactionCompletedStatus = _lobbyManager.BuyItemFromShop(car.GetPrice());

            if (transactionCompletedStatus)
            {
                _player.GetComponent<PlayerSettings>().playerCar = car.GetCarAsGameObject();

                Destroy(_buyButton);

                GameObject carsMenu = _selectGameModeMenu.transform.parent.transform.GetChild(8).gameObject;
                carsMenu.SetActive(false);
                listOfOpenedUIElements.Remove(carsMenu);

                car.PurchaseCar();

                ClosePreviewMode();

                UpdateMoneyScore();

                UpdateSelectedPlayerCarInUIComponent(car);
                _lobbyManager.SetCarName(car);

                _player.GetComponent<PlayerSettings>().playerCar.SetActive(true);

                DestroyAllCarsUIElements();
                RefreshCarsMenu();
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
        public void UpdateMoneyScore() => _playersStatsMenu.transform.GetChild(1).GetComponent<Text>().text = _player.GetComponent<PlayerSettings>().playerMoney.ToString();

        public void UpdateSelectedPlayerCarInUIComponent(in Car car) =>
            _carsMenuButtton.transform.GetChild(2).GetComponent<Text>().text = car.GetName().ToString();

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
            // TODO : add new logic cause if preview mode active && another window poped up it will close preview mode and last opened window
            // TODO : something like if(previewCarModeActive && listOfOpened)
            if (previewCarModeActive)
            {
                ClosePreviewMode();
            }

            listOfOpenedUIElements[listOfOpenedUIElements.Count - 1].SetActive(false);
            listOfOpenedUIElements.Remove(listOfOpenedUIElements[listOfOpenedUIElements.Count - 1]);
        }
    }
}
