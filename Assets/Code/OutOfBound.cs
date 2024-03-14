using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBound : MonoBehaviour
{
    private Animator animator;
    void OnCollisionEnter2D(Collision2D other)
    {
        animator = BowlController.instance.getAnimator();
        animator.SetBool("IsBroken", true);
        StartCoroutine(PlayAnimationThenChangeScene());
    }

    IEnumerator PlayAnimationThenChangeScene()
    {
        BowlController.instance.stopBowl();

        // Wait for animation to start
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Broken_Idle") &&
                                           animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0f);

        // Wait until the animation is complete
        yield return new WaitForSeconds(1f);

        BowlController.instance.gameOver();
    }
}