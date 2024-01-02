using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isCollected", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BowlController>())
        {
            BowlController.instance.coinCount++;
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    IEnumerator DestroyAfterAnimation()
    {
        animator.SetBool("isCollected", true);

        // Wait for the animation duration
        yield return new WaitForSecondsRealtime(
            animator.runtimeAnimatorController.animationClips[0].length);

        Destroy(gameObject);
    }
}
