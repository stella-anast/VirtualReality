using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; set; }

    public List<Quest> allActiveQuests;
    public List<Quest> allCompletedQuests;

    [Header("QuestMenu")]
    public GameObject questMenu;
    public bool isQuestMenuOpen;

    public GameObject activeQuestPrefab;
    public GameObject completedQuestPrefab;

    public GameObject questMenucontent;

    private void Start()
    {
        questMenu.SetActive(false);

    }

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

    public void AddActiveQuest(Quest quest)
    {
        allActiveQuests.Add(quest);
        RefreshQuestList();
    }

    public void MarkQuestCompleted(Quest quest)
    {
        allActiveQuests.Remove(quest);
        allCompletedQuests.Add(quest);

        RefreshQuestList();

    }

    public void RefreshQuestList()
    {
        foreach (Transform child in questMenucontent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Quest activeQuest in allActiveQuests)
        {
            GameObject questPrefab = Instantiate(activeQuestPrefab, Vector3.zero, Quaternion.identity);
            questPrefab.transform.SetParent(questMenucontent.transform, false);

            QuestRow qRow = questPrefab.GetComponent<QuestRow>();

            qRow.questName.text = activeQuest.questName;
            qRow.questGiver.text = activeQuest.questGiver;

            qRow.isActive = true;
        }
        foreach (Quest completedQuest in allCompletedQuests)
        {
            GameObject questPrefab = Instantiate(completedQuestPrefab, Vector3.zero, Quaternion.identity);
            questPrefab.transform.SetParent(questMenucontent.transform, false);

            QuestRow qRow = questPrefab.GetComponent<QuestRow>();

            qRow.questName.text = completedQuest.questName;
            qRow.questGiver.text = "Completed";
            qRow.isActive = false;
        }
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q) && !isQuestMenuOpen)
        {

            questMenu.SetActive(true);
            isQuestMenuOpen = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && isQuestMenuOpen)
        {
            questMenu.SetActive(false);
            isQuestMenuOpen = false;

            if (!InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

            }
        }
    }
}

