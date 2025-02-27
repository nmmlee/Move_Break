using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private Vector3 mouseStartPosition;
    private Vector3 objectStartPosition;

    public float movementSpeed = 5f;
    public float topBoundary = 3.5f;
    public float bottomBoundary = -3.5f;
    public float horizontalSpeed = 10f;
    public float stopDistance = 0.01f;

    public bool isDragging = false;
    public bool isMoving = false;

    public GameManager gameManager;
    public int prevScore, nextScore;

    void Start()
    {
        objectStartPosition = transform.position;
    }

    void Update()
    {
        if (!isMoving && Input.GetMouseButtonDown(0))
        {
            mouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseStartPosition.z = 0;
            objectStartPosition = transform.position;
            isDragging = true;
        }

        if (isDragging)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mouseDelta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseStartPosition;
                mouseDelta.z = 0;
                Vector3 movementVector = new Vector3(0f, Input.GetAxisRaw("Mouse Y"), 0f);
                float newYPos = Mathf.Clamp(transform.position.y + (movementVector.y * movementSpeed * Time.deltaTime * 5f), bottomBoundary, topBoundary);
                transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                Vector3 targetPosition = new Vector3(transform.position.x * -1, transform.position.y, transform.position.z);
                StartCoroutine(MoveObject(targetPosition));
                prevScore = gameManager.score;
            }
        }

        ScoreScale();

    }

    IEnumerator MoveObject(Vector3 targetPosition)
    {
        isMoving = true;
        float remainingDistance = Vector3.Distance(transform.position, targetPosition);
        float moveDistance = horizontalSpeed * Time.deltaTime;

        while (remainingDistance > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveDistance);
            remainingDistance = Vector3.Distance(transform.position, targetPosition);
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
        nextScore = gameManager.score;

        if(nextScore == prevScore)
        {
            gameManager.GameOver();
        }
            
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            gameManager.score += 1;
            gameManager.speedScore += 1;

            Destroy(collision.gameObject);
        }
    }

    void ScoreScale()
    {
        if (gameManager.score < 10)
            transform.localScale = new Vector3(transform.localScale.x, 0.7f, transform.localScale.z);
        else if (gameManager.score >= 10 && gameManager.score < 20)
            transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
        else if (gameManager.score >= 20 && gameManager.score < 30)
            transform.localScale = new Vector3(transform.localScale.x, 0.3f, transform.localScale.z);
        else
            transform.localScale = new Vector3(transform.localScale.x, 0.2f, transform.localScale.z);
    }
}
