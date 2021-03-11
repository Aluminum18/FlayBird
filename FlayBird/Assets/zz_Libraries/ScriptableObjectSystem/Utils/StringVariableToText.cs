using UnityEngine;
using TMPro;

public class StringVariableToText : MonoBehaviour
{
    [SerializeField]
    private StringVariable _stringVariable;
    [SerializeField]
    private TMP_Text _textMesh;

    private void OnEnable()
    {
        _textMesh.text = _stringVariable.Value;
        _stringVariable.OnValueChange += UpdateStringValue;
    }

    private void OnDisable()
    {
        _stringVariable.OnValueChange -= UpdateStringValue;
    }

    private void UpdateStringValue(string newValue)
    {
        if (newValue.Equals(_textMesh.text))
        {
            return;
        }

        _textMesh.text = newValue;
    }
}
