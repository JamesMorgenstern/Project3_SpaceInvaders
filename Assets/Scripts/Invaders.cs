using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Invaders : MonoBehaviour
{
    public Enemy[] prefabs;
    public Enemy alienShipPrefab;
    public Bullet bulletPrefab;
    public int rows = 5;
    public int columns = 11;
    public float speed = 10f;
    public float shipSpeed = 10f;
    public float attackRate = 1f;
    
    public float totalEnemies;
    private Vector3 _direction = Vector2.right;
    private float randomNumber = 0f;
    private Enemy alienShip;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Enemy.OnEnemyDied += CountOnEnemyDied;
        for (int row = 0; row < rows; row++)
        {
            float width = 1f * (columns - 1);
            float height = 1f * (rows - 1);
            Vector3 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 1f), 0f);
            for (int col = 0; col < columns; col++)
            {
                Enemy enemy = Instantiate(prefabs[row], transform);
                Vector3 position = rowPosition;
                position.x += col * 1f;
                enemy.transform.localPosition = position;
            }
        }
        totalEnemies = rows * columns;
        InvokeRepeating(nameof(Attack), attackRate, attackRate);
        InvokeRepeating(nameof(SpawnShip), 20, 15);
    }

    void OnDestroy()
    {
        Enemy.OnEnemyDied -= CountOnEnemyDied;
    }
    
    void CountOnEnemyDied(int points)
    {
        totalEnemies -= 1;
        speed += speed * 0.04f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * (speed * Time.deltaTime);
        
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform enemy in transform)
        {
            if (_direction == Vector3.right && enemy.position.x >= (rightEdge.x - 1f))
            {
                AdvanceRow();
            }
            else if (_direction == Vector3.left && enemy.position.x <= (leftEdge.x + 1f))
            {
                AdvanceRow();
            }
        }
        
        
        if (randomNumber < 0.5f && alienShip != null)
        {
            alienShip.transform.position += Vector3.left * (shipSpeed * Time.deltaTime);
            if (alienShip.transform.position.x <= (leftEdge.x - 1f))
            {
                Destroy(alienShip.gameObject);
            }
        }
        else if (randomNumber >= 0.5f && alienShip != null)
        {
            alienShip.transform.position += Vector3.right * (shipSpeed * Time.deltaTime);
            if (alienShip.transform.position.x >= (rightEdge.x + 1f))
            {
                Destroy(alienShip.gameObject);
            }
        }
    }

    void Attack()
    {
        foreach (Transform enemy in transform)
        {
            if (Random.value < (1f / totalEnemies))
            {
                Instantiate(bulletPrefab, enemy.position, Quaternion.identity);
                break;
            }
        }
    }

    void SpawnShip()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        randomNumber = Random.Range(0f, 1f);

        if (randomNumber < 0.5f)
        {
            alienShip = Instantiate(alienShipPrefab, new Vector3(rightEdge.x, 4.5f, 0f), Quaternion.identity);
        }
        else
        {
            alienShip = Instantiate(alienShipPrefab, new Vector3(leftEdge.x, 4.5f, 0f), Quaternion.identity);
        }
    }

    void AdvanceRow()
    {
        _direction.x *= -1f;
        
        Vector3 position = transform.position;
        position.y -= 0.5f;
        transform.position = position;
    }
}
