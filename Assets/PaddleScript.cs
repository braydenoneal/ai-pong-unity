using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PaddleScript : Agent
{
    public GameObject ball;
    public GameObject otherPaddle;
    public float paddleMovementSpeed = 0.25f;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, 0, 0);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.y);

        sensor.AddObservation(otherPaddle.transform.localPosition.x);
        sensor.AddObservation(otherPaddle.transform.localPosition.y);

        sensor.AddObservation(ball.transform.localPosition.x);
        sensor.AddObservation(ball.transform.localPosition.y);

        Rigidbody2D ball_rb = ball.GetComponent<Rigidbody2D>();
        sensor.AddObservation(ball_rb.velocity.x);
        sensor.AddObservation(ball_rb.velocity.y);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        int direction = actionBuffers.DiscreteActions[0];

        if (direction == 1)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + paddleMovementSpeed, transform.localPosition.z);
        }
        else if (direction == 2)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - paddleMovementSpeed, transform.localPosition.z);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);

        if (collision.gameObject == ball)
        {
            SetReward(0.1f);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActionsOut = actionsOut.DiscreteActions;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            discreteActionsOut[0] = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            discreteActionsOut[0] = 2;
        }
    }
}
