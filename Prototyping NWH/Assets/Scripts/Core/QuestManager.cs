using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class QuestManager : MonoBehaviour
{
    public List<Quest_SO> questList;
    public Quest_SO currentQuest;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateObjectives(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateObjectives(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpdateObjectives(2);
        }
    }
    public void CompleteObjective(Objectives objective)
    {
        objective.complete = true;

       if(objective.complete )
        {
            Debug.Log("completed " + objective.ToString());

            Debug.Log(objective.QuestUpdate.ToString());
            Debug.Log(objective.JournalEntry.ToString());   
        }
       

        if (currentQuest.objectives.All(o => o.complete))
        {
            CompleteQuest();
        }
    }

    private void CompleteQuest()
    {
        currentQuest.isCompleted = true;
    }

    void UpdateObjectives(int index)
    {
        // Check if the index is valid
        if (index >= 0 && index < currentQuest.objectives.Count)
        {
            // Complete the objective at the specified index
            CompleteObjective(currentQuest.objectives[index]);
        }
    }
}
