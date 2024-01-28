using System.Collections;
using CoreGames.GameName.Events.States;
using CoreGames.GameName.EventSystem;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CoreGames.GameName.Managers
{
    public class UIManager : MonoBehaviour
    {
        [Header("Collactible")]
        [SerializeField] private TextMeshProUGUI preparePanel;
        [SerializeField] private GameObject infoPanel;
        [SerializeField] private GameObject finishPanel;
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
            EventBus<TutorialEvent>.AddListener(SetActiveTutPanel);
            EventBus<FinishEvent>.AddListener(FinishGame);

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
            EventBus<TutorialEvent>.RemoveListener(SetActiveTutPanel);
            EventBus<FinishEvent>.RemoveListener(FinishGame);
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
            preparePanel.transform.DOScale(Vector3.one * 1.2f, 0.25f).OnComplete(() =>
            {
                preparePanel.transform.DOScale(Vector3.one, 0.25f);
            });
        }

        private void ResetLevel(object sender, ResetLevelEvent e)
        {
            collactibleCount = 0;
            preparePanel.text = collactibleCount.ToString();
            preparePanel.transform.DOScale(Vector3.one * 1.2f, 0.25f).OnComplete(() =>
            {
                preparePanel.transform.DOScale(Vector3.one, 0.25f);
            });
        }

        private void SetActiveTutPanel(object sender, TutorialEvent e)
        {
            StartCoroutine(nameof(SetActiveDelay));
        }

        private IEnumerator SetActiveDelay()
        {
            infoPanel.SetActive(true);
            yield return new WaitForSeconds(10f);
            infoPanel.SetActive(false);
        }
        
        
        private void FinishGame(object sender, FinishEvent e)
        {
            StartCoroutine(nameof(SetActiveDelay2));
        }
        
        private IEnumerator SetActiveDelay2()
        {
            yield return new WaitForSeconds(5f);
            finishPanel.SetActive(true);
            yield return new WaitForSeconds(5f);
            Application.Quit();
        }
    }
}
