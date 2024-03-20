using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    private QuestManager questManager;

    void Start()
    {
        questManager = QuestManager.Instance; // Assuming you're using a singleton pattern

        if (questManager != null)
        {
            PrintActiveQuests();
        }
        else
        {
            Debug.LogWarning("QuestManager not found!");
        }
    }
    public void PrintActiveQuests()
    {
        Debug.Log("Active Quests:");
        foreach (Quest quest in allActiveQuests)
        {
            Debug.Log("Quest Name: " + quest.questName + ", Difficulty Level: " + quest.requiredDifficulty);
        }
    }
}
