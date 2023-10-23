using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownController : MonoBehaviour
{
    [SerializeField] private AudioSource emptyGameObject;
    [SerializeField] private TMP_Dropdown dropdown;

    public void HandleDropdownValue()
    {
        int val=dropdown.value;
        emptyGameObject.Play();
        Debug.Log(val + " Selected");
        TankInfoManager.tankInfoManager.ChangeTankTo(val);
    }
}