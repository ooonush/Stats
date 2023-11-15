using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    internal class StatAssetOverride<TNumber> : IStat<TNumber> where TNumber : IStatNumber<TNumber>
    {
        [SerializeField] private StatAsset<TNumber> _statAsset;

        [SerializeField] private bool _changeBase;
        [SerializeField] private TNumber _base;

        [SerializeField] private bool _changeFormula;
        [SerializeField] private StatFormula<TNumber> _formula;

        public StatId<TNumber> StatId => _statAsset != null ? _statAsset.StatId : default;

        public TNumber Base
        {
            get
            {
                if (!_statAsset) return default;
                return _changeBase ? _base : _statAsset.Base;
            }
        }

        public StatFormula<TNumber> Formula
        {
            get
            {
                if (!_statAsset) return default;
                return _changeFormula ? _formula : _statAsset.Formula;
            }
        }
    }
}