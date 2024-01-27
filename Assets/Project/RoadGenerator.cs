using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private Transform _roadPivot;
    [SerializeField]
    private GameObject _center;

    [SerializeField]
    private float _obstacleHeight = 1f;
    [SerializeField]
    private GameObject[] _obstacles;
    [SerializeField]
    private float _minDistanceBetweenObstacles = 4f;
    [SerializeField]
    private float _maxDistanceBetweenObstacles = 10f;
    [SerializeField]
    private float _nextObstaclePosition;
    [SerializeField]
    private float _obstacleCounter = 0;
    
    [SerializeField]
    private float _kebabHeight = 1f;
    [SerializeField]
    private GameObject[] _kebabs;
    [SerializeField]
    private float _minDistanceBetweenKebabs = 6f;
    [SerializeField]
    private float _maxDistanceBetweenKebabs = 15f;
    [SerializeField]
    private float _nextKebabPosition;
    [SerializeField]
    private float _kebabCounter = 0;
    
    [SerializeField]
    private float _minDistanceBetweenObstacleAndKebab = 2;
    
    [SerializeField]
    private float _segmentLength;
    [SerializeField]
    private float _horizonDistance = 120;
    [SerializeField]
    private float _roadWidth = 3f;
    [SerializeField] 
    private RoadGenerator _roadGenerator;
    [SerializeField] 
    private float _speed;
    [SerializeField] 
    private float _resetDistance = 20f;
    [SerializeField]
    private float _currentDistance = 0f;
    private float _furthestDistance = 0f;

    private Queue<GameObject> _segments = new Queue<GameObject>();

    public float CurrentDistance => _currentDistance;


    void Start()
    {
        _nextObstaclePosition = generateRandomNextObstaclePosition(0f);
        _nextKebabPosition = generateRandomNextKebabPosition(_nextObstaclePosition);
        
        do
        {
            CreateSegment();
        } while (_furthestDistance < _horizonDistance);
    }
    
    private void Update()
    {
        float move = Time.deltaTime * _speed;
        _currentDistance += move;
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
        _speed += speed;
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
            RandomSpawnObstacle(center, centerPosition);
            _nextObstaclePosition = generateRandomNextObstaclePosition(_nextKebabPosition);
            _obstacleCounter = 0;
        }
        else
        {
            _obstacleCounter++;
        }
        
        if (_kebabCounter > _nextKebabPosition)
        {
            RandomSpawnKebab(center, centerPosition);
            _nextKebabPosition = generateRandomNextKebabPosition(_nextObstaclePosition);
            _kebabCounter = 0;
        }
        else
        {
            _kebabCounter++;
        }
        
        _segments.Enqueue(center);
    }

    private float generateRandomNextKebabPosition(float nextObstaclePosition)
    {
        float nextKebabPosition = Random.Range(_minDistanceBetweenKebabs, _maxDistanceBetweenKebabs);
        if (nextKebabPosition == nextObstaclePosition)
        {
            nextKebabPosition += _minDistanceBetweenObstacleAndKebab;
        }

        return nextKebabPosition;
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

    private void RandomSpawnKebab(GameObject center, Vector3 centerPosition)
    {
        GameObject kebab = Instantiate(_kebabs[Random.Range(0, _kebabs.Length)]);
        kebab.transform.SetParent(center.transform);
        kebab.transform.position = centerPosition + RandomSegmentPosition();
    }

    private void RandomSpawnObstacle(GameObject center, Vector3 centerPosition)
    {
        GameObject obstacle = Instantiate(_obstacles[Random.Range(0, _obstacles.Length)]);
        obstacle.transform.SetParent( center.transform);
        obstacle.transform.position = centerPosition + RandomSegmentPosition();
    }

    private Vector3 RandomSegmentPosition()
    {
        int i = Random.Range(0, 3);
        float x = CenterXPosition(i);
        float y = _obstacleHeight;
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
