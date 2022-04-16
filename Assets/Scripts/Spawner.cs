using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _wavs;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentVave;
    private int _currentWaveNumber = 0; // номер текущей волны
    private float _timerAfterLastSpawn; //время прошедшее с прошлого спавна.
    private int _spawned; // сколько уже врагов создано

    public event UnityAction AllEnemySpawned;
    public event UnityAction<int, int> EnemyCountChanged;

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        if (_currentVave == null)
        {
            return;
        }

        _timerAfterLastSpawn += Time.deltaTime;

        if (_timerAfterLastSpawn >= _currentVave.Delay)  //спаун Врага
        {
            InstantiateEnemy();
            _spawned++;
            _timerAfterLastSpawn = 0;
            EnemyCountChanged?.Invoke(_spawned, _currentVave.Count);
        }

        if (_currentVave.Count <= _spawned)  //проверка закончилась ли волна
        {
            if (_wavs.Count > _currentWaveNumber + 1)
            {
                AllEnemySpawned.Invoke();   //Сообщаем что все враги созданы для появления кнопки новой волны
            }
            _currentVave = null;
        }
    }

    public void NextWave() //следующая волна
    {
        SetWave(++_currentWaveNumber);
        _spawned = 0;
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentVave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dieng += OnEnemyDieng;
    }

    private void SetWave(int index)
    {
        _currentVave = _wavs[index];
        EnemyCountChanged?.Invoke(0, 1);
    }

    private void OnEnemyDieng(Enemy enemy)
    {
        enemy.Dieng -= OnEnemyDieng;
        _player.AddManey(enemy.Reward);
    }
}

[System.Serializable]
public class Wave
{
    public GameObject Template;
    public int Count;
    public int Delay;
}
