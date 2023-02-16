using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DeathHandler : MonoBehaviour
{
    private Animator anim;
    private bool hasDied = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("hasDied", hasDied);
    }

    public void SetHandle(bool newHandle)
    {
        hasDied = newHandle;
    }
    public void Die()
    {
        hasDied = true;
        GameStateController.Instance.ChangeToDeadState();
    }
}