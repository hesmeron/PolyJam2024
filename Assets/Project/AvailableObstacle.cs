using UnityEngine;

public class AvailableObstacle
{
    public AvailableObstacle(GameObject obstacle, float height)
    {
        _obstacle = obstacle;
        _height = height;
    }

    private GameObject _obstacle;
    public GameObject Obstacle => _obstacle;

    private float _height;
    public float Height => _height;
}
