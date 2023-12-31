using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class TankInfoManager : MonoBehaviour
{
    public static TankInfoManager tankInfoManager;
    [SerializeField] public GameObject tankBody, tankMuzzle;
    [SerializeField] public TMP_Text tankName, tankSteerSpeed, tankMoveSpeed, tankBulletSpeed;
    [SerializeField] public List<TankTemplate> tankTemplates;

    private void Awake()
    {
        tankInfoManager = this;
    }

    private void Start()
    {
        ChangeTankTo();
    }

    public void ChangeTankTo(int index = 0)
    {
        for (var i = 0; i < tankTemplates.Count; i++)
            if (i == index)
                tankTemplates[i].isSelected = true;
            else
                tankTemplates[i].isSelected = false;

        var tankTemplate = tankTemplates[index];
        tankBody.GetComponent<SpriteRenderer>().sprite = tankTemplate.tankBodySprite;
        tankMuzzle.GetComponent<SpriteRenderer>().sprite = tankTemplate.tankMuzzleSprite;
        tankName.text = tankTemplate.tankName;
        tankSteerSpeed.text = "Steering: " + tankTemplate.steerSpeed;
        tankMoveSpeed.text = "Movement: " + tankTemplate.moveSpeed;
        tankBulletSpeed.text = "Bullet Speed: " + tankTemplate.bulletSpeed;
    }
}