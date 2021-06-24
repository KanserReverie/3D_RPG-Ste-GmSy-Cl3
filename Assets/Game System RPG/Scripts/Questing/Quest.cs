using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public enum QuestStage
    {
        Locked, // We can't actually get the Quest.
        Unlocked, // The Quest is now accepted.
        InProgress, // We have accepted the quest.
        RequirementMet, // We have done all the things we just need to hand it in.
        Complete // The Quest is done and we can ignore it.
    }
    [System.Serializable]
    // Can do with class but showing the difference between the 2.
    public abstract class Quest : MonoBehaviour
    {
        public string title;
        [TextArea] public string desciption;
        public QuestReward reward;

        public QuestStage stage;

        public int requiredLevel;
        [Tooltip("The title of the previous quest in the chain.")]
        public string previousQuest;
        [Tooltip("The title of the quests to be unlocked.")]
        public string[] unlockedQuests;

        public abstract bool CheckQuestCompletion();
    }

    [System.Serializable]
    // Can do with class but showing the difference between the 2.
    public struct QuestReward
    {
        public float experience;
        public int gold;
    }
}