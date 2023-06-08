using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public sealed class StatItem
    {
        [SerializeField] private Stat _stat;

        [SerializeField] private bool _changeBase;
        [SerializeField] private float _base = 100;

        [SerializeField] private bool _changeFormula;
        [SerializeField] private StatFormula _formula;

        public StatType StatType => _stat.Type;

        public float Base
        {
            get
            {
                if (!_stat) return 0f;
                return _changeBase ? _base : _stat.Base;
            }
        }

        public StatFormula Formula
        {
            get
            {
                if (!_stat) return null;
                return _changeFormula ? _formula : _stat.Formula;
            }
        }
    }
}