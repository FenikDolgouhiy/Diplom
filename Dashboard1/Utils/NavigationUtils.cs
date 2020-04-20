using System.Collections.Generic;

namespace Dashboard1.Utils
{
    static class NavigationUtils
    {
        static Dictionary<NavigationPanel, INavigationManager> _panels = new Dictionary<NavigationPanel, INavigationManager>();

        public static void Register(NavigationPanel navigationPanel, INavigationManager manager)
        {
            if (_panels.ContainsKey(navigationPanel))
            {
                _panels.Remove(navigationPanel);
            }
            _panels.Add(navigationPanel, manager);
        }

        public static INavigationManager GetNavigationManager(NavigationPanel navigationPanel)
        {
            _panels.TryGetValue(navigationPanel, out INavigationManager result);
            return result;
        }


        public enum NavigationPanel
        {
            START_WINDOW,
            LISTS
        }
    }
}
