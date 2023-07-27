using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _transforms;
    [SerializeField] private float _speed = 1;
    [SerializeField] private bool _stopOnKnightHit;

    private bool _stopParallaxAnimation = false;
    private float _width;

    private void Start()
    {
        SignalsManager.Instance.AddListener<KnightHitted>(OnKinightHitted);
        SignalsManager.Instance.AddListener<KnightStartWalk>(OnKnightStartWalk);

        _width = _transforms[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        for (int i = 0; i < _transforms.Length; i++)
            _transforms[i].localPosition = new Vector3(_width * i, 0, 0);
    }

    private void Update()
    {
        if (_stopOnKnightHit == true && _stopParallaxAnimation == true)
            return;

        foreach (var transform in _transforms)
        {
            transform.localPosition = new Vector3(transform.localPosition.x - _speed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);

            if (transform.localPosition.x < -_width)
                transform.localPosition = new Vector3(transform.localPosition.x + _width * _transforms.Length, transform.localPosition.y, transform.localPosition.z);

            if (transform.localPosition.x > _width)
                transform.localPosition = new Vector3(transform.localPosition.x - _width * _transforms.Length, transform.localPosition.y, transform.localPosition.z);
        }
    }

    private void OnKinightHitted(KnightHitted signal)
    {
        _stopParallaxAnimation = true;
    }

    private void OnKnightStartWalk(KnightStartWalk signal)
    {
        _stopParallaxAnimation = false;
    }
}
