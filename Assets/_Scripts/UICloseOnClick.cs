using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICloseOnClick : MonoBehaviour
{
    public GameObject _WinUI;

    public void CloseScoreBoard_BtnOnClick()
    {
        _WinUI.SetActive(false);
    }
}
