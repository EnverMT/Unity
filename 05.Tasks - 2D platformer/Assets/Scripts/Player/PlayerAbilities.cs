using Platformer.Ability;
using System.Linq;

using UnityEngine;


namespace Platformer.Player
{
    public class PlayerAbilities : MonoBehaviour
    {
        [SerializeField] private BaseAbility[] _abilities;

        public bool TryGetAbility<T>(out T ability) where T : BaseAbility
        {
            ability = _abilities?.OfType<T>().FirstOrDefault();
            return ability != null;
        }
    }
}