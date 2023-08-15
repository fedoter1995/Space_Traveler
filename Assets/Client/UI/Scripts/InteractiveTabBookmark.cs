using System;

namespace SpaceTraveler.UI
{
    public class InteractiveTabBookmark : Bookmark
    {

        private InteractiveTab tab;

        public event Action<InteractiveTabBookmark> OnClickEvent;

        public void SetGarageTab(InteractiveTab tab)
        {
            this.tab = tab;

            _textMesh.text = tab.Name;
        }
        public override void MouseEnter()
        {
    
        }

        public override void MouseExit()
        {
        
        }

        public override void SetActive(bool activity)
        {
            if(activity)
                tab.Open();
            else
                tab.Close();

            base.SetActive(activity);
        }

        public override void OnClick()
        {
            OnClickEvent?.Invoke(this);
        }
    }
}


