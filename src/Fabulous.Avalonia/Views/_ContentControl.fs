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
    /// <summary>Sets the HorizontalContentAlignment property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The value to set.</param>
    /// <example>
    /// <code lang="fsharp">
    /// [&lt;Struct&gt;]
    /// type HorizontalAlignment =
    /// | Stretch = 0
    /// | Left = 1
    /// | Center = 2
    /// | Right = 3
    /// </code>
    /// </example>
    [<Extension>]
    static member inline horizontalContentAlignment(this: WidgetBuilder<'msg, #IFabContentControl>, value: HorizontalAlignment) =
        this.AddScalar(ContentControl.HorizontalContentAlignment.WithValue(value))

    /// <summary>Sets the VerticalContentAlignment property.</summary>
    /// <param name="this">Current widget.</param>
    /// <param name="value">The value to set.</param>
    /// <example>
    /// <code lang="fsharp">
    /// [&lt;Struct&gt;]
    /// type VerticalAlignment =
    /// | Stretch = 0
    /// | Top = 1
    /// | Center = 2
    /// | Bottom = 3
    /// </code>
    /// </example>
    [<Extension>]
    static member inline verticalContentAlignment(this: WidgetBuilder<'msg, #IFabContentControl>, value: VerticalAlignment) =
        this.AddScalar(ContentControl.VerticalContentAlignment.WithValue(value))
