using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard
}

[System.Serializable]
public class Quest
{
    [Header("strings")]
    public string questName;
    public string questGiver;
    public string questDescription;

    [Header("Bools")]
    public bool accepted;
    public bool declined;
    public bool initialDialogCompleted;
    public bool isCompleted;

    public bool hasNoRequirements;

    [Header("Quest Info")]
    public QuestInfo info;

    public DifficultyLevel requiredDifficulty;

}

