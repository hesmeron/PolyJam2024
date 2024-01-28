using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] 
    private GameManager _gameManager;
    [SerializeField] 
    private ObstacleSpawner _obstacleSpawner;   
    [SerializeField] 
    private ObstacleSpawner _kebabSpawner;
    [SerializeField] private Transform _roadPivot;
    [SerializeField]
    private GameObject _center;

    [SerializeField]
    private float _minDistanceBetweenObstacles = 4f;
    [SerializeField]
    private float _maxDistanceBetweenObstacles = 10f;
    private float _nextObstaclePosition;
    [SerializeField]
    private float _obstacleCounter = 0;
    
    [SerializeField]
    private float _minDistanceBetweenObstacleAndKebab = 2;
    
    [SerializeField]
    private float _segmentLength;
    [SerializeField]
    private float _horizonDistance = 120;
    [SerializeField]
    private float _roadWidth = 3f;
    [SerializeField] 
    private float _speed;
    [SerializeField]
    private float _currentDistance = 0f;
    private float _furthestDistance = 0f;

    private Queue<GameObject> _segments = new Queue<GameObject>();


    void Start()
    {
        _nextObstaclePosition = generateRandomNextObstaclePosition(0f);
        _obstacleSpawner.OnHit += _gameManager.Lose;
        _kebabSpawner.OnHit += KebabSpawnerOnOnHit;
        
        do
        {
            CreateSegment();
        } while (_furthestDistance < _horizonDistance);
    }

    private void KebabSpawnerOnOnHit()
    {
        Debug.Log("Feed");
        _gameManager.Eat(15f);
    }

    private void Update()
    {
        float move = Time.deltaTime * _speed;
        _currentDistance += move;
        _obstacleSpawner.Move(move);
        _kebabSpawner.Move(move);
        _roadPivot.position += Vector3.forward * -move;
        if (_currentDistance > _segmentLength)
        {
            CreateSegment();
            Destroy(_segments.Dequeue());
            _currentDistance -= _segmentLength;
            foreach (GameObject segment in _segments)
            {
                segment.transform.position -= Vector3.forward * _segmentLength;
            }
            _roadPivot.position = new Vector3(0, 0, -_currentDistance);
        }

        _speed = Mathf.Lerp(_speed, 0, Time.deltaTime * 2f);
    }

    public void AddSpeed(float speed)
    {
        if (Time.timeSinceLevelLoad > 2.5f)
        {
            _speed += Mathf.Min(10, speed);
        }
    }

    public float MinX()
    {
        return -0.5f * _roadWidth;
    }    
    
    public float MaxX()
    {
        return 0.5f * _roadWidth;
    }

    private void CreateSegment()
    {
        GameObject center = Instantiate(_center);
        center.transform.SetParent(_roadPivot.transform);
        
        Vector3 centerPosition = new Vector3(0, 0, (_segments.Count * _segmentLength) - _currentDistance);
        _furthestDistance = centerPosition.z;
        center.transform.position = centerPosition;

        if (_obstacleCounter > _nextObstaclePosition)
        {
            _obstacleCounter = 0;
        }
        else
        {
            _obstacleCounter++;
        }
        
        _segments.Enqueue(center);
    }
    
    private float generateRandomNextObstaclePosition(float nextKebabPosition)
    {
        float nextObstaclePosition = Random.Range(_minDistanceBetweenObstacles, _maxDistanceBetweenObstacles);
        if (nextObstaclePosition == nextKebabPosition)
        {
            nextObstaclePosition += _minDistanceBetweenObstacleAndKebab;
        }

        return nextObstaclePosition;
    }

    public Vector3 RandomSegmentPosition(float roadObjectHeight)
    {
        int i = Random.Range(0, 3);
        float x = CenterXPosition(i);
        float y = roadObjectHeight;
        float halfZSpread = _segmentLength / 2f;
        float z = Random.Range(-halfZSpread, halfZSpread);
        return new Vector3(x,y,z);
    }

    private float CenterXPosition(int i)
    {
        float segmentWidth = _roadWidth / 3f;
        return (i-1) * segmentWidth;
    }
}
