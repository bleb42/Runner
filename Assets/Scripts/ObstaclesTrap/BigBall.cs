using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBall : MonoBehaviour
{
    [SerializeField] private float _firstTargetAngle = 90f; 
    [SerializeField] private float _secondTargetAngle = 180f;
    public float rotationSpeed = 2f;

    private Quaternion _firstTargetRotation;
    private Quaternion _secondTargetRotation;

    private bool _switch = false;

    void Start()
    {
        _firstTargetRotation = Quaternion.Euler(0f, 0f, _firstTargetAngle); 
        _secondTargetRotation = Quaternion.Euler(0f, 0f, _secondTargetAngle);
        StartCoroutine(RotateToFirstTarget());
    }

    private IEnumerator RotateToFirstTarget()
    {
        while (!_switch)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _firstTargetRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, _firstTargetRotation) < 0.1f)
            {
                transform.rotation = _firstTargetRotation;
                _switch = true;
                StartCoroutine(RotateToSecondTarget());
            }

            yield return null;
        }
    }

    private IEnumerator RotateToSecondTarget()
    {
        while (_switch)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _secondTargetRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, _secondTargetRotation) < 0.1f)
            {
                transform.rotation = _secondTargetRotation;
                _switch = false;
                StartCoroutine(RotateToFirstTarget());
            }

            yield return null;
        }
    }
}
