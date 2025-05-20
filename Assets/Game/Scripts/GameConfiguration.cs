using UnityEngine;

namespace Moyba.Game
{
    [CreateAssetMenu(fileName = "NewGameConfiguration", menuName = "Configuration/Game")]
    public partial class GameConfiguration : ScriptableObject
    {
        private void OnValidate()
        {
            OnValidate_Signal();
        }
    }
}
