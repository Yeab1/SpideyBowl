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
            Debug.Log("here");
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    IEnumerator DestroyAfterAnimation()
    {
        animator.SetBool("isCollected", true);
        Debug.Log("starting animation");

        // Wait for the animation duration
        yield return new WaitForSecondsRealtime(animator.runtimeAnimatorController.animationClips[0].length);

        Debug.Log("Destroying");
        Destroy(gameObject);
    }
}
