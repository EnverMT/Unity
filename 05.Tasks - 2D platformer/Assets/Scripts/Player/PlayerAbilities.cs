using Platformer.Ability;
using System.Linq;

using UnityEngine;


namespace Platformer.Player
{
    public class PlayerAbilities : MonoBehaviour
    {
        [SerializeField] private BaseAbility[] _abilities;

        public bool TryGetAbility<T>(out BaseAbility ability) where T : BaseAbility
        {
            if (_abilities == null && _abilities.Length == 0)
            {
                ability = null;
                return false;
            }

            var result = _abilities.FirstOrDefault(item => item is T);

            if (result != null)
            {
                ability = result;
                return true;
            }

            ability = null;
            return false;
        }
    }
}