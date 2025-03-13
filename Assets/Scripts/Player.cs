using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
  public Bullet bulletPrefab;
  public Transform shottingOffset;
  public float speed = 5f;
  public System.Action died;
  public AudioSource shootSound;
  public AudioSource deathSound;
  public GameObject shootAnimationPrefab;
  
  Animator playerAnimator;
  Rigidbody2D rb;

  private bool bulletActive;
  //private bool isDead;
  private Vector2 screenBounds;
  private float playerHalfWidth;
  

  void Start()
  {
    Enemy.OnEnemyDied += EnemyOnOnEnemyDied;
    playerAnimator = GetComponent<Animator>();
    screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    playerHalfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
  }

  void OnDestroy()
  {
    Enemy.OnEnemyDied -= EnemyOnOnEnemyDied;
  }

  void EnemyOnOnEnemyDied(int points)
  {
    //Debug.Log($"I know about dead enemy, points: {points}");
  }

  void bulletDestroyed()
  {
    bulletActive = false;
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (died != null)
    {
      died.Invoke();
      playerAnimator.SetTrigger("PlayerDied");
      deathSound.Play();
    }
    Destroy(gameObject, 0.33f);
    //call reset level function
  }

  // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        if (!bulletActive)
        {
          //playerAnimator.SetTrigger("Shoot Trigger");
          //Debug.Log("Bang!");
          Bullet shot = Instantiate(bulletPrefab, shottingOffset.position, Quaternion.identity);
          shootSound.Play();
          GameObject shootAnimationObject = Instantiate(shootAnimationPrefab, transform.position, transform.rotation, transform);
          shootAnimationObject.transform.Rotate(new Vector3(0f, 0f, -90f));
          shootAnimationObject.transform.localPosition = new Vector3(0f, 0.62f, 0f);
          Destroy(shootAnimationObject, 0.25f);
          shot.destroyed += bulletDestroyed;
          bulletActive = true;
        }
      }

      if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
      {
        transform.position += Vector3.left * (speed * Time.deltaTime);
      }

      if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
      {
        transform.position += Vector3.right * (speed * Time.deltaTime);
      }
      
      float clampedX = Mathf.Clamp(transform.position.x, -screenBounds.x + playerHalfWidth, screenBounds.x - playerHalfWidth);
      Vector2 pos = transform.position;
      pos.x = clampedX;
      transform.position = pos;
    }
}
