using UI;
using UnityEngine;

namespace UI
{
    public class UIFactory : MonoBehaviour
    {
        [SerializeField] private UIBase[] _allUIs;

        private void Start()
        {
            GetUI<MainMenu_UI>();
        }

        public UIBase GetUI<_SomeUI>()
        {
            UIBase result = null;

            foreach (UIBase ui in _allUIs)
            {
                if (ui is _SomeUI == true)
                {
                    result = Instantiate(ui);
                };
            }

            return result;
        }
    }
}