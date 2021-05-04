using System;
using System.Reactive.Subjects;
using System.Collections.Generic;
using Avalonia.Controls;
using ReactiveUI;

namespace MWE.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        readonly BehaviorSubject<IReadOnlyList<Item>> _specimens = new(Array.Empty<Item>());
        static readonly BehaviorSubject<bool> _alwaysTrue_lasting = new(true);
        static BehaviorSubject<bool> _alwaysTrue_ephemeral => new(true);

        public record Item(string Name, Control LeakTrackingControl, IObservable<bool> AlwaysTrue);
        public IObservable<IReadOnlyList<Item>> Specimens => _specimens;

        public IReactiveCommand PopulateCommand => ReactiveCommand.Create(Populate);
        public IReactiveCommand ClearCommand => ReactiveCommand.Create(Clear);

        void Populate()
        {
            _specimens.OnNext(new[] {
                new Item(nameof(ControlAlpha), new ControlAlpha(), _alwaysTrue_lasting),
                new Item(nameof(ControlBeta), new ControlBeta(), _alwaysTrue_ephemeral),
            });
        }

        void Clear()
        {
            _specimens.OnNext(Array.Empty<Item>());
            GC.Collect();
            GC.Collect();
            GC.Collect();
        }
    }
}
