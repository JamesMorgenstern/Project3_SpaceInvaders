using UnityEngine;

public class Barricade : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }

    void Update()
    {
        if (transform.localScale.x <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
