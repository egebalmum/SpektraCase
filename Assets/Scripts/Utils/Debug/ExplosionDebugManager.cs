using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDebugManager : MonoBehaviour
{
    private static ExplosionDebugManager _instance;
    private List<ExplosionDebugInfo> _explosions = new List<ExplosionDebugInfo>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (_explosions.Count > 0)
        {
            foreach (var explosion in _explosions)
            {
                // Draw a wireframe sphere with a more visually appealing look
                Gizmos.color = explosion.Color;
                DrawWireframeSphere(explosion.Position, explosion.Radius);
            }
        }
    }

    public static void RegisterExplosion(Vector3 position, float radius, Color color)
    {
        if (_instance != null)
        {
            _instance.StartCoroutine(_instance.ShowExplosionGizmo(position, radius, color));
        }
    }

    private IEnumerator ShowExplosionGizmo(Vector3 position, float radius, Color color)
    {
        _explosions.Add(new ExplosionDebugInfo(position, radius, color));
        yield return new WaitForSeconds(0.2f);
        _explosions.RemoveAll(explosion => explosion.Position == position && explosion.Radius == radius);
    }

    private void DrawWireframeSphere(Vector3 position, float radius)
    {
        float step = 10f;
        for (float theta = 0; theta < 360; theta += step)
        {
            for (float phi = 0; phi < 360; phi += step)
            {
                Vector3 start = SphericalToCartesian(radius, theta, phi);
                Vector3 endTheta = SphericalToCartesian(radius, theta + step, phi);
                Vector3 endPhi = SphericalToCartesian(radius, theta, phi + step);

                Gizmos.DrawLine(position + start, position + endTheta);
                Gizmos.DrawLine(position + start, position + endPhi);
            }
        }
    }

    private Vector3 SphericalToCartesian(float radius, float theta, float phi)
    {
        float radTheta = Mathf.Deg2Rad * theta;
        float radPhi = Mathf.Deg2Rad * phi;
        float x = radius * Mathf.Sin(radTheta) * Mathf.Cos(radPhi);
        float y = radius * Mathf.Sin(radTheta) * Mathf.Sin(radPhi);
        float z = radius * Mathf.Cos(radTheta);
        return new Vector3(x, y, z);
    }

    private class ExplosionDebugInfo
    {
        public Vector3 Position { get; }
        public float Radius { get; }
        public Color Color { get; }

        public ExplosionDebugInfo(Vector3 position, float radius, Color color)
        {
            Position = position;
            Radius = radius;
            Color = color;
        }
    }
}
