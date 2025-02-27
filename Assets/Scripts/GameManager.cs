using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // ���ھ� ����
    public int score;

    // ���ھ� UI
    public Text scoreText;

    // Projectile ������
    public GameObject projectilePrefab;

    // Projectile ���� ����
    public Transform topSpawner;
    public Transform bottomSpawner;

    // ���� �ֱ�
    public float spawnInterval;

    // ���� �ֱ� ���� �迭
    private float[] spawnValue = { 0, -0.1f, -0.15f };

    // Projectile �ӵ�
    public float projectileSpeed = 3f;

    // Projectile �ӵ� ���̵� ������ ���� ����
    public int speedScore;

    // ���� Ÿ�̸� ����
    private float spawnTimer = 0f;

    // ��ȯ ��ġ�� ���� ���� 1
    private float positionValue = 0.6f;

    // ��ȯ ��ġ�� ���� ���� 2
    private Vector3 valueVec;

    // ��ȯ ��ġ
    private Transform spawnTransform;

    public GameObject gameOverPanel;


    public int spawnTimeNum;
    public int spawnNumber;
    public int whereSpawnNumber;

    void Update()
    {
        scoreText.text = score.ToString();

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval + spawnValue[spawnTimeNum])
        {
            spawnTimer = 0f;
            spawnNumber = Random.Range(0, 2);
            switch(spawnNumber)
            {
                case 0:
                    OneSpawn();
                    break;

                case 1:
                    TwoSpawn();
                    break;
            }

            spawnTimeNum = Random.Range(0, 3);
        }

        ProjectSpeedUp();
        ProjectSpawnIntervalDown();
    }

    void OneSpawn()
    {
        whereSpawnNumber = Random.Range(0, 2);

        switch (whereSpawnNumber)
        {
            case 0:
                spawnTransform = topSpawner;
                valueVec = Vector3.down;
                break;

            case 1:
                spawnTransform = bottomSpawner;
                valueVec = Vector3.up;
                break;
        }

        // Instantiate the projectile at the spawner's position
        GameObject projectile = Instantiate(projectilePrefab, spawnTransform.position + valueVec * positionValue, Quaternion.identity);

        // Set the projectile's speed
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (spawnTransform == topSpawner)
            projectileScript.speed = projectileSpeed * -1;
        else
            projectileScript.speed = projectileSpeed;
    }

    void TwoSpawn()
    {
        GameObject projectile1 = Instantiate(projectilePrefab, topSpawner.position + Vector3.down * positionValue, Quaternion.identity);
        GameObject projectile2 = Instantiate(projectilePrefab, bottomSpawner.position + Vector3.up * positionValue, Quaternion.identity);

        Projectile projectileScript1 = projectile1.GetComponent<Projectile>();
        Projectile projectileScript2 = projectile2.GetComponent<Projectile>();

        projectileScript1.speed = projectileSpeed * -1;
        projectileScript2.speed = projectileSpeed;

    }

    void ProjectSpeedUp()
    {
        if(speedScore == 5)
        {
            speedScore = 0;

            if (projectileSpeed >= 8f)
                return;
            projectileSpeed += 1f;
            
        }
    }

    void ProjectSpawnIntervalDown()
    {
        if (score < 10)
            spawnInterval = 1.5f;
        else if (score >= 10 && score < 20)
            spawnInterval = 1.4f;
        else if (score >= 20 && score < 30)
            spawnInterval = 1.3f;
        else
            spawnInterval = 1.2f;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    public void ClearData()
    {
        spawnInterval = 1.5f;
        projectileSpeed = 2f;
        spawnTimer = 0f;
        score = 0;
        speedScore = 0;

        spawnTimeNum = 0;
        spawnNumber = 0;
        whereSpawnNumber = 0;
    }
}
