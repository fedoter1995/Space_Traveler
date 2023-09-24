namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public class PlayerUnarmedState : PlayerArmedUnarmedState
    {
        public PlayerUnarmedState(Player player) : base(player)
        {
            stateName = "Unarmed_States";
        }
        public override void Enter()
        {
            base.Enter();
        }
    }
}