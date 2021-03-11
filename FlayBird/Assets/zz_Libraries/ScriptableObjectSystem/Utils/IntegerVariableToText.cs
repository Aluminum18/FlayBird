using UnityEngine;
using TMPro;

public class IntegerVariableToText : MonoBehaviour
{
    [SerializeField]
    private IntegerVariable _intVariable;
    [SerializeField]
    private TMP_Text _textMesh;

    private void OnEnable()
    {
        _textMesh.text = _intVariable.Value.ToString();
        _intVariable.OnValueChange += UpdateStringValue;
    }

    private void OnDisable()
    {
        _intVariable.OnValueChange -= UpdateStringValue;
    }

    private void UpdateStringValue(int newValue)
    {
        _textMesh.text = newValue.ToString();
    }
}
