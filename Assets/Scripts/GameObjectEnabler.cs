using UnityEngine;

public class GameObjectEnabler : MonoBehaviour, IInteractible
{
    [SerializeField] private GameObject _toEnable;

    public void Interact()
    {
        _toEnable.SetActive(true);
    }
}
