using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDangerCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _dangerZone;

    private bool _checkForDanger=false;

    private bool _added;
   
    private void OnCollisionStay2D(Collision2D collision)
    {


        if (_checkForDanger == false)
        {
            _checkForDanger = true;

         if(   Physics2D.OverlapCircle(transform.position, 0.1f, _dangerZone) == null)
            {

              
                GameFlowScript.instance.AddDanger(transform);
                _added = true;
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_dangerZone.value & (1 << collision.gameObject.layer)) > 0&&_checkForDanger&&_added==true)
        {
         
            GameFlowScript.instance.RemoveDanger(transform);
            _added = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((_dangerZone.value & (1 << collision.gameObject.layer)) > 0 && _checkForDanger && _added == true)
        {
       
            GameFlowScript.instance.RemoveDanger(transform);
            _added = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((_dangerZone.value & (1 << collision.gameObject.layer)) > 0 && _checkForDanger&&_added==false&&gameObject.activeSelf)
        {
           
            GameFlowScript.instance.AddDanger(transform);
            _added = true;
        }
    }
 
    private void OnDisable()
    {
        GameFlowScript.instance.RemoveDanger(transform);
    }

}
