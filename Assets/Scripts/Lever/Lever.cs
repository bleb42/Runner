using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private Animator _leveranim;
    public void TurnLever()
    {
        _leveranim.SetBool("IsTurned", true);
    }
}
