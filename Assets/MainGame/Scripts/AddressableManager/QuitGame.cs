using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreGames.GameName
{
    public class QuitGame : MonoBehaviour
    {
       public void GameQuit()
        {
            Application.Quit();
            Debug.Log("Quited");
        }
    }
}
