using System;
using Avalonia.Controls;

namespace MWE
{
    public record DataAlpha(IObservable<string> AlphaText);
    public record DataBeta(IObservable<string> BetaText);
    public record DataGamma();

    public class ControlAlpha : TextBlock { }
    public class ControlBeta : TextBlock { }
    public class ControlGamma : TextBlock { }
}
