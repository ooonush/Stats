using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(menuName = "Stats/Traits Class", fileName = "Class")]
    public sealed class TraitsClass : TraitsClassBase
    {
        [SerializeField] private List<StatItem> _statItems;
        [SerializeField] private List<AttributeItem> _attributeItems;
    }
}