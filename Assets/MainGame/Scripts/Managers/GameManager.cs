using System.Collections.Generic;
using CoreGames.GameName.Events.States;
using CoreGames.GameName.EventSystem;
using UnityEngine;

namespace CoreGames.GameName.Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Interact Objects")] 
        [SerializeField] private List<GameObject> levelItems;
        [SerializeField] private GameObject itemsParent;
        [SerializeField] private float startPos;
        [SerializeField] private float sideSpace;
        private float initialStartPos;

        private void OnEnable()
        {
            EventBus<GamePrepareEvent>.AddListener(GamePrepare);
            EventBus<GameStartEvent>.AddListener(GameStart);
            EventBus<GameNextLevelEvent>.AddListener(GameNextLevel);
            EventBus<GameRestartLevelEvent>.AddListener(GameRestartLevel);
            EventBus<GameOverEvent>.AddListener(GameOver);
            EventBus<GameEndEvent>.AddListener(GameEnd);
            EventBus<ResetLevelEvent>.AddListener(ResetLevel);
        }

        private void OnDisable()
        {
            EventBus<GamePrepareEvent>.RemoveListener(GamePrepare);
            EventBus<GameStartEvent>.RemoveListener(GameStart);
            EventBus<GameNextLevelEvent>.RemoveListener(GameNextLevel);
            EventBus<GameRestartLevelEvent>.RemoveListener(GameRestartLevel);
            EventBus<GameOverEvent>.RemoveListener(GameOver);
            EventBus<GameEndEvent>.RemoveListener(GameEnd);
            EventBus<ResetLevelEvent>.RemoveListener(ResetLevel);
        }

        private void Start()
        {
            //EventBus<GamePrepareEvent>.Emit(this, new GamePrepareEvent());
            initialStartPos = startPos;
            SetInteractObjects();
        }

        private void GamePrepare(object sender, GamePrepareEvent e)
        {
            GameStateManager.Instance.SetGameState(GameStateManager.GameState.Prepare);
        }

        private void GameStart(object sender, GameStartEvent e)
        {
            GameStateManager.Instance.SetGameState(GameStateManager.GameState.Start);
        }

        private void GameNextLevel(object sender, GameNextLevelEvent e)
        {
            GameStateManager.Instance.SetGameState(GameStateManager.GameState.NextLevel);
            EventBus<GamePrepareEvent>.Emit(this, new GamePrepareEvent());
        }

        private void GameRestartLevel(object sender, GameRestartLevelEvent e)
        {
            GameStateManager.Instance.SetGameState(GameStateManager.GameState.RestartLevel);
            EventBus<GamePrepareEvent>.Emit(this, new GamePrepareEvent());
        }

        private void GameOver(object sender, GameOverEvent e)
        {
            GameStateManager.Instance.SetGameState(GameStateManager.GameState.Over);
        }

        private void GameEnd(object sender, GameEndEvent e)
        {
            GameStateManager.Instance.SetGameState(GameStateManager.GameState.End);
        }

        //Called by button on click event
        public void CallStartEvent()
        {
            EventBus<GameStartEvent>.Emit(this, new GameStartEvent());
        }

        //Called by button on click event
        public void CallRestartEvent()
        {
            //FIRST FOR RESTART CODES
            EventBus<GameRestartLevelEvent>.Emit(this, new GameRestartLevelEvent());
        }

        //Called by button on click event
        public void CallNextLevelEvent()
        {
            //FIRST FOR NEXT LEVEL CODES
            EventBus<GameNextLevelEvent>.Emit(this, new GameNextLevelEvent());
        }

        private void ResetLevel(object sender, ResetLevelEvent e)
        {
            Invoke(nameof(SetInteractObjects), 0.05f);
        }

        private void SetInteractObjects()
        {
            DestroyPreviousInteractObjects();
            
            for (int i = 0; i < levelItems.Count; i++)
            {
                Instantiate(levelItems[i].gameObject,
                    new Vector3(GetRandomIndex(-3, 0, 3), levelItems[i].transform.position.y, startPos),
                    Quaternion.Euler(levelItems[i].transform.rotation.x, levelItems[i].transform.rotation.y,
                        levelItems[i].transform.rotation.z), itemsParent.transform);
                startPos += sideSpace;
            }
        }

        private void DestroyPreviousInteractObjects()
        {
            if (itemsParent.transform.childCount == 0) return;

            startPos = initialStartPos;

            for (int i = 0; i < itemsParent.transform.childCount; i++)
            {
               Destroy(itemsParent.transform.GetChild(i).gameObject);
            }
        }

        private int GetRandomIndex(int valueOne, int valueTwo, int valueThree)
        {
            int randomValue = Random.Range(0, 4);

            switch (randomValue)
            {
                case 1:
                    randomValue = valueOne;
                    break;
                case 2:
                    randomValue = valueTwo;
                    break;
                case 3:
                    randomValue = valueThree;
                    break;
            }

            return randomValue;
        }
    }
}