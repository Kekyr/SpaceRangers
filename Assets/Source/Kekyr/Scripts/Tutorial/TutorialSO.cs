using UnityEngine;

[CreateAssetMenu(fileName = "new TutorialSO", menuName = "TutorialSO/Create new TutorialSO")]
public class TutorialSO : ScriptableObject
{
    [SerializeField] private bool[] _positions;

    public bool Check(int index)
    {
        return _positions[index] == false;
    }

    public void Completed(int index)
    {
        _positions[index] = true;
    }
}