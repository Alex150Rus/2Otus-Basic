using UnityEngine;

namespace Assets.Scripts
{
    public class Attack : MonoBehaviour
    {
        public bool Ok;
        public int Atk;

        [ContextMenu("Attack")]
        void AttackTarget()
        {
            Atk = CanAttack() ? 1 : 2;
            this.Info("Atk: " + Atk);
        }

        [ContextMenu("Attack", isValidateFunction: true)]
        bool CanAttack()
        {
            return Ok;
        }
    }
}
