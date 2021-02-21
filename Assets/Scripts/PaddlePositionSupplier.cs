using System;
using UnityEngine;

public interface PaddlePositionSupplier
{
    float X { get; }


}

public class PaddlePositionSupplierFactory
{
    public static PaddlePositionSupplier fromMouse(float screenWidthInUnits)
    {
        float paddleDemiLength = UnityEngine.Object.FindObjectOfType<Paddle>().GetComponent<Renderer>().bounds.size.x / 2;
        return new MousePaddlePositionSupplier(screenWidthInUnits, paddleDemiLength);
    }

    public static PaddlePositionSupplier fromBall()
    {
        float paddleDemiLength = UnityEngine.Object.FindObjectOfType<Paddle>().GetComponent<Renderer>().bounds.size.x / 2;
        return new BallPaddlePositionSupplier(UnityEngine.Object.FindObjectOfType<GameSession>().GetBall, paddleDemiLength);
    }
}

public class MousePaddlePositionSupplier : PaddlePositionSupplier
{
    private readonly float screenWidthInUnits;
    private readonly float paddleDemiLength;

    public MousePaddlePositionSupplier(float screenWidthInUnits, float paddleDemiLength)
    {
        this.screenWidthInUnits = screenWidthInUnits;
        this.paddleDemiLength = paddleDemiLength;
    }

    public float X => Mathf.Clamp(screenWidthInUnits * Input.mousePosition.x / Screen.width, paddleDemiLength, screenWidthInUnits - paddleDemiLength);
}

public class BallPaddlePositionSupplier : PaddlePositionSupplier
{

    private readonly Func<Ball> ballSupplier;
    private readonly float paddleDemiLength;

    public BallPaddlePositionSupplier(Func<Ball> ballSupplier, float paddleDemiLength)
    {
        this.ballSupplier = ballSupplier;
        this.paddleDemiLength = paddleDemiLength;
    }

    public float X => ballSupplier().transform.position.x - paddleDemiLength;
}