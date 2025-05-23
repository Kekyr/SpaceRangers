using UnityEngine;

[CreateAssetMenu(fileName = "new ResetSO", menuName = "ResetSO/Create new ResetSO")]
public class ResetSO : ScriptableObject
{
    [SerializeField] private bool _isReseted;

    public bool IsReseted => _isReseted;

    public void Reseted()
    {
        _isReseted = true;
    }
}
