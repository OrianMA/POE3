using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{    
    public Player _Player;
    public NavMeshAgent _agent;
    public LayerMask targetMask;
    public LayerMask groundMask;

    private PlayerInput playerInput;
    public GameObject uiPanelPlayer;
    private bool clickOn;
    private bool panelOpen;

    public enum input
    {
        a,
        z,
        e,
        r,
        click
    }


    private void Awake()
    {
        _Player = GetComponent<Player>();
        _agent = GetComponent<NavMeshAgent>();

        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.PlayerController.Skill1.performed += OnUseSpell1;
        playerInput.PlayerController.Skill2.performed += OnUseSpell2;
        playerInput.PlayerController.Skill3.performed += OnUseSpell3;
        playerInput.PlayerController.Skill4.performed += OnUseSpell4;
        playerInput.PlayerController.Skill5.performed += OnUseSpell5;
        playerInput.PlayerController.OpenPlayerPanel.started += OnTab;
    }

    private void Update()
    {
        //#region Gestion Anim
        //Animator PlayerAnim = _Player.visual.GetComponent<Animator>();
        //if (!_agent.pathPending)
        //{
        //    if (_agent.remainingDistance <= _agent.stoppingDistance)
        //    {
        //        if ((!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f )&& !PlayerAnim.GetBool("Idle"))
        //        {
        //            PlayerAnim.GetComponent<Animator>().SetBool("Idle",true);                    
        //        }
        //    }
        //}
        //#endregion


        RaycastTarget();

        if (clickOn)
        {
            RaycastMove();
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if(context.started) 
        {
            clickOn = true;
        }

        if(context.performed)
        {
            RaycastMove();
        }

        if (context.canceled)
        {
            clickOn = false;
        }
    }

    public void OnTab(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(panelOpen)
            {
                uiPanelPlayer.SetActive(false);
                panelOpen = false;
            }
            else
            {
                uiPanelPlayer.SetActive(true);
                panelOpen= true;
            }
        }        
    }

    public void OnUseSpell1(InputAction.CallbackContext context)
    {

        _Player.UseSpell(input.click);
    }

    public void OnUseSpell2(InputAction.CallbackContext context)
    {
        _Player.UseSpell(input.a);
    }

    public void OnUseSpell3(InputAction.CallbackContext context)
    {
        _Player.UseSpell(input.z);
    }

    public void OnUseSpell4(InputAction.CallbackContext context)
    {
        _Player.UseSpell(input.e);
    }

    public void OnUseSpell5(InputAction.CallbackContext context)
    {
        _Player.UseSpell(input.r);
    }

    private void RaycastMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, groundMask))
        {
            _Player.Move(hit.point);
        }
    }

    private void RaycastTarget()
    {
        //Debug.Log("Click");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, targetMask))
        {
            if (hit.transform.TryGetComponent<LootScript>(out LootScript loot))
            {
                _Player.lootOnMouse = loot;
            }
            else
            {
                _Player.lootOnMouse = null;
            }
            

            if (hit.transform.TryGetComponent<AEntity>(out AEntity entity))
            {
                _Player.target = entity;
            }
            else
            {
                _Player.target = null;
            }
        }
        else
        {
            _Player.target = null;
            _Player.lootOnMouse = null;
        }
    }

    
}
