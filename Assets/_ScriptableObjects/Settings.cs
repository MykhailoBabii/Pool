using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Provider
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Create SO/Settings")]
    public class Settings : ScriptableObject
    {
        [field: SerializeField, Range(0f, 1f)] public float Stretch { get; private set; }
        [field: SerializeField] public float MinTouchDistance { get; private set; }
        [field: SerializeField] public float MaxTouchDistance { get; private set; }
        [field: SerializeField] public float AdditionalPower { get; private set; }
    }
}
