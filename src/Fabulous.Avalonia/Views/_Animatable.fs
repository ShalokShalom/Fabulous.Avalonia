namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Animation
open Fabulous
open Fabulous.StackAllocatedCollections

type IFabAnimatable =
    inherit IFabElement

module Animatable =

    let Clock = Attributes.defineAvaloniaPropertyWithEquality Animatable.ClockProperty

    let Transitions =
        Attributes.defineAvaloniaListWidgetCollection "Animatable_Transitions" (fun target ->
            let target = (target :?> Animatable)

            if target.Transitions = null then
                let newColl = Transitions()
                target.Transitions <- newColl
                newColl
            else
                target.Transitions)

[<Extension>]
type AnimatableModifiers =
    /// <summary>Set the Clock property.</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The Clock value</param>
    [<Extension>]
    static member inline clock(this: WidgetBuilder<'msg, #IFabAnimatable>, value: IClock) =
        this.AddScalar(Animatable.Clock.WithValue(value))

    /// <summary>Set the Transitions property.</summary>
    /// <param name="this">Current widget</param>
    /// <example>
    /// <code lang="fsharp">
    /// Border()
    ///     .background(Brushes.Red)
    ///     .transitions() {
    ///         BrushTransition(Border.BackgroundProperty, TimeSpan.FromSeconds(0.5))
    ///     }
    /// </code>
    /// </example>
    [<Extension>]
    static member inline transitions(this: WidgetBuilder<'msg, #IFabAnimatable>) =
        AttributeCollectionBuilder<'msg, #IFabAnimatable, IFabTransition>(this, Animatable.Transitions)

[<Extension>]
type AnimatableCollectionBuilderExtensions =
    [<Extension>]
    static member inline Yield<'msg, 'marker, 'itemType when 'marker :> IFabAnimatable and 'itemType :> IFabTransition>
        (
            _: AttributeCollectionBuilder<'msg, 'marker, IFabTransition>,
            x: WidgetBuilder<'msg, 'itemType>
        ) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }

    [<Extension>]
    static member inline Yield<'msg, 'marker, 'itemType when 'marker :> IFabAnimatable and 'itemType :> IFabTransition>
        (
            _: AttributeCollectionBuilder<'msg, 'marker, IFabTransition>,
            x: WidgetBuilder<'msg, Memo.Memoized<'itemType>>
        ) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }
