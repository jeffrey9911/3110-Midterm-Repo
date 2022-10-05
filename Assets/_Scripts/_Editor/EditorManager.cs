using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditorManager : MonoBehaviour
{
    public static EditorManager Instance;
    PlayerAction inputAction;

    public Camera mainCam;
    public Camera editorCamera;

    public GameObject prefab1;
    public GameObject prefab2;

    public GameObject item;

    public bool editorMode = false;

    public bool instantiated = false;

    Vector3 mousePos;

    Subject subject = new Subject();

    iCommand _icommand;

    UIManager UI;

    /*
    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }*/

    private void Start()
    {

        if(Instance == null)
        {
            Instance = this;
        }
        //inputAction = new PlayerAction();
        inputAction = PlayerInputController.controller.inputAction;


        inputAction.Editor.Enabled.performed += cntxt => SwitchCamera();
        inputAction.Editor.AddItem1.performed += cntxt => AddItem(1);
        inputAction.Editor.AddItem2.performed += cntxt => AddItem(2);
        inputAction.Editor.DropItem.performed += cntxt => Drop();

        mainCam.enabled = true;
        editorCamera.enabled = false;

        UI = GetComponent<UIManager>();
    }
    
    private void SwitchCamera()
    {
        mainCam.enabled = !mainCam.enabled;
        editorCamera.enabled = !editorCamera.enabled;

        UI.ToggleEditorUI();
    }

    private void AddItem(int itemID)
    {
        if(editorMode && !instantiated)
        {
            switch(itemID)
            {
                case 1:
                    item = Instantiate(prefab1);
                    SpikeBall spike1 = new SpikeBall(item, new GreenMat());
                    subject.AddObserver(spike1);
                    break;
                case 2:
                    item = Instantiate(prefab2);
                    SpikeBall spike2 = new SpikeBall(item, new YellowMat());
                    subject.AddObserver(spike2);
                    break;


                    default:
                    break;
            }
            subject.Notify();
            instantiated = true;
        }
    }

    private void Drop()
    {
        if(editorMode && instantiated)
        {
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<SphereCollider>().enabled = true;

            _icommand = new PlaceItemCommand(item.transform.position, item.transform);
            CommandInvoker.AddCommand(_icommand);

            instantiated = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(mainCam.enabled == false && editorCamera.enabled == true)
        {
            editorMode = true;
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            editorMode = false;
            Time.timeScale = 1.0f;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if(instantiated)
        {
            mousePos = Mouse.current.position.ReadValue();
            mousePos = new Vector3(mousePos.x, mousePos.y, 10.0f);

            item.transform.position = editorCamera.ScreenToWorldPoint(mousePos);
        }
    }
}
