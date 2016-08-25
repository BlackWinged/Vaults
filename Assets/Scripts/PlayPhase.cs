using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayPhase : MonoBehaviour {
    public const string RecruitmentPhase = "Recruitment";
    public const string PlanningPhase = "Planing";
    public const string JobsPhase = "Jobs";
    public const string HideoutPhase = "Hideout";
    public const string NewTurn = "newTurn";
    private static string currentPhase;
    private List<String> phases = new List<string>();
    private static IEnumerator phaseEnum;
    private static PlayPhase playPhase;
    public static PlayPhase instance
         {
        get
        {
            if (!playPhase)
            {
                playPhase = FindObjectOfType(typeof(PlayPhase)) as PlayPhase;
            }
            if (!playPhase)
            {
                Debug.LogError("No active playphase managers loaded in scene");
            }
            return playPhase;
        }
    }
	// Use this for initialization
	void Start () {
        phases.Add(RecruitmentPhase);
        phases.Add(PlanningPhase);
        phases.Add(JobsPhase);
        phases.Add(HideoutPhase);

        phaseEnum = phases.GetEnumerator();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void newTurn()
    {
        phaseEnum.Reset();
    }

    public static string nextPhase()
    {
        if (phaseEnum.MoveNext())
        {
            currentPhase = phaseEnum.Current as string;
            return currentPhase;
        }
        phaseEnum.Reset();
        phaseEnum.MoveNext();
        currentPhase = phaseEnum.Current as string;
        return NewTurn;
    }

    public static string getCurrentPhase()
    {
        return currentPhase;
    }
   
    }


