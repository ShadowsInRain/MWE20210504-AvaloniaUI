using System;
using System.Reactive.Subjects;
using System.Collections.Generic;
using ReactiveUI;

namespace MWE.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        static IReadOnlyList<object> NoItems => Array.Empty<object>();
        readonly BehaviorSubject<IReadOnlyList<object>> _items = new(NoItems);

        IObservable<string> _alphaText { get; } = new BehaviorSubject<string>("Alpha");
        IObservable<string> _betaText => new BehaviorSubject<string>("Beta");
        public string GammaText => "Gamma";

        public IObservable<IReadOnlyList<object>> AllItems => _items;

        public IReactiveCommand PopulateCommand => ReactiveCommand.Create(Populate);
        public IReactiveCommand ClearCommand => ReactiveCommand.Create(Clear);

        void Populate()
        {
            _items.OnNext(new object[] {
                new DataAlpha(_alphaText),
                new DataBeta(_betaText),
                new DataGamma(),
            });
        }

        void Clear()
        {
            _items.OnNext(NoItems);

            // does not seems to help, but whatever
            GC.Collect();
            GC.Collect();
            GC.Collect();
        }
    }
}
