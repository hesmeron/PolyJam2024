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

    // Update is called once per frame
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
    }

    private void CreateSegment()
    {
        GameObject center = Instantiate(_center);
        center.transform.SetParent(_roadPivot.transform);
        Vector3 centerPosition = new Vector3(0, 0, (_segments.Count * _segmentLength) - _currentDistance);
        _furthestDistance = centerPosition.z;
        center.transform.position = centerPosition;
        GameObject obstacle = Instantiate(_obstacles[Random.Range(0, _obstacles.Length)]);
        obstacle.transform.SetParent( center.transform);
        obstacle.transform.position = centerPosition + RandomSegmentPosition();
        _segments.Enqueue(center);
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
