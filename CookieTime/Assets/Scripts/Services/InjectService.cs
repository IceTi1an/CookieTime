using Zenject;

namespace Game
{
    public static class InjectService
    {
        public static void Inject(object obj)
        {
            ProjectContext.Instance.Container.Inject(obj);
        }
    }
}