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

    [Header("QuestTracker")]
    public GameObject questTrackerContent;
    public GameObject trackerRowPrefab;

    public List<Quest> allTrackedQuests;

    public string currentDifficultyLevel;
    public string requiredDifficulty;



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

    public void TrackQuest(Quest quest)
    {
        allTrackedQuests.Add(quest);
        RefreshTrackerList();
    }

    public void UnTrackQuest(Quest quest)
    {
        allTrackedQuests.Remove(quest);
        RefreshTrackerList();
    }

    public void AddActiveQuest(Quest quest)
    {

        currentDifficultyLevel = mainMenu.Instance.gameDifficulty;

        Debug.Log("heloo"+ currentDifficultyLevel);

        if (quest.requiredDifficulty.Equals(currentDifficultyLevel))
        {
            allActiveQuests.Add(quest);
            TrackQuest(quest);
            RefreshQuestList();
        }
        else
        {
            Debug.Log("This quest is not available at the current difficulty level.");
        }
    }

    public void MarkQuestCompleted(Quest quest)
    {
        allActiveQuests.Remove(quest);
        allCompletedQuests.Add(quest);
        UnTrackQuest(quest);


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

    public void RefreshTrackerList()
    {
        // Destroying the previous list
        foreach (Transform child in questTrackerContent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Quest trackedQuest in allTrackedQuests)
        {
            GameObject trackerPrefab = Instantiate(trackerRowPrefab, Vector3.zero, Quaternion.identity);
            trackerPrefab.transform.SetParent(questTrackerContent.transform, false);

            TrackerRow tRow = trackerPrefab.GetComponent<TrackerRow>();

            tRow.questName.text = trackedQuest.questName;
            tRow.description.text = trackedQuest.questDescription;

            var req1 = trackedQuest.info.firstRequirementItem;
            var req1Amount = trackedQuest.info.firstRequirementAmount;
            var req2 = trackedQuest.info.secondRequirementItem;
            var req2Amount = trackedQuest.info.secondRequirementAmount;
            var req3 = trackedQuest.info.thirdRequirementItem;
            var req3Amount = trackedQuest.info.thirdRequirementAmount;


            if (trackedQuest.info.secondRequirementItem != "") // if we have 2 requirements
            {
                tRow.requirements.text = $"{req1} " + InventorySystem.Instance.CheckItemAmount(req1) + "/" + $" {req1Amount}\n" +
               $"{req2} " + InventorySystem.Instance.CheckItemAmount(req2) + "/" + $" {req2Amount}\n";
            }
            else if (trackedQuest.info.thirdRequirementItem != "") // if we have 3 requirements
            {
                tRow.requirements.text = $"{req1} " + InventorySystem.Instance.CheckItemAmount(req1) + "/" + $" {req1Amount}\n" +
                $"{req2} " + InventorySystem.Instance.CheckItemAmount(req2) + "/" + $" {req2Amount}\n" +
                $"{req3} " + InventorySystem.Instance.CheckItemAmount(req3) + "/" + $" {req3Amount}\n";
            }
            else // if we only have one
            {
                tRow.requirements.text = $"{req1} " + InventorySystem.Instance.CheckItemAmount(req1) + "/" + $" {req1Amount}\n";
            }


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

