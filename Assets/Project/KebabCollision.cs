using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KebabCollision : MonoBehaviour
{
    [SerializeField]
    public GameObject fin;
    [SerializeField]
    private InteractionBounds _interactionBounds;

    void Start()
    {
        
    }

    void Update()
    {
        // if (_interactionBounds.IsWithinReach(transform.position))
        // {
        //
        // }
        if (_interactionBounds)
        {
            Debug.Log(_interactionBounds.IsWithinReach(fin.transform.position));
        }
    }
}
