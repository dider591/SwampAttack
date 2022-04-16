using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWave : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Button _newWaveButton;

    private void OnEnable()
    {
        _spawner.AllEnemySpawned += OnAllEnemySpawned;
        _newWaveButton.onClick.AddListener(OnWaveButtonClick);
    }

    private void OnDisable()
    {
        _spawner.AllEnemySpawned -= OnAllEnemySpawned;
        _newWaveButton.onClick.RemoveListener(OnWaveButtonClick);
    }

    public void OnAllEnemySpawned()
    {
        _newWaveButton.gameObject.SetActive(true);   
    }

    public void OnWaveButtonClick()
    {
        _spawner.NextWave(); 
        _newWaveButton.gameObject.SetActive(false); 
    }
}
