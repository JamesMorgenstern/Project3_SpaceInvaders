using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDied(int points);
    public static event EnemyDied OnEnemyDied;
    public static event EnemyDied AlienShipDied;
    
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
      //Debug.Log("Ouch!");
      if (!collision.gameObject.CompareTag("Player"))
      {
          if (gameObject.CompareTag("Enemy 1"))
          {
              OnEnemyDied?.Invoke(10);
          }
          else if (gameObject.CompareTag("Enemy 2"))
          {
              OnEnemyDied?.Invoke(20);
          }
          else if (gameObject.CompareTag("Enemy 3"))
          {
              OnEnemyDied?.Invoke(30);
          }
          else if (gameObject.CompareTag("Enemy 4"))
          {
              AlienShipDied?.Invoke(40);
          }
          Destroy(gameObject);
      }
    }
}
