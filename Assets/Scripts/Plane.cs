using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Plane : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    public float force = 15f;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GameController.Instance.GameStarted.Where((value) => value).Subscribe((value) =>
        {
            rigidbody2d.isKinematic = false;
        }).AddTo(this);
        GameController.Instance.GameEnded.Where((value) => value).Subscribe((value) =>
        {
            animator.enabled = false;
        }).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space) && !GameController.Instance.GameEnded.Value)
        {
            if (!GameController.Instance.GameStarted.Value)
            {
                GameController.Instance.StartGame();
            }
            rigidbody2d.velocity = Vector2.up * force;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameController.Instance.GameEnded.Value = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Star"))
        {
            Destroy(collision.gameObject);
            GameController.Instance.Stars.Value++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ScoreIncreaseTrigger"))
        {
            GameController.Instance.Score.Value++;
        }
    }
}
