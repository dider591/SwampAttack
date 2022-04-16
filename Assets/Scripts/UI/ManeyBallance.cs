using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManeyBallance : MonoBehaviour
{
    [SerializeField] private TMP_Text _maney;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _maney.text = _player.Maney.ToString();
        _player.ManeyChanged += OnManeyChanged;
    }

    private void OnDisable()
    {
        _player.ManeyChanged -= OnManeyChanged;
    }

    private void OnManeyChanged(int maney)
    {
        _maney.text = maney.ToString();
    }
}
