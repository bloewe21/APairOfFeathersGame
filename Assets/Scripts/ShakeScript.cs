using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScript : MonoBehaviour
{
    [SerializeField] private GameObject myParent;

    [Header("Info")]
    private Vector3 _startPos;
    private float _timer;
    private Vector3 _randomPos;

    [Header("Settings")]
    [Range(0f, 2f)]
    [SerializeField] private float _time = 0.2f;
    [Range(0f, 2f)]
    [SerializeField] private float _distance = 0.1f;
    [Range(0f, 0.1f)]
    [SerializeField] private float _delayBetweenShakes = 0f;

    private void Awake()
    {
        // _startPos = myParent.transform.position;

        // Begin();
        // //StartCoroutine(ShakeRoutine());
    }

    private void OnEnable()
    {
        _startPos = myParent.transform.position;

        Begin();
        //StartCoroutine(ShakeRoutine());
    }

    private void Update()
    {
        //Begin();
    }

    private void OnValidate()
    {
        if (_delayBetweenShakes > _time)
            _delayBetweenShakes = _time;
    }

    public void Begin()
    {
        //StopAllCoroutines();
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        _timer = 0f;

        while (_timer < _time)
        {
            _timer += Time.deltaTime;

            _randomPos = _startPos + (Random.insideUnitSphere * _distance);

            myParent.transform.position = _randomPos;

            if (_delayBetweenShakes > 0f)
            {
                yield return new WaitForSeconds(_delayBetweenShakes);
            }
            else
            {
                yield return null;
            }
        }

        myParent.transform.position = _startPos;
    }

    private IEnumerator ShakeRoutine()
    {
        Begin();
        yield return new WaitForSeconds(1f);
        StopAllCoroutines();
        Begin();
        StopAllCoroutines();
    }

}
