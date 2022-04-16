using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// класс родитель
public class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    
    protected Player Target { get; private set; } 
    public State TargetState => _targetState;
    public bool NeedTranzit { get; protected set; }  //флаг нужен ли переход

    public void Init(Player target) // Поля заполняются скриптом State
    {
        Target = target;
    }

    public void OnEnable()
    {
        NeedTranzit = false;
    }
}
