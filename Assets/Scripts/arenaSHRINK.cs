using UnityEngine;

public class ArenaShrink : MonoBehaviour
{
    public float shrinkSpeed = 0.05f;
    public float minSize = 1.5f;

    void Update()
    {
        if (transform.localScale.x > minSize)
        {
            float shrink = shrinkSpeed * Time.deltaTime;
            transform.localScale -= new Vector3(shrink, 0f, shrink);
        }
    }
}
