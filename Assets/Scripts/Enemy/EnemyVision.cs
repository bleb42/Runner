using UnityEngine;

public class EnemySense : MonoBehaviour
{
    
    public float viewRadius;
    public float viewAngle;

    public LayerMask targetPlayer;
    public LayerMask obstacleMask;
    public GameObject player;
    public bool IfSeenYou;
    void Start()
    {
        
    }
    public void IsSeeYou()
    {
        Vector3 playerTarget = (player.transform.position - transform.position).normalized;
        if(Vector3.Angle(transform.forward, playerTarget) < viewAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
            if(distanceToTarget < viewRadius)
            {
                if(Physics.Raycast(transform.position, playerTarget, distanceToTarget, obstacleMask) == false)
                {
                    IfSeenYou = true;
                    Debug.Log(IfSeenYou);
                }
                else
                {
                    IfSeenYou = false;
                    Debug.Log(IfSeenYou);
                }
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        IsSeeYou();
    }
}