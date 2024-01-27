using UnityEngine;

public class PropellingHand : MonoBehaviour
{
    private Vector3 previousPostion;

    private void Start()
    {
        previousPostion = transform.position;
    }

    public float Evaluate()
    {
        Vector3 transition = transform.position - previousPostion;

        float velocity = 0f;
        if (transition.z > 0)
        {
            velocity = transition.sqrMagnitude / Time.deltaTime;
        }
        Reset();

        return velocity;
    }

    public void Reset()
    {
        previousPostion = transform.position;
    }
}
