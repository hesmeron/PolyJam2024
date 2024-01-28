 using System;
 using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    public event Action OnHit;
    [SerializeField] 
    private float _height = 1f;
    [SerializeField] 
    private bool _destroyOnTouch = false;
    [SerializeField]
    private RoadGenerator _roadGenerator;
    [SerializeField] 
    private Transform _playerTransaform;

    [SerializeField] 
    private InteractionBounds[] prefabs;

    private List<InteractionBounds> _instances = new List<InteractionBounds>();

    [SerializeField]
    private Vector2 _spawnRange = Vector2.zero;
    private float _nextSpawnRange = 0f;
    [SerializeField]
    float _distancePassed = 0f;
    
    public void Move(float distance)
    {
        _distancePassed += distance;
        if (_distancePassed > _nextSpawnRange)
        {
            SpawnObstacle();
        }
        foreach (InteractionBounds instance in _instances)
        {
            instance.transform.position += new Vector3(0, 0, -distance);
            if (instance.IsWithinReach(_playerTransaform.position))
            {
                _instances.Remove(instance);
                if (_destroyOnTouch)
                {
                    instance.transform.DOScale(Vector3.zero, 0.7f).onComplete +=
                        () => { Destroy(instance.gameObject); };
                }
                OnHit?.Invoke();
            }
        }
    }

    private void SpawnObstacle()
    {
        _nextSpawnRange = Random.Range(_spawnRange.x, _spawnRange.y);
        _distancePassed = 0f;
        Vector3 position = _roadGenerator.RandomSegmentPosition(0f);
        InteractionBounds instance = Instantiate(prefabs[Random.Range(0, prefabs.Length)]);
        instance.transform.position = position + new Vector3(0,_height, 75);
        _instances.Add(instance);
    }
}
