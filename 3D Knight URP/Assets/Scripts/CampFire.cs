using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : Interactable
{
    public override void Interact(GameObject subject)    //����������� ����������� ����� �� Interactable
    {
        Player.Instance.Heal();
        Player.Instance.Replenishment();
    }
}
