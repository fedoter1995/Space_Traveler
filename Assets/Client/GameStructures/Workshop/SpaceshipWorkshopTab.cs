using System;


namespace SpaceTraveler.GameStructures.Workshop.UI
{
    public class SpaceshipWorkshopTab : WorkshopTab
    {
        public override void Initialize()
        {

            var spaceShip = Architecture.Game.GetInteractor<SpaceshipInteractor>().spaceship;

            currentObject = spaceShip;

            if (currentObject == null)
            {
                throw new Exception("An interface of the wrong type was passed");
            }

            Close();
            _craftPanel.Initialize(currentObject);

            _craftPanel.ButtonClickEvent.AddListener(TryChangeEquipment);

            InitTrees();
        }
    }
}
