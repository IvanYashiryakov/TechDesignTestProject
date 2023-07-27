using Spine;
using Spine.Unity;
using UnityEngine;

public class Knight : MonoBehaviour, IInteractible
{
    [SpineAnimation] public string walkAnimationName;
    [SpineAnimation] public string hitAnimationName;

    private SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState spineAnimationState;

    private void OnEnable()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        spineAnimationState.Complete += OnComplete;
    }

    private void OnDisable()
    {
        spineAnimationState.Complete -= OnComplete;
    }

    private void OnComplete(TrackEntry trackEntry)
    {
        if (trackEntry.Loop == false && trackEntry.Next == null)
            StartWalk();
    }

    private void StartWalk()
    {
        spineAnimationState.SetAnimation(0, walkAnimationName, true);
        SignalsManager.Instance.Send(new KnightStartWalk());
    }

    private void Hit()
    {
        spineAnimationState.SetAnimation(0, hitAnimationName, false);
        SignalsManager.Instance.Send(new KnightHitted());
    }

    public void Interact()
    {
        Hit();
    }
}
