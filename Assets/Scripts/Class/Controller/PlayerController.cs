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
    public Rigidbody _rb;
    public LayerMask targetMask;
    public LayerMask groundMask;

    private PlayerInput playerInput;
    public GameObject uiPanelPlayer;
    private bool clickOn;
    private bool panelOpen;

    public enum input
    {
        a,
        e,
        auto,
        click
    }


    private void Awake()
    {
        _Player = GetComponent<Player>();
        _agent = GetComponent<NavMeshAgent>();

        playerInput = new PlayerInput();
        playerInput.Enable();

        playerInput.PlayerController.Move.performed += Move;
        playerInput.PlayerController.Skill1.performed += OnUseSpell1;
        playerInput.PlayerController.Skill2.performed += OnUseSpell2;
        //playerInput.PlayerController.Skill3.performed += OnUseSpell3;
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

        /*if (clickOn)
        {
            RaycastMove();
        }*/
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

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed) // Vérifie si une entrée est en cours
        {
            // Obtenir les axes d'entrée (stick gauche ou touches ZQSD/WASD)
            Vector2 inputDirection = context.ReadValue<Vector2>();

            // Convertir en direction 3D
            Vector3 moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);

            // Identifier si c'est un clavier ou une manette
            string deviceName = context.control.device.name;
            if (deviceName.Contains("Keyboard"))
            {
                Debug.Log("Contrôleur actif : Clavier");
            }
            else if (deviceName.Contains("Gamepad"))
            {
                Debug.Log("Contrôleur actif : Manette");
            }

            // Effectuer le déplacement en fonction de la direction
            if (moveDirection.magnitude > 0.1f) // Ajouter un seuil pour éviter les mouvements parasites
            {
                moveDirection = moveDirection.normalized; // Normaliser pour éviter des vitesses irrégulières
                //_agent.Move(moveDirection * _Player.GetStats(EStats.MovementSpeed) * Time.deltaTime); // Utiliser NavMeshAgent pour se déplacer
                _Player.Move(moveDirection);
            }
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

    /*public void OnUseSpell3(InputAction.CallbackContext context)
    {
        _Player.UseSpell(input.z);
    }*/

    public void OnUseSpell4(InputAction.CallbackContext context)
    {
        _Player.UseSpell(input.e);
    }

    public void OnUseSpell5(InputAction.CallbackContext context)
    {
        _Player.UseSpell(input.auto);
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
