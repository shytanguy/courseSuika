using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoseTimerVisuals : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;

    private Vector3 _initScale;

  [SerializeField]  private GameFlowScript _flowScript;
    private void Start()
    {
        _textMesh.gameObject.SetActive(false);

        _initScale = _textMesh.rectTransform.localScale;

    }

    private void OnEnable()
    {
        _flowScript.TimeRemaining += ChangeText;
    }

    private void OnDisable()
    {
        _flowScript.TimeRemaining -= ChangeText;
    }

    private void ChangeText(float time,bool active)
    {

        _textMesh.gameObject.SetActive(active);

        if (active)
        {
            _textMesh.text = time.ToString("0.00");
            if (time>0)
            _textMesh.rectTransform.localScale = _initScale * Mathf.Clamp( 1/time,0.5f,10);
        }
    }
}
