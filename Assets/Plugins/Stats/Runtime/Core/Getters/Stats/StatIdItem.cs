using System;

namespace Stats
{
    [Serializable]
    public sealed class StatIdItem : Getter<StatId>
    {
        public StatIdItem() : base(new StatIdValueGetType())
        {
        }

        public StatIdItem(StatId statId) : base(new StatIdValueGetType(statId))
        {
        }
    }

    [Serializable]
    public sealed class StatIdItem<TNumber> : Getter<StatId<TNumber>> where TNumber : IStatNumber<TNumber>
    {
        public StatIdItem() => SetDefaultEditorPropertyType<StatIdValueGetType<TNumber>>();

        public StatIdItem(StatId<TNumber> statId)
        {
            Property = new StatIdValueGetType(statId);
        }

        public StatIdItem(string statId)
        {
            Property = new StatIdValueGetType(statId);
        }

        public override StatId<TNumber> Value
        {
            get
            {
                if (Property is StatIdValueGetType stringGetType)
                {
                    return (StatId<TNumber>)stringGetType.Get();
                }
                return base.Value;
            }
        }
    }
}