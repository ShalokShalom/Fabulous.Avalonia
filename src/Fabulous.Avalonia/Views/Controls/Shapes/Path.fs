namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls.Shapes
open Avalonia.Media
open Fabulous
open Fabulous.StackAllocatedCollections
open Fabulous.StackAllocatedCollections.StackList

type IFabPath =
    inherit IFabShape

module Path =
    let WidgetKey = Widgets.register<Path>()

    let DataWidget = Attributes.defineAvaloniaPropertyWidget Path.DataProperty

    let DataString = Attributes.defineAvaloniaPropertyWithEquality Path.DataProperty

[<AutoOpen>]
module PathBuilders =
    type Fabulous.Avalonia.View with

        /// <summary>Creates a Path widget.</summary>
        /// <param name="content">The content of the Path.</param>
        static member Path(content: WidgetBuilder<'msg, #IFabGeometry>) =
            WidgetBuilder<'msg, IFabPath>(
                Path.WidgetKey,
                AttributesBundle(StackList.empty(), ValueSome [| Path.DataWidget.WithValue(content.Compile()) |], ValueNone)
            )

        /// <summary>Creates a Path widget.</summary>
        /// <param name="data">The content of the Path.</param>
        static member Path(data: string) =
            WidgetBuilder<'msg, IFabPath>(Path.WidgetKey, Path.DataString.WithValue(Geometry.Parse(data)))

[<Extension>]
type PathModifiers =
    /// <summary>Link a ViewRef to access the direct Path control instance.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control.</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabPath>, value: ViewRef<Path>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
