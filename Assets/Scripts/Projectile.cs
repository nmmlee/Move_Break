using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    void Update()
    {
        // Move the projectile in the direction of its local right vector (i.e. its own forward direction)
        transform.Translate(transform.up * speed * Time.deltaTime);
    }
}
