using FMODUnity;
using UnityEngine;

namespace CoreGames.GameName.Sound
{
    public class FMODEvents : MonoBehaviour
    {
        // Specify the category of the sound.
        //[field: Header("ENV")]
        
        // Define the sound in a variable.
        
        [field: Header("MSC")]
        [field: SerializeField] public EventReference bg_msc { get; private set; }
        
        [field: Header("SFX")]
        [field: SerializeField] public EventReference sfx_frogLaugh { get; private set; }
        [field: SerializeField] public EventReference sfx_ha { get; private set; }

        #region MyRegion
        public static FMODEvents instance { get; private set; }
        private void Awake()
        {
            if (instance != null)
                Debug.LogError("Found more than one FMOD Events instance in the scene");

            instance = this;
        }
        #endregion
    }
}