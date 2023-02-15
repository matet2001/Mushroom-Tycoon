using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public bool hasSeenTutorial = false;
    [Space] [SerializeField] private GameObject tutorialField;
    private Animator tutorialManagerAnimator;

    private void Start()
    {
        tutorialManagerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        tutorialManagerAnimator.SetBool("hasFinishedTutorial", hasSeenTutorial);
    }

    public void TurnOffGO()
    {
        gameObject.SetActive(false);
    }
}