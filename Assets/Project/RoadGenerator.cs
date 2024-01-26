using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
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

    private float _furthestDistance = 0f;

    private List<GameObject> _segments = new List<GameObject>();
    void Start()
    {
        do
        {
            CreateSegment();
        } while (_furthestDistance < _horizonDistance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateSegment()
    {
        if (_segments.Count > 0)
        {
            _furthestDistance += _segmentLength;
        }
        GameObject center = Instantiate(_center);
        center.transform.SetParent(transform);
        Vector3 centerPosition = new Vector3(0, 0, _furthestDistance);
        center.transform.localPosition = centerPosition ;
        GameObject obstacle = Instantiate(_obstacles[Random.Range(0, _obstacles.Length)]);
        obstacle.transform.SetParent( center.transform);
        obstacle.transform.position = centerPosition + RandomSegmentPosition();
        _segments.Add(center);
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
