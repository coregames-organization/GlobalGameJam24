using CoreGames.GameName.Events.States;
using CoreGames.GameName.EventSystem;
using TMPro;
using UnityEngine;

namespace CoreGames.GameName.Managers
{
    public class UIManager : MonoBehaviour
    {
        [Header("Collactible")]
        [SerializeField] private TextMeshProUGUI preparePanel;
        private int collactibleCount;
        
        private void OnEnable()
        {
            EventBus<GameStartEvent>.AddListener(GameStart);
            EventBus<GameNextLevelEvent>.AddListener(GameNextLevel);
            EventBus<GameRestartLevelEvent>.AddListener(GameRestartLevel);
            EventBus<GameOverEvent>.AddListener(GameOver);
            EventBus<GameEndEvent>.AddListener(GameEnd);
            EventBus<GamePrepareEvent>.AddListener(GamePrepare);
            EventBus<SetCollactibleCountEvent>.AddListener(CollactibleHa);
            EventBus<ResetLevelEvent>.AddListener(ResetLevel);
        }

        private void OnDisable()
        {
            EventBus<GameStartEvent>.RemoveListener(GameStart);
            EventBus<GameNextLevelEvent>.RemoveListener(GameNextLevel);
            EventBus<GameRestartLevelEvent>.RemoveListener(GameRestartLevel);
            EventBus<GameOverEvent>.RemoveListener(GameOver);
            EventBus<GameEndEvent>.RemoveListener(GameEnd);
            EventBus<GamePrepareEvent>.RemoveListener(GamePrepare);
            EventBus<SetCollactibleCountEvent>.RemoveListener(CollactibleHa);
            EventBus<ResetLevelEvent>.RemoveListener(ResetLevel);
        }

        private void GamePrepare(object sender, GamePrepareEvent e)
        {
           
        }

        private void GameStart(object sender, GameStartEvent e)
        {
           
        }
        
        private void GameNextLevel(object sender, GameNextLevelEvent e)
        {
            
        }
        
        private void GameRestartLevel(object sender, GameRestartLevelEvent e)
        {
            
        }
        
        private void GameOver(object sender, GameOverEvent e)
        {
            
        }
        
        private void GameEnd(object sender, GameEndEvent e)
        {
            
        }

        private void CollactibleHa(object sender, SetCollactibleCountEvent e)
        {
            collactibleCount++;
            preparePanel.text = collactibleCount.ToString();
        }

        private void ResetLevel(object sender, ResetLevelEvent e)
        {
            collactibleCount = 0;
            preparePanel.text = collactibleCount.ToString();
        }
    }
}
