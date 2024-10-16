using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject leftPaddle;
    public GameObject rightPaddle;
    public float speed;
    // RaycastHit hit;

    public void Reset()
    {
        transform.localPosition = new Vector3(8, 0, 0);
        speed = 15.0f;
        Vector2 direction = Random.insideUnitCircle.normalized;
        rb.velocity = direction * speed;
    }

    void Start()
    {
        Reset();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rb.velocity);
        rb.velocity *= hit.normal;

        if (collision.gameObject == leftPaddle)
        {
            Vector2 distance = (transform.localPosition - leftPaddle.transform.localPosition).normalized;
            rb.velocity = distance * 10.0f;
        }
        else if (collision.gameObject == rightPaddle)
        {
            Vector2 distance = (transform.localPosition - rightPaddle.transform.localPosition).normalized;
            rb.velocity = distance * 10.0f;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        speed *= 1.05f;
        rb.velocity = rb.velocity.normalized * speed;
    }
}
