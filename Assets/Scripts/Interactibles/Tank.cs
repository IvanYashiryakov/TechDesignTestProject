using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Tank : MonoBehaviour, IInteractible
{
    [SerializeField] private Explosion _explosionPrefab;
    [SerializeField] private Transform _explosionPoint;

    private Animator _animator;
    private AudioSource _audioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Shot()
    {
        Explosion explosion = Instantiate(_explosionPrefab);
        explosion.transform.position = _explosionPoint.position;
    }

    public void Interact()
    {
        _animator.SetBool("Shot", true);
        Shot();
        _audioSource.Play();
    }
}
