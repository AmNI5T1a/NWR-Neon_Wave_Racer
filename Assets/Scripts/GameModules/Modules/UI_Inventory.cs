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
        [Space(10)]
        [Header("RoadMenuUI Elements: ")]
        [SerializeField] private GameObject _roadSlotContainer;
        [SerializeField] private GameObject _roadSlotTemplate;
        [Space(10)]
        [Header("GameStylesUI Elements")]
        [SerializeField] private GameObject _gameStyleSlotContainer;
        [SerializeField] private GameObject _gameStyleSlotTemplate;

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
                    // Create slot template
                    GameObject slot = Instantiate(_roadSlotTemplate, _roadSlotContainer.transform);

                    // SetPrice
                    if (item.boughtStatus == false)
                    {
                        slot.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<Text>().text = item.price.ToString();
                    }
                    else if (item.boughtStatus == true)
                    {
                        slot.transform.GetChild(4).gameObject.SetActive(false);
                        slot.transform.GetChild(5).gameObject.SetActive(true);
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


                    _listOfInstanciatedUIElements.Add(slot);
                }
            }
        }
    }
}
