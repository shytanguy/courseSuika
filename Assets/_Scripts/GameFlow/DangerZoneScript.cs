using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZoneScript : MonoBehaviour
{

    private MeshRenderer _meshRenderer;

    [SerializeField] private LayerMask _fruitLayer;

   [SerializeField] private bool _Danger;

    [SerializeField] private float _flashSpeed=0.01f;

   [SerializeField] private float _transparency=0;

    private bool _addTransparency=true;

  [SerializeField]  private float _TimeBeforeDanger=1;

    private bool _Colliding;
    private IEnumerator DangerTimer()
    {
        yield return new WaitForSeconds(_TimeBeforeDanger);
        if (_Colliding==true)
        _Danger = true;

    }
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if ((_fruitLayer.value & (1 << collision.gameObject.layer)) > 0 && _Danger == false)
            {
                _Colliding = true;

                StartCoroutine(DangerTimer());
            }
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((_fruitLayer.value & (1 << collision.gameObject.layer)) > 0)
        _Colliding = false;
        if ((_fruitLayer.value & (1 << collision.gameObject.layer)) > 0 && _Danger == true)
        {
            if (collision.gameObject.transform.position.y < transform.position.y)
            {
              
                _Danger = false;
            }
        }
    }
  

    private void FixedUpdate()
    {
        if (_Danger)
        {

            if (_addTransparency)
            {
                _transparency += _flashSpeed;

                if (_transparency > 1f)
                    _addTransparency = false;
            }
            else
            {
                _transparency -= _flashSpeed;

                if (_transparency < 0.1f)
                    _addTransparency = true;
            }
            _meshRenderer.material.color = new Color(1, 0, 0, _transparency);

        }
        else
        {
            if (_transparency > 0f)
                _transparency -= _flashSpeed ;
            _meshRenderer.material.color = new Color(1, 0, 0, 0);
        }
    }
}
