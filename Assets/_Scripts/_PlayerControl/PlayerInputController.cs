using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    // Singleton: Creating an instance
    public static PlayerInputController controller;

    public PlayerAction inputAction;

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    private void Awake()
    {
        if(controller == null)
        {
            controller = this;
        }

        inputAction = new PlayerAction();

    }
}
