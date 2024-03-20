using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZoneScript : MonoBehaviour
{

    private MeshRenderer _meshRenderer;

    [SerializeField] private LayerMask _fruitLayer;

    private bool _Danger;

    private float _transparency=0;

    private bool _addTransparency=true;
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionStay(Collision collision)
    {
       if ((_fruitLayer.value & (1 << collision.gameObject.layer)) > 0&&_Danger==false)
        {
            _Danger = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((_fruitLayer.value & (1 << collision.gameObject.layer)) > 0 && _Danger == true)
        {
            if (collision.gameObject.transform.position.y < transform.position.y)
            {
                _meshRenderer.material.color = new Vector4(1, 0, 0, 0);
                _Danger = false;
            }
        }
    }

    private void Update()
    {
        if (_Danger)
        {
            _meshRenderer.material.SetColor(0, new Vector4(1,0,0,_transparency));

            if (_addTransparency) {
                _transparency += 0.01f;

                if (_transparency > 0.85f)
                    _addTransparency = false;
            }
            else
            {
                _transparency -= 0.01f;

                if (_transparency < 0.1f)
                    _addTransparency = true;
            }

        }
    }
}
