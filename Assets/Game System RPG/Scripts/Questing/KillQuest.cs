using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public class KillQuest : Quest
    {
        public int requiedKills;
        public int killCount;
        // Some enemy type.

        public override bool CheckQuestCompletion()
        {
            // Add to the kill count.
            return killCount == requiedKills;
        }
    }
}