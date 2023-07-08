namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls.Documents
open Fabulous

type IFabBold =
    inherit IFabSpan

module Bold =
    let WidgetKey = Widgets.register<Bold>()

[<AutoOpen>]
module BoldBuilders =
    type Fabulous.Avalonia.View with

        static member private Bold<'msg>() =
            CollectionBuilder<'msg, IFabBold, IFabInline>(Bold.WidgetKey, Span.Inlines)

        static member Bold<'msg>(text: string) =
            View.Bold<'msg>() { View.Run<'msg>(text) }

[<Extension>]
type BoldModifiers =
    /// <summary>Link a ViewRef to access the direct Bold control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabBold>, value: ViewRef<Bold>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))
