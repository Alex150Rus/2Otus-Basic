using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

[Serializable]
public class Point
{
    public int X, Y;
}

[ExecuteAlways()]
public class CameraControl : MonoBehaviour
{
    [ContextMenuItem("Дефолтный угол", "DefaultAngle")]
    public int Angle = 90;

    [SerializeField] private Point Point;

    [HideInInspector] public Point BigPoint;
    internal Point PickPoint;

    public Transform P1, P2;

    [ContextMenu("Context")]
    public void Context()
    {
        transform.Rotate(Angle, 0, 0);
    }

    void DefaultAngle()
    {
        Angle = 90;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"{name} Start");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.Info("Информация, которя будет вырезана в релизе");
#if DEBUG
        Debug.Log($"{name} Update");
        //Log.Info("Log.Info");
        Debug.DrawLine(P1.position, P2.position, Color.red, 1);
#else

#endif
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(P1.position, 2);
    }
}

static class Log
{
    [Conditional("DEBUG")]
    public static void Info(this object o, string s) =>
        Debug.Log($"<color=#F00>{o}</color>: {s}");
}
