using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// класс родитель
public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected Player Target { get; set;}

    public void Enter(Player target)  // Запускает состояние
    {
        if(enabled == false)
        {
            Target = target; // Устанавливаем таргет
            enabled = true;  // включаем состояние

            foreach (var transition in _transitions)
            {
                transition.enabled = true;  //включаем состояние перехода или проверялки
                transition.Init(target);  //добавляет таргет в переход
            }
        }
    }

    public void Exit() // выключает все состояния
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }
            enabled = false;
        }
    }

    public State GetNextState()   // Определяем какое состояние сейчас включено и устанавливаем его в EnemyStateMashine 
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTranzit)
            {
                return transition.TargetState;
            }
        }
        return null;
    }
}
