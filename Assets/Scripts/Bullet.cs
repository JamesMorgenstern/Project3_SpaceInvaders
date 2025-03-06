using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //technique for making sure there isn't a null reference during runtime if you are going to use get component
public class Bullet : MonoBehaviour
{
  public System.Action destroyed;
  public Vector3 direction;
  public float speed = 5;

  private Rigidbody2D myRigidbody2D;
  
    // Start is called before the first frame update
    void Start()
    {
      myRigidbody2D = GetComponent<Rigidbody2D>();
      Fire();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
      if (destroyed != null)
      {
        destroyed.Invoke();
      }
      Destroy(gameObject);
    }

    // Update is called once per frame
    private void Fire()
    {
      myRigidbody2D.linearVelocity = direction * speed; 
      //Debug.Log("Wwweeeeee");
    }

    void Update()
    {
      if (transform.position.y >= 5.4f || transform.position.y <= -5.4f)
      {
        if (destroyed != null)
        {
          destroyed.Invoke();
        }
        Destroy(gameObject);
      }
    }
}
