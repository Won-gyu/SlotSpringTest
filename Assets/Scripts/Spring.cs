using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    /**
    https://www.myphysicslab.com/springs/single-spring-en.html
    y = position of the block
    v = y' = velocity of the block
    mass = mass of the block
    R = rest length of the spring
    stifness = spring stiffness
    damping = damping constant (friction)

    y'' = − (k⁄m) y − (b⁄m) y'
    **/
    public float y;
    public bool active;
    [SerializeField]
    private float v;
    [SerializeField]
    private float mass;
    [SerializeField]
    private float stifness;
    [SerializeField]
    private float damping;
    [SerializeField]
    private float offset;

    private void Awake()
    {
        offset = transform.localPosition.y;
    }

    private float GetAccelration(float y, float k, float m, float b, float v)
    {
        return - (k / m) * y - (b / m) * v;
    }

    private void FixedUpdate()
    {
        if (active)
        {
            v += GetAccelration(y, stifness, mass, damping, v);
            y += v;
            transform.localPosition = Vector3.up * (y + offset);
        }
    }
}
