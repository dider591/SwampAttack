using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTranzition : Transition
{
    [SerializeField] private float _trasitionRange;
    [SerializeField] private float _rangetSpreed;

    private void Start()
    {
        _trasitionRange += Random.Range(-_rangetSpreed, _rangetSpreed);
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) < _trasitionRange)
        {
            NeedTranzit = true; 
        }
    }
}
