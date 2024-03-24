using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectScript : MonoBehaviour
{
    [SerializeField] private float _timerBeforeDeleted;
    void Start()
    {
        StartCoroutine(delete());
    }

   private IEnumerator delete()
    {
        yield return new WaitForSeconds(_timerBeforeDeleted);

        Destroy(gameObject);
        StopCoroutine(delete());
    }
}
