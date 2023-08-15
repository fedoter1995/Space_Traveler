using SpaceTraveler.GameStructures.Stats.Presets;

namespace SpaceTraveler.GameStructures.Stats
{
    public abstract class LastingStatEffect : BaseStat
    {

        protected LastingEffectStatPreset lastingeffectPreset;
        public override void Initialize(StatsHandler handler)
        {
            base.Initialize(handler);

            if (statPreset is null)
                statPreset = lastingeffectPreset;
            else
                lastingeffectPreset = statPreset as LastingEffectStatPreset;
        }
    }
}