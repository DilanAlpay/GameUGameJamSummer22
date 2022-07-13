using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Interactor : MonoBehaviour
{
    public InputObj inputInteract;
    public LayerMask interactable;

    [SerializeField]
    private float range = 2;
    private List<Interactable> nearby = new List<Interactable>();
    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {

    }

    private void Update()
    {
        foreach (Interactable item in nearby)
        {
            item.SetAlert(false);
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, range, interactable);
        foreach (Collider hit in hits)
        {
            Interactable i = hit.GetComponent<Interactable>();

            if (i)
            {
                i.SetAlert(true);
                nearby.Add(i);
            }
        }
    }


    private void OnEnable()
    {
        inputInteract.Action.started += TryInteract;    
    }

    private void OnDisable()
    {
        foreach (Interactable item in nearby)
        {
            item.SetAlert(false);
        }

        if (inputInteract.Action == null) return;
        inputInteract.Action.started -= TryInteract;
    }

    private void TryInteract(InputAction.CallbackContext ctx)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range, interactable);
        foreach (Collider hit in hits)
        {
            if (hit.GetComponent<Interactable>())
            {
                hit.GetComponent<Interactable>().Interact();
            }
        }
    }
}
