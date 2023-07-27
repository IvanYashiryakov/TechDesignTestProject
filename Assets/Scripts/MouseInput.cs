using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    private Camera _camera;
    private float _clickRadius = 0.1f;
    private ContactFilter2D _contactFilter = new ContactFilter2D();
    private List<Collider2D> _colliderResults = new List<Collider2D>();

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
            CheckCollision();
    }

    private void CheckCollision()
    {
        _colliderResults.Clear();
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Physics2D.OverlapCircle(mousePosition, _clickRadius, _contactFilter, _colliderResults);

        foreach (var collider in _colliderResults)
        {
            if (collider.TryGetComponent(out IInteractible component) == true)
                component.Interact();
        }
    }
}
