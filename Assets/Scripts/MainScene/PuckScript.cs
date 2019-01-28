﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckScript : MonoBehaviour {

    public ScoreScript ScoreScriptInstance;
    public static bool WasGoal { get; private set; }
    private Rigidbody2D rb;
    public float MaxSpeed;

    public Transform particlePrefab;

    public AudioManager audioManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        WasGoal = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!WasGoal) {
            if (other.tag == "AiGoal") {
                ScoreScriptInstance.Increment(ScoreScript.Score.PlayerScore);
                WasGoal = true;
                audioManager.PlayGoal();
                StartCoroutine(ResetPuck(false));
            } else if (other.tag == "PlayerGoal") {
                ScoreScriptInstance.Increment(ScoreScript.Score.AiScore);
                WasGoal = true;
                audioManager.PlayGoal();
                StartCoroutine(ResetPuck(true));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioManager.PlayPuckCollision();

        if (collision.gameObject.CompareTag("Wall")) {
            StartCoroutine(destroyCircleParticle());
        }

        

    }

    IEnumerator destroyCircleParticle()
    {

        Transform localParticle = Instantiate(particlePrefab,transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);

        Destroy(localParticle.gameObject);


    }

    public void CenterPack() {
        rb.position = Vector2.zero;
    }

    private IEnumerator ResetPuck(bool didAiScore) {
        yield return new WaitForSecondsRealtime(1);
        WasGoal = false;
        rb.velocity = rb.position = Vector2.zero;

        if (didAiScore)
        {
            rb.position = new Vector2(0, -1);
        }
        else {
            rb.position = new Vector2(0,1);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
    }
}
