namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Avalonia.Layout
open Fabulous

type IFabContentControl =
    inherit IFabTemplatedControl

module ContentControl =
    let ContentWidget =
        Attributes.defineAvaloniaPropertyWidget ContentControl.ContentProperty

    let ContentString =
        Attributes.defineAvaloniaProperty<string, obj> ContentControl.ContentProperty box ScalarAttributeComparers.equalityCompare

    let HorizontalContentAlignment =
        Attributes.defineAvaloniaPropertyWithEquality ContentControl.HorizontalContentAlignmentProperty

    let VerticalContentAlignment =
        Attributes.defineAvaloniaPropertyWithEquality ContentControl.VerticalContentAlignmentProperty

[<Extension>]
type ContentControlModifiers =
    [<Extension>]
    static member inline horizontalContentAlignment(this: WidgetBuilder<'msg, #IFabContentControl>, value: HorizontalAlignment) =
        this.AddScalar(ContentControl.HorizontalContentAlignment.WithValue(value))

    [<Extension>]
    static member inline verticalContentAlignment(this: WidgetBuilder<'msg, #IFabContentControl>, value: VerticalAlignment) =
        this.AddScalar(ContentControl.VerticalContentAlignment.WithValue(value))
