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
        
        RandomSpawnObstacle(center, centerPosition);

        // if (timeToSpawnObstacle())
        // {
        //     RandomSpawnObstacle(center, centerPosition);
        // }
        //
        // if (timeToSpawnKebab())
        // {
        //     RandomSpawnKebab(center, centerPosition);
        // }
        
        _segments.Enqueue(center);
    }
    
    // private bool timeToSpawnObstacle()
    // {
    //     
    // }
    //
    // private bool timeToSpawnKebab()
    // {
    //     
    // }

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
