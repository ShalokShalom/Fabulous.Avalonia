namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls.Shapes
open Avalonia
open Fabulous

type IFabLine =
    inherit IFabShape

module Line =
    let WidgetKey = Widgets.register<Line>()

    let StartPoint =
        Attributes.defineAvaloniaPropertyWithEquality Line.StartPointProperty

    let EndPoint = Attributes.defineAvaloniaPropertyWithEquality Line.EndPointProperty

[<AutoOpen>]
module LineBuilders =
    type Fabulous.Avalonia.View with

        static member Line(starPoint: Point, endPoint: Point) =
            WidgetBuilder<'msg, IFabLine>(Line.WidgetKey, Line.StartPoint.WithValue(starPoint), Line.EndPoint.WithValue(endPoint))

[<Extension>]
type LineModifiers =
    /// <summary>Link a ViewRef to access the direct Line control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabLine>, value: ViewRef<Line>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))