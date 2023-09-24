
using SpaceTraveler.Audio;

namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public interface IСheckGroundState
    {
        void OnGroundStateChange(bool onGround);
        void OnGroundTypeChange(GroundSettings settings);
    }
}
