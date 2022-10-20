using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Player : Character
{
    private Camera mainCamera;
    [SerializeField] private LayerMask movableMask;
    [SerializeField] private Interactable focus;

    public static Player Instance;   //�������� ��������(�������) - ������ ���� ������������

    [SerializeField] private GameObject swordObject;
    [SerializeField] private GameObject shieldObject;
    [SerializeField] private int coinsCount;
    float multipliedMaxHealth = 2f;    //������������ �������� ��������� �� 2
  

    private void Awake()
    {
        mainCamera = Camera.main;
        Instance = this;  //�������� �� ������ ������.this-������
        canAttack = false;
        transform.position = SaveSystem.GetPlayerPosition();  
    }


    protected override void Update()
    {
        base.Update();    //��� ������ � Interactable � Update ������ ����� � ���
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, movableMask))
            {
                motor.MoveToPoint(hit.point);
                RemoveFocus();
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                var interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
            SaveSystem.SetPlayerPosition(transform.position);
    }

    private void SetFocus(Interactable newFocus)  //������ ������� �����, ���� � �������
    {
        if(newFocus != focus)  //���� �� ��������������� � ����� �������� � ������ ����������������� � ������,�� � ������� 
                                // �� ��� �� ���������������
        {
            if (focus != null)
                focus.OnDeFocused();

            focus = newFocus;    
            motor.FollowTarget(newFocus);

        }
        newFocus.OnFocused(gameObject);
    }

    private void RemoveFocus()  //����� ������� �����, ���� �� ��������� �����
    {
        if (focus != null)
            focus.OnDeFocused();

        focus = null;
        motor.StopFollowingTarget();  //��������� ��������� �� ����� �����
    }

    public void Heal()
    {
        currentHealth = maxHealth;
    }

    public void Replenishment() //�����������
    {
        motor.StartReplenishment();
    }

    public void ActivateSword()  // ��� ���������� ��������� ����
    {
            canAttack = true;  // ��� ������� ����������, ���� ����� ��� ���
            swordObject.SetActive(true);   //��������� ����
    }

    public void ActivateShield()
    {
        maxHealth *= multipliedMaxHealth;
        shieldObject.SetActive(true);
    }

    protected override void Die()
    {
        Time.timeScale = 0;   //����� ������ �� ������������� �����
        animator.SetAnimState(AgentAnimator.AnimStates.Death);
    }

    //public void ActivateSpear()
    // {
    // if (hand_freeLeft == true)
    //     spearObject.SetActive(true);
    // else
    //   spearObject.SetActive(false);
    // }

    public void AddCoins(int coins)
    {
        coinsCount += coins;
        print($"�� �������� {coinsCount} ������");
    }
}
