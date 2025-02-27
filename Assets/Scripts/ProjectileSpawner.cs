using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameManager gameManager;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            gameManager.GameOver();
        }
    }

}