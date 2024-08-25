﻿using System.Collections;
using UnityEngine;

public class Bow : Weapon 
{
    [SerializeField] private float _tension;
    [SerializeField] private AnimationCurve _ropeReturnAnimation;
    [SerializeField] private Vector3 _ropeNearLocalPosition;
    [SerializeField] private Transform _ropeTransform;
    [SerializeField] private Vector3 _ropeFarLocalPosition;
    [SerializeField] private Arrow _currentArrow;
    [SerializeField] private float _arrowSpeed;
    [SerializeField] private AudioSource _bowTensionAudioSource;
    [SerializeField] private AudioSource _bowWhistlingAudioSource;
    [SerializeField] private float _returnTime;
    [SerializeField] private Arrow[] _arrowsPool;
    
    private bool _pressed;
    private int ArrowIndex = 0;

    private void Start () 
    {
        _ropeNearLocalPosition = _ropeTransform.localPosition;
    }

    private void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pressed = true;

            ArrowIndex++;

            if (ArrowIndex >= _arrowsPool.Length) {
                ArrowIndex = 0;
            }

            _currentArrow = _arrowsPool[ArrowIndex];

            _currentArrow.gameObject.SetActive(true);
            _currentArrow.SetToRope(_ropeTransform);

            _bowTensionAudioSource.pitch = Random.Range(0.8f, 1.2f);
            _bowTensionAudioSource.Play();
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            _pressed = false;
            StartCoroutine(RopeReturn());
            _currentArrow.Shot(_arrowSpeed * _tension);
            _tension = 0;

            _bowTensionAudioSource.Stop();

            _bowWhistlingAudioSource.pitch = Random.Range(0.8f, 1.2f);
            _bowWhistlingAudioSource.Play();
        }

        if (_pressed) {
            if (_tension < 1f) {
                _tension += Time.deltaTime;
            }
            _ropeTransform.localPosition = Vector3.Lerp(_ropeNearLocalPosition, _ropeFarLocalPosition, _tension);
        }
    }

    private IEnumerator RopeReturn() 
    {
        Vector3 startLocalPosition = _ropeTransform.localPosition;

        for (float i = 0; i < 1f; i += Time.deltaTime / _returnTime) 
        {
            _ropeTransform.localPosition = Vector3.LerpUnclamped(startLocalPosition, _ropeNearLocalPosition, _ropeReturnAnimation.Evaluate(i));

            yield return null;
        }

        _ropeTransform.localPosition = _ropeNearLocalPosition;
    }
}