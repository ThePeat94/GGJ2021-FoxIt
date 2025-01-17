using UnityEngine;

namespace Nidavellir.FoxIt.Enemy.Colliders
{
    public class RepeatingAttackCollider : MonoBehaviour
    {
        private float m_currentTickCoolDown;
        private PlayerController m_hitPlayer;
        
        public int Damage { get; set; }
        public AudioClip AttackSound { get; set; }

        private void Update()
        {
            if (this.m_currentTickCoolDown > 0f)
                this.m_currentTickCoolDown -= Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            var hitPlayer = other.GetComponent<PlayerController>();
            if (hitPlayer != null) this.m_hitPlayer = hitPlayer;
        }

        private void OnTriggerExit(Collider other)
        {
            var hitPlayer = other.GetComponent<PlayerController>();
            if (hitPlayer != null) this.m_hitPlayer = null;
        }

        private void OnTriggerStay(Collider other)
        {
            if (this.m_currentTickCoolDown <= 0f && this.m_hitPlayer != null)
            {
                this.m_hitPlayer.HealthController.UseResource(this.Damage);
                this.m_currentTickCoolDown = 1f;
            }
        }
    }
}
