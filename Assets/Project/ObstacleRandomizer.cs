using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleRandomizer : MonoBehaviour
{
    [SerializeField]
    private GameObject _obstacle1;
    [SerializeField] private GameObject _obstacle2;
    
    private List<AvailableObstacle> _availableObstacles;

    public AvailableObstacle GetRandomAvailableObstacle()
    {
        int randomObstacleNumber = Random.Range(0, _availableObstacles.Count);
        return _availableObstacles[randomObstacleNumber];
    }
    
    void Start()
    {
        _availableObstacles = new List<AvailableObstacle>()
        {
            new (_obstacle1, 1f),
            new (_obstacle2, 1f)
        };
    }
}
