using UnityEngine;

public class Bird : MonoBehaviour, IInteractible
{
    [SerializeField] private Explosion _explosionPrefab;

    public void Interact()
    {
        Explosion explosion = Instantiate(_explosionPrefab);
        explosion.transform.position = new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z);
        Destroy(gameObject);
    }
}
