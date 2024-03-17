using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Playables;


public class InteractableNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public GameObject player;

    public string InteractionPrompt => _prompt;

    public bool isTalkingWithPlayer;
    public bool playerInRange;

    TextMeshProUGUI npcDialogText;

    Button option1Button;
    TextMeshProUGUI option1ButtonText;

    Button option2Button;
    TextMeshProUGUI option2ButtonText;

    public List<Quest> quests;
    public Quest currentActiveQuest = null;
    public int activeQuestIndex = 0;
    public bool firstTimeInteraction = true;
    public int currentDialog;


    private void Start()
    {
        npcDialogText = DialogSystem.Instance.dialogText;

        option1Button = DialogSystem.Instance.option1Button;
        option1ButtonText = DialogSystem.Instance.option1Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

        option2Button = DialogSystem.Instance.option2Button;
        option2ButtonText = DialogSystem.Instance.option2Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

    }



    public bool Interact(Interactor interactor)
    {
        StartConversation();
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }


    internal void StartConversation()
    {
        isTalkingWithPlayer = true;

        LookAtPlayer();

        // Interacting with the NPC for the first time
        if (firstTimeInteraction)
        {
            firstTimeInteraction = false;
            currentActiveQuest = quests[activeQuestIndex]; // 0 at start
            StartQuestInitialDialog();
            currentDialog = 0;
        }
        else // Interacting with the NPC after the first time
        {

            // If we return after declining the quest
            if (currentActiveQuest.declined)
            {

                DialogSystem.Instance.OpenDialogUI();

                npcDialogText.text = currentActiveQuest.info.comebackAfterDecline;

                SetAcceptAndDeclineOptions();
            }


            // If we return while the quest is still in progress
            if (currentActiveQuest.accepted && currentActiveQuest.isCompleted == false)
            {
                if (AreQuestRequirmentsCompleted())
                {

                    SubmitRequiredItems();

                    DialogSystem.Instance.OpenDialogUI();

                    npcDialogText.text = currentActiveQuest.info.comebackCompleted;

                    option1ButtonText.text = "[Take Reward]";
                    option1Button.onClick.RemoveAllListeners();
                    option1Button.onClick.AddListener(() => {
                        ReceiveRewardAndCompleteQuest();
                    });
                }
                else
                {
                    DialogSystem.Instance.OpenDialogUI();

                    npcDialogText.text = currentActiveQuest.info.comebackInProgress;

                    option1ButtonText.text = "[Close]";
                    option1Button.onClick.RemoveAllListeners();
                    option1Button.onClick.AddListener(() => {
                        DialogSystem.Instance.CloseDialogUI();
                        isTalkingWithPlayer = false;
                    });
                }
            }

            if (currentActiveQuest.isCompleted == true)
            {
                DialogSystem.Instance.OpenDialogUI();

                npcDialogText.text = currentActiveQuest.info.finalWords;

                option1ButtonText.text = "[Close]";
                option1Button.onClick.RemoveAllListeners();
                option1Button.onClick.AddListener(() => {
                    DialogSystem.Instance.CloseDialogUI();
                    isTalkingWithPlayer = false;
                });
            }

            // If there is another quest available
            if (currentActiveQuest.initialDialogCompleted == false)
            {
                StartQuestInitialDialog();
            }
            
        }

    }

    private void StartQuestInitialDialog()
    {
        DialogSystem.Instance.OpenDialogUI();

        npcDialogText.text = currentActiveQuest.info.initialDialog[currentDialog];
        option1ButtonText.text = "Next";
        option1Button.onClick.RemoveAllListeners();
        option1Button.onClick.AddListener(() => {
            currentDialog++;
            CheckIfDialogDone();
        });

        option2Button.gameObject.SetActive(false);
    }

    private void AcceptedQuest()
    {
        QuestManager.Instance.AddActiveQuest(currentActiveQuest);
        currentActiveQuest.accepted = true;
        currentActiveQuest.declined = false;

        if (currentActiveQuest.hasNoRequirements)
        {
            npcDialogText.text = currentActiveQuest.info.comebackCompleted;
            option1ButtonText.text = "[Take Reward]";
            option1Button.onClick.RemoveAllListeners();
            option1Button.onClick.AddListener(() => {
                ReceiveRewardAndCompleteQuest();
            });
            option2Button.gameObject.SetActive(false);
        }
        else
        {
            npcDialogText.text = currentActiveQuest.info.acceptAnswer;
            CloseDialogUI();
        }



    }

    private void CloseDialogUI()
    {
        option1ButtonText.text = "[Close]";
        option1Button.onClick.RemoveAllListeners();
        option1Button.onClick.AddListener(() => {
            DialogSystem.Instance.CloseDialogUI();
            isTalkingWithPlayer = false;
        });
        option2Button.gameObject.SetActive(false);
    }

    private void SubmitRequiredItems()
    {
        string firstRequiredItem = currentActiveQuest.info.firstRequirementItem;
        int firstRequiredAmount = currentActiveQuest.info.firstRequirementAmount;

        if (firstRequiredItem != "")
        {
            InventorySystem.Instance.RemoveItem(firstRequiredItem, firstRequiredAmount);
        }


        string secondtRequiredItem = currentActiveQuest.info.secondRequirementItem;
        int secondRequiredAmount = currentActiveQuest.info.secondRequirementAmount;

        if (firstRequiredItem != "")
        {
            InventorySystem.Instance.RemoveItem(secondtRequiredItem, secondRequiredAmount);
        }
       
    }

    private bool AreQuestRequirmentsCompleted()
    {
        print("Checking Requirments");

        // First Item Requirment

        string firstRequiredItem = currentActiveQuest.info.firstRequirementItem;
        int firstRequiredAmount = currentActiveQuest.info.firstRequirementAmount;

        var firstItemCounter = 0;

        foreach (string item in InventorySystem.Instance.itemList)
        {
            if (item == firstRequiredItem)
            {
                firstItemCounter++;
            }
        }

        // Second Item Requirment -- If we dont have a second item, just set it to 0

        string secondRequiredItem = currentActiveQuest.info.secondRequirementItem;
        int secondRequiredAmount = currentActiveQuest.info.secondRequirementAmount;

        var secondItemCounter = 0;

        foreach (string item in InventorySystem.Instance.itemList)
        {
            if (item == secondRequiredItem)
            {
                secondItemCounter++;
            }
        }

        if (firstItemCounter >= firstRequiredAmount && secondItemCounter >= secondRequiredAmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ReceiveRewardAndCompleteQuest()
    {
        currentActiveQuest.isCompleted = true;
        QuestManager.Instance.MarkQuestCompleted(currentActiveQuest);

        var coinsRecieved = currentActiveQuest.info.coinReward;
        print("You recieved " + coinsRecieved + " gold coins");

        if (currentActiveQuest.info.rewardItem1 != "")
        {
            InventorySystem.Instance.AddToInventory(currentActiveQuest.info.rewardItem1);
        }

        if (currentActiveQuest.info.rewardItem2 != "")
        {
            InventorySystem.Instance.AddToInventory(currentActiveQuest.info.rewardItem2);
        }

        activeQuestIndex++;

        // Start Next Quest 
        if (activeQuestIndex < quests.Count)
        {
            currentActiveQuest = quests[activeQuestIndex];
            currentDialog = 0;
            DialogSystem.Instance.CloseDialogUI();
            isTalkingWithPlayer = false;
        }
        else
        {
            DialogSystem.Instance.CloseDialogUI();
            isTalkingWithPlayer = false;
            print("No more quests");
        }

    }

    private void DeclinedQuest()
    {
        currentActiveQuest.declined = true;

        npcDialogText.text = currentActiveQuest.info.declineAnswer;
        CloseDialogUI();
    }
    public void LookAtPlayer()
    {
        var player = PlayerState.Instance.playerBody.transform;
        Vector3 direction = player.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);

        var yRotation = transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);

    }

    private void SetAcceptAndDeclineOptions()
    {
        option1ButtonText.text = currentActiveQuest.info.acceptOption;
        option1Button.onClick.RemoveAllListeners();
        option1Button.onClick.AddListener(() => {
            AcceptedQuest();
        });

        option2Button.gameObject.SetActive(true);
        option2ButtonText.text = currentActiveQuest.info.declineOption;
        option2Button.onClick.RemoveAllListeners();
        option2Button.onClick.AddListener(() => {
            DeclinedQuest();
        });
    }

    private void CheckIfDialogDone()
    {
        if (currentDialog == currentActiveQuest.info.initialDialog.Count - 1) // If its the last dialog 
        {
            npcDialogText.text = currentActiveQuest.info.initialDialog[currentDialog];

            currentActiveQuest.initialDialogCompleted = true;

            SetAcceptAndDeclineOptions();
        }
        else  // If there are more dialogs
        {
            npcDialogText.text = currentActiveQuest.info.initialDialog[currentDialog];

            option1ButtonText.text = "Next";
            option1Button.onClick.RemoveAllListeners();
            option1Button.onClick.AddListener(() => {
                currentDialog++;
                CheckIfDialogDone();
            });
        }
    }


}
