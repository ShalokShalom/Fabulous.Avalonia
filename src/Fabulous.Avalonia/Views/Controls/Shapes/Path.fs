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

        static member Path(content: WidgetBuilder<'msg, #IFabGeometry>) =
            WidgetBuilder<'msg, IFabPath>(
                Path.WidgetKey,
                AttributesBundle(StackList.empty(), ValueSome [| Path.DataWidget.WithValue(content.Compile()) |], ValueNone)
            )

        static member Path(pathData: string) =
            WidgetBuilder<'msg, IFabPath>(Path.WidgetKey, Path.DataString.WithValue(Geometry.Parse(pathData)))

[<Extension>]
type PathModifiers =
    /// <summary>Link a ViewRef to access the direct Path control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabPath>, value: ViewRef<Path>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))