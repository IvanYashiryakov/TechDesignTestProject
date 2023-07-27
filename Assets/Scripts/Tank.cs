using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Tank : MonoBehaviour, IInteractible
{
    private Animator _animator;
    private AudioSource _audioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        _animator.SetBool("Shot", true);
        _audioSource.Play();
    }
}
