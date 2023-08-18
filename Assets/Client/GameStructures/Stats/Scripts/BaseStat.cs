using Architecture;
using SpaceTraveler.GameStructures.Stats.Presets;
using SpaceTraveler.GameStructures.Stats.StatModifiers;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceTraveler.GameStructures.Stats
{
    [System.Serializable]
    public abstract class BaseStat : IJsonSerializable
    {
        [SerializeField]
        protected string _name;
        [SerializeField]
        protected float _baseValue;


        protected StatPreset statPreset;
        public string Name  => statPreset.Name;

        public float Value { get; protected set; }

        public string Id => statPreset.Id;

        public virtual void Initialize()
        {
            Value = _baseValue;
        }
        public void CalculateValue(List<StatModifier> modifiers)
        {
            Value = _baseValue;

            var flatModifiers = modifiers.FindAll(modifier => modifier.Type == StatModifierType.FlatAdd);
            var multiplierModifiers = modifiers.FindAll(modifier => modifier.Type == StatModifierType.Multiplier);
            var percentaddModifiers = modifiers.FindAll(modifier => modifier.Type == StatModifierType.PercentAdd);

            foreach (var modifier in flatModifiers)
            {
                Value += modifier.Value;
            }
            foreach (var modifier in multiplierModifiers)
            {
                Value *= modifier.Value;
            }
            foreach (var modifier in percentaddModifiers)
            {
                var addedValue = Value * modifier.Value;
                Value += addedValue;
            }

        }
        public virtual void CalculateValue()
        {
            var newValue = _baseValue;
            var modifiers = statsHandler.GetAllModifiers(Name);

            foreach (StatModifier modifier in modifiers)
            {

                    switch (modifier.Type)
                    {
                        case StatModifierType.FlatAdd :
                            newValue += modifier.Value;
                            break;

                        case StatModifierType.PercentAdd:
                            var valueToAdd = newValue * modifier.Value;
                            newValue += valueToAdd;
                            break;

                        case StatModifierType.Multiplier:
                            var multValue = newValue * (modifier.Value);
                            newValue = multValue;
                            break;
                    }           
            }
            Value = newValue;

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

