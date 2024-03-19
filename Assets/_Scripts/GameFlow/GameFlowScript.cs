using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowScript : MonoBehaviour
{
    [SerializeField] private LayerMask _FruitLayers;

    [SerializeField] private float _timeToLose;

    private bool _FruitInDangerZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_FruitLayers.value & (1 << collision.gameObject.layer)) > 0)
        {
            _FruitInDangerZone = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((_FruitLayers.value & (1 << collision.gameObject.layer)) > 0)
        {

            StartCoroutine(LosingTimer());
        }
    }

    private IEnumerator LosingTimer()
    {
        if (_FruitInDangerZone) yield break;

        _FruitInDangerZone = true;
        yield return new WaitForSeconds(_timeToLose);
        
        if (_FruitInDangerZone == true)
        {
          
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

         
        }
    }
}
