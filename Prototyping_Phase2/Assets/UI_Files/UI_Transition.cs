using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Transition : MonoBehaviour
{
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    public void HoverOver()
    {
        Debug.Log("Hovering Over");
    }
}