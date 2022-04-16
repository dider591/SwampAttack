using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMashine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private Player _target;
    private State _currentState;

    public State Current => _currentState;

    private void Start()
    {
        _target = GetComponent<Enemy>().Target;
        Reset(_firstState); //вызов стартового состояния.
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();

        if (nextState != null)
            Tranzit(nextState);
    }

    private void Reset(State startState) // если установили состояние, то запускаем его.
    {
        _currentState = startState;

        if (_currentState != null)
        {
            _currentState.Enter(_target);  // включаем и передаем таргет
        }
    }

    private void Tranzit(State nextState) //переключатель состояния 
    {
        if (_currentState != null)
        {
            _currentState.Exit(); // старое выключаем
            
        }

        _currentState = nextState;  //  устанавливаем

        if (_currentState != null) 
        {
            _currentState.Enter(_target);  // новое включаем
        }
    }
}
