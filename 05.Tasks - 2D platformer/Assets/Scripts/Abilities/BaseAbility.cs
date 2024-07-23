using Platformer.Base;
using UnityEngine;

namespace Platformer.Ability
{
    public abstract class BaseAbility : MonoBehaviour
    {
        public abstract void Execute(BaseUnit player);
    }
}
