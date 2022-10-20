using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Interactable
{
    public override void Interact(GameObject subject)
    {
        Player.Instance.Replenishment();  //������ ���� � ������ � ���� ���� �����.�����������
        Player.Instance.ActivateSword();  //��������� ����
        Destroy(gameObject);
    }
}
