using TMPro;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    [SerializeField] float distance;
    
    RaycastHit hit;

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.collider.TryGetComponent(out Lever lever))
                {
                    lever.TurnLever();
                }
;
            }
        }
    }
}
