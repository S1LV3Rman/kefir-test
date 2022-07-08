using System.Collections.Generic;

namespace Source
{
    public class AppSystems
    {
        private List<IInitSystem> _initsystems;
        private List<IRunSystem> _runSystems;

        public AppSystems()
        {
            _initsystems = new List<IInitSystem>();
            _runSystems = new List<IRunSystem>();
        }
        
        public AppSystems Add(ISystem system)
        {
            if (system is IInitSystem initSystem)
                _initsystems.Add(initSystem);
            
            if (system is IRunSystem runSystem)
                _runSystems.Add(runSystem);
            
            return this;
        }

        public void Init()
        {
            foreach (var system in _initsystems)
                system.Init();
        }

        public void Run()
        {
            foreach (var system in _runSystems)
                system.Run();
        }
    }
}