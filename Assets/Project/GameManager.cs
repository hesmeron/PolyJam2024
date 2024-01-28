using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private float _foodDecaySpeed;
    [FormerlySerializedAs("foodEaten")] [SerializeField] 
    private float _foodEaten = 50f;
    [SerializeField] 
    private Image _losePanel;

    private void Update()
    {
        _foodEaten -= Time.deltaTime * _foodDecaySpeed;
        if (_foodEaten <= 0)
        {
            Lose();
        }
    }

    public void Lose()
    {
        _losePanel.gameObject.SetActive(true);
        _losePanel.DOFade(1f, 0.7f);
    }

    public void Eat(float foodAmount = 0f)
    {
        _foodEaten = Mathf.Clamp(_foodEaten + foodAmount, 0, 100f);
    }

    public float _FedPercentage()
    {
        return _foodEaten / 100f;
    }
}
