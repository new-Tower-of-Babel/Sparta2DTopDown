using System;
using UnityEngine;

public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer armRendrer;
    [SerializeField] private Transform armPivoit;

    [SerializeField] private SpriteRenderer characterRenderer;
    private TopDownController controller;
    private void Awake()
    {
        controller = GetComponent<TopDownController>();
    }
    private void Start()
    {
        controller.OnLookEvent +=OnAim;
    }

    private void OnAim(Vector2 direction)
    {
        RotateArm(direction);
    }

    private void RotateArm(Vector2 direction)
    {
        float rotZ = MathF.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        characterRenderer.flipX = MathF.Abs(rotZ)>90f;
        armPivoit.rotation = Quaternion.Euler(0,0,rotZ);
    }
}