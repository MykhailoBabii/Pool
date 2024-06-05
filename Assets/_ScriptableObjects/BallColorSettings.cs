using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Provider
{
    [CreateAssetMenu(fileName = "BallColorSettings", menuName = "Create SO/BallColorSettings")]
    public class BallColorSettings : ScriptableObject
    {
        [field: SerializeField] public List<Color> BallColorList { get; private set; }
    }
}
