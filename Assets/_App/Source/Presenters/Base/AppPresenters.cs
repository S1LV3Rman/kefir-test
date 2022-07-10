using System.Collections.Generic;

namespace Source
{
    public class AppPresenters
    {
        private List<IInitPresenter> _initPresenters;
        private List<IRunPresenter> _runPresenters;

        public AppPresenters()
        {
            _initPresenters = new List<IInitPresenter>();
            _runPresenters = new List<IRunPresenter>();
        }
        
        public AppPresenters Add(IPresenter presenter)
        {
            if (presenter is IInitPresenter initSystem)
                _initPresenters.Add(initSystem);
            
            if (presenter is IRunPresenter runSystem)
                _runPresenters.Add(runSystem);
            
            return this;
        }

        public void Init()
        {
            foreach (var presenter in _initPresenters)
                presenter.Init();
        }

        public void Run()
        {
            foreach (var presenter in _runPresenters)
                presenter.Run();
        }
    }
}