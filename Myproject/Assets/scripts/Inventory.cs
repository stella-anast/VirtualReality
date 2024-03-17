using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySystem : MonoBehaviour
{

    public static InventorySystem Instance { get; set; }
    public static GameObject itemBeingClicked;


    public GameObject inventoryScreenUI;
    public GameObject slotUI;


    public List<GameObject> slotList = new List<GameObject>();
    public List<String> itemList = new List<string>();

    private GameObject whatSlotToEquip;
    private GameObject itemToAdd;

    public bool isOpen;
    //public bool isFull;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        isOpen = false;
        //isFull = false;
        inventoryScreenUI.SetActive(false);

        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in slotUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {

            inventoryScreenUI.SetActive(true);
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            isOpen = false;
        }
        else if (Input.GetMouseButtonDown(0) && isOpen) // 0 represents the left mouse button
        {
            DropItem();
        }
    }

    public void AddToInventory(string itemName)
    {

        whatSlotToEquip = FindNextEmptySlot();

        itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position,
            whatSlotToEquip.transform.rotation);

        itemToAdd.transform.SetParent(whatSlotToEquip.transform);

        itemList.Add(itemName);

        QuestManager.Instance.RefreshTrackerList();

    }
    private GameObject FindNextEmptySlot()

    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }

        return new GameObject();
    }

    public bool CheckIfFull()
    {
        int counter = 0;
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                counter += 1;
            }
            
        }
        if (counter == 24)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int CheckItemAmount(string name)
    {
        int itemCounter = 0;

        foreach (string item in itemList)
        {
            if (item == name)
            {
                itemCounter++;
            }
        }
        return itemCounter;
    }

    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        int counter = amountToRemove;

        for (var i = slotList.Count - 1; i >= 0; i--)
        {
            if (slotList[i].transform.childCount > 0)
            {
                if (slotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter != 0)
                {
                    DestroyImmediate(slotList[i].transform.GetChild(0).gameObject);
                    counter -= 1;
                    itemList.Remove(nameToRemove);
                }
            }
        }

        QuestManager.Instance.RefreshTrackerList();

    }

    public void DropItem()
    {
        var tempItemReference = gameObject;

        tempItemReference.SetActive(false);

        AlertDialog dialog = FindAnyObjectByType<AlertDialog>();
        dialog.ShowDialog("Do you want to remove this item?", (response)=>
        {
            if (response)
            {
                //DropItemIntoTheWorld(tempItemReference);
                DestroyImmediate(tempItemReference.gameObject);

            }
            else
            {
                tempItemReference.SetActive(true);
            }
        });

    }

    private void DropItemIntoTheWorld(GameObject tempItemReference)
    {
        string cleanName = tempItemReference.name.Split(new string[] { "(Clone" }, StringSplitOptions.None)[0];

        GameObject item = Instantiate(Resources.Load<GameObject>(cleanName + "_Model"));

        item.transform.position = Vector3.zero;
        var dropSpawnPosition = PlayerState.Instance.playerBody.transform.Find("DropPoint").transform.position;
        item.transform.localPosition = new Vector3(dropSpawnPosition.x, dropSpawnPosition.y, dropSpawnPosition.z);

        //var itemsObject = FindAnyObjectByType<>

        DestroyImmediate(tempItemReference.gameObject);
        //InventorySystem.Instance.ReCalc
    }
}