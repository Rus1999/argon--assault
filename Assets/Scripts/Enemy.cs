using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 4;

    ScoreBoard scoreBoard;
    GameObject parentGameObject;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidbody();
    }

    private void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints < 1)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        hitPoints--;
    }

    void KillEnemy()
    {
        scoreBoard.IncreaseScore(scorePerHit);
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }
}
