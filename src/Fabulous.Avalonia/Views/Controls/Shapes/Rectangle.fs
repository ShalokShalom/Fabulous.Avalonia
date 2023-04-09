namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls.Shapes
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabRectangle =
    inherit IFabShape

module Rectangle =
    let WidgetKey = Widgets.register<Rectangle>()

    let RadiusX =
        Attributes.defineAvaloniaPropertyWithEquality Rectangle.RadiusXProperty

    let RadiusY =
        Attributes.defineAvaloniaPropertyWithEquality Rectangle.RadiusYProperty

[<AutoOpen>]
module RectangleBuilders =
    type Fabulous.Avalonia.View with

        static member Rectangle(radiusX: float, radiusY: float) =
            WidgetBuilder<'msg, IFabRectangle>(Rectangle.WidgetKey, Rectangle.RadiusX.WithValue(radiusX), Rectangle.RadiusY.WithValue(radiusY))

        static member Rectangle() =
            WidgetBuilder<'msg, IFabRectangle>(Rectangle.WidgetKey, AttributesBundle(StackList.empty(), ValueNone, ValueNone))

[<Extension>]
type RectangleModifiers =
    /// <summary>Link a ViewRef to access the direct Rectangle control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabRectangle>, value: ViewRef<Rectangle>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))