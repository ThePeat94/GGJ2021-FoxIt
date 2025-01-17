using System.Collections;
using Nidavellir.FoxIt.Dialogue;
using Nidavellir.FoxIt.Enemy;
using Nidavellir.FoxIt.Enemy.Lord_Magma;
using Nidavellir.FoxIt.EventArgs;
using Nidavellir.FoxIt.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nidavellir.FoxIt.UI
{
    public class PlayerHud : MonoBehaviour
    {
        [SerializeField] private Slider m_healthBar;
        [SerializeField] private TextMeshProUGUI m_ammoText;
        [SerializeField] private PlayerController m_player;
        [SerializeField] private TextMeshProUGUI m_bossName;
        [SerializeField] private Slider m_bossHealthBar;
        [SerializeField] private GameObject m_playerBossHud;
        [SerializeField] private GameObject m_dialogueBox;
        [SerializeField] private TextMeshProUGUI m_dialogueText;
        [SerializeField] private GameObject m_youDiedPanel;
        [SerializeField] private GameObject m_youWonPanel;
        [SerializeField] private Image m_reloadImage;
        [SerializeField] private DialogueUI m_dialogueUI;

        private Coroutine m_hideCoroutine;
        private Coroutine m_reloadCoroutine;

        // Start is called before the first frame update
        private void Start()
        {
            this.m_healthBar.maxValue = this.m_player.HealthController.MaxValue;
            this.m_healthBar.minValue = 0;
            this.m_healthBar.value = this.m_player.HealthController.CurrentValue;

            this.m_player.HealthController.ResourceValueChanged += this.PlayerHealthChanged;
            this.m_player.HealthController.MaxValueChanged += this.PlayerMaxHealthChanged;
            this.m_ammoText.text = $"{this.m_player.QuiverController.CurrentValue}/{this.m_player.QuiverController.MaxValue}";
            this.m_player.QuiverController.MaxValueChanged += this.ArrowControllerValueChanged;
            this.m_player.QuiverController.ResourceValueChanged += this.ArrowControllerValueChanged;
        }

        public void ShowBossHud(MagmaBoss boss)
        {
            this.m_bossName.text = boss.Name;
            boss.HealthController.ResourceValueChanged += this.BossHealthChanged;
            this.m_bossHealthBar.maxValue = boss.HealthController.MaxValue;
            this.m_bossHealthBar.minValue = 0;
            this.m_bossHealthBar.value = boss.HealthController.CurrentValue;
            this.m_playerBossHud.SetActive(true);
        }

        public void ShowReloadUI(float duration)
        {
            this.StartCoroutine(this.ShowReloadAnimation(duration));
        }

        public void ShowWinScreen()
        {
            this.m_youWonPanel.SetActive(true);
            this.StartCoroutine(this.HideWinScreen());
        }

        private void ArrowControllerValueChanged(object sender, ResourceValueChangedEvent e)
        {
            this.m_ammoText.text = $"{this.m_player.QuiverController.CurrentValue}/{this.m_player.QuiverController.MaxValue}";
        }

        private void BossHealthChanged(object sender, ResourceValueChangedEvent e)
        {
            this.m_bossHealthBar.value = e.NewValue;

            if (e.NewValue <= 0) this.m_playerBossHud.SetActive(false);
        }

        private IEnumerator HideDialogue()
        {
            yield return new WaitForSeconds(10f);
            this.m_dialogueBox.SetActive(false);
            this.m_hideCoroutine = null;
        }

        private IEnumerator HideWinScreen()
        {
            yield return new WaitForSeconds(5f);
            this.m_youWonPanel.SetActive(false);
        }

        private void PlayerHealthChanged(object sender, ResourceValueChangedEvent e)
        {
            this.m_healthBar.value = e.NewValue;
            if (e.NewValue <= 0) this.m_youDiedPanel.SetActive(true);
        }

        private void PlayerMaxHealthChanged(object sender, ResourceValueChangedEvent e)
        {
            this.m_healthBar.maxValue = e.NewValue;
        }

        private IEnumerator ShowReloadAnimation(float duration)
        {
            var currentDuration = 0f;
            while (currentDuration < duration)
            {
                this.m_reloadImage.fillAmount = 1f - currentDuration / duration;
                yield return new WaitForEndOfFrame();
                currentDuration += Time.deltaTime;
            }

            this.m_reloadImage.fillAmount = 0f;
        }
    }
}