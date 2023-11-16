namespace SpaceTraveler.Characters.Player.PlayerFiniteStateMachine
{
    public class PlayerStateMachine
    {
        public PlayerPostureState PostureState { get; private set; }
        public PlayerState CurrentState { get; private set; }
        public PlayerArmedUnarmedState CurrentArmamentState { get; private set; }
        public SuperState SuperState { get; private set; }

        public void Initialize(PlayerArmedUnarmedState initialSuperState, PlayerPostureState postureState, PlayerState initialState)
        {
            CurrentArmamentState = initialSuperState;
            PostureState = postureState;
            CurrentState = initialState;


            CurrentArmamentState.Enter();
            PostureState.Enter();

            //It's last
            CurrentState.Enter();

        }
        public void ChangeState(PlayerState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
        public void ChangeArmamentState(PlayerArmedUnarmedState newState)
        {
            CurrentArmamentState.Exit();
            CurrentArmamentState = newState;
            CurrentArmamentState.Enter();

            ChangeState(CurrentState);
        }
        public void ChangePostureState(PlayerPostureState newState)
        {
            PostureState.Exit();
            PostureState = newState;
            PostureState.Enter();

            ChangeState(CurrentState);
        }
        public void SetSuperState(SuperState newState)
        {
            if (SuperState != null)
                ClearSuperState();

            SuperState = newState;
            SuperState.Enter();

        }
        public void ClearSuperState()
        {
            if (SuperState != null)
            {
                SuperState.Exit();
                SuperState = null;
            }
        }
        public string GetMainStateName()
        {
            if (SuperState != null)
                return SuperState.Name;

            if(PostureState.Name != "")
                return CurrentArmamentState.Name +"_"+ PostureState.Name;
            else 
                return CurrentArmamentState.Name;
        }

    }
}
