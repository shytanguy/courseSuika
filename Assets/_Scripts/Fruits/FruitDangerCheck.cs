using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDangerCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _dangerZone;

    private bool _checkForDanger=false;

    private bool _added;
  /*  private void FixedUpdate()
    { if (!_checkForDanger) return;
        if (transform.position.y > GameFlowScript.instance.yCoordinate&&_added==false)
        {
            _added = true;
            GameFlowScript.instance.AddDanger(transform);
        }
        else if (_added == true && transform.position.y < GameFlowScript.instance.yCoordinate)
        {
            GameFlowScript.instance.RemoveDanger(transform);
            _added = false;
        }
    }*/
    private void OnCollisionStay2D(Collision2D collision)
    {


        if (_checkForDanger == false) 
        _checkForDanger = true;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_dangerZone.value & (1 << collision.gameObject.layer)) > 0&&_checkForDanger&&_added==true)
        {
            Debug.Log($"remove danger {gameObject} {transform.position}");
            GameFlowScript.instance.RemoveDanger(transform);
            _added = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((_dangerZone.value & (1 << collision.gameObject.layer)) > 0 && _checkForDanger && _added == true)
        {
            Debug.Log($"remove danger {gameObject} {transform.position}");
            GameFlowScript.instance.RemoveDanger(transform);
            _added = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((_dangerZone.value & (1 << collision.gameObject.layer)) > 0 && _checkForDanger&&_added==false&&gameObject.activeSelf)
        {
            Debug.Log($"add danger {gameObject}  {transform.position}");
            GameFlowScript.instance.AddDanger(transform);
            _added = true;
        }
    }
 
    private void OnDisable()
    {
        GameFlowScript.instance.RemoveDanger(transform);
    }

}
