using Architecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    [System.Serializable]
    public abstract class BaseStat : IJsonSerializable
    {
        [SerializeField]
        protected float _baseValue;

        protected StatPreset statPreset;
        public string Name  => statPreset.Name; 
        [System.NonSerialized]
        protected float value;
        public float Value { get => value; }

        protected StatsHandler statsHandler;

        public virtual void Initialize(StatsHandler handler)
        {
            statsHandler = handler;
            value = _baseValue;
        }
        public virtual void CalculateValue()
        {
            var newValue = _baseValue;
            var modifiers = statsHandler.GetAllModifiers(Name);
            foreach(StatModifier modifier in modifiers)
            {
                    switch(modifier.Type)
                    {
                        case StatModType.Flat :
                            newValue += modifier.Value;
                            break;

                        case StatModType.PercentAdd:
                            var valueToAdd = newValue * modifier.Value;
                            newValue += valueToAdd;
                            break;

                        case StatModType.PercentMult:
                            var multValue = newValue * (modifier.Value);
                            newValue = multValue;
                            break;
                    }           
            }
            value = newValue;
        }

        public virtual void SetObjectData(Dictionary<string, object> data)
        {
            var id = data["Stat_ID"].ToString();
            var basevalue = System.Convert.ToInt32(data["Base_Value"]);

            var repository = Game.GetRepository<StatsPresetRepository>();
            statPreset = repository.GetStatPreset(id);
            _baseValue = basevalue;

        }

        public virtual Dictionary<string, object> GetObjectData()
        {
            var data = new Dictionary<string, object>();

            data.Add("Stat_ID", statPreset.Id);
            data.Add("Base_Value", _baseValue);

            return data;
        }
    }
}

