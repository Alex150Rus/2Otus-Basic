using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "Scriptable", menuName = "AddScriptable", order = 9000)]
    public class PersonGroup : ScriptableObject
    {
        [Header("Параметры")]
        [Range(0, 100)]
        public int Health;
        [Space]
        [Header("Описание")]
        public string Name;
        [TextArea(3, 10)]
        [Tooltip("Подробное описание типа героя")]
        public string Description;

    }
}