using UnityEngine;

namespace ScoreSystem
{
    [CreateAssetMenu(fileName = "new ScoreSO", menuName = "ScoreSO/Create new ScoreSO")]
    public class ScoreSO : ScriptableObject
    {
        [SerializeField] private int _points;

        public int Points => _points;

        public void Init(int points)
        {
            _points = points;
        }

        public void Add(int count)
        {
            _points += count;
        }

        public void Reset()
        {
            _points = 0;
        }
    }
}