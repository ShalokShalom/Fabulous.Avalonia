namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia
open Avalonia.Controls.Primitives
open Avalonia.Media
open Avalonia.Media.Immutable
open Fabulous

type IFabTemplatedControl =
    inherit IFabControl

module TemplatedControl =
    let BackgroundWidget =
        Attributes.defineAvaloniaPropertyWidget TemplatedControl.BackgroundProperty

    let Background =
        Attributes.defineAvaloniaPropertyWithEquality TemplatedControl.BackgroundProperty

    let BorderBrushWidget =
        Attributes.defineAvaloniaPropertyWidget TemplatedControl.BorderBrushProperty

    let BorderBrush =
        Attributes.defineAvaloniaPropertyWithEquality TemplatedControl.BorderBrushProperty

    let BorderThickness =
        Attributes.defineAvaloniaPropertyWithEquality TemplatedControl.BorderThicknessProperty

    let CornerRadius =
        Attributes.defineAvaloniaPropertyWithEquality TemplatedControl.CornerRadiusProperty

    let FontFamily =
        Attributes.defineAvaloniaPropertyWithEquality TemplatedControl.FontFamilyProperty

    let FontSize =
        Attributes.defineAvaloniaPropertyWithEquality TemplatedControl.FontSizeProperty

    let FontStyle =
        Attributes.defineAvaloniaPropertyWithEquality TemplatedControl.FontStyleProperty

    let FontWeight =
        Attributes.defineAvaloniaPropertyWithEquality TemplatedControl.FontWeightProperty

    let FontStretch =
        Attributes.defineAvaloniaPropertyWithEquality TemplatedControl.FontStretchProperty

    let ForegroundWidget =
        Attributes.defineAvaloniaPropertyWidget TemplatedControl.ForegroundProperty

    let Foreground =
        Attributes.defineAvaloniaPropertyWithEquality TemplatedControl.ForegroundProperty

    let Padding =
        Attributes.defineAvaloniaPropertyWithEquality TemplatedControl.PaddingProperty

[<Extension>]
type TemplatedControlModifiers =
    [<Extension>]
    static member inline background(this: WidgetBuilder<'msg, #IFabTemplatedControl>, widget: WidgetBuilder<'msg, #IFabBrush>) =
        this.AddWidget(TemplatedControl.BackgroundWidget.WithValue(widget.Compile()))

    [<Extension>]
    static member inline background(this: WidgetBuilder<'msg, #IFabTemplatedControl>, value: IBrush) =
        this.AddScalar(TemplatedControl.Background.WithValue(value))

    [<Extension>]
    static member inline background(this: WidgetBuilder<'msg, #IFabTemplatedControl>, value: string) =
        this.AddScalar(TemplatedControl.Background.WithValue(value |> Color.Parse |> ImmutableSolidColorBrush))

    [<Extension>]
    static member inline borderBrush(this: WidgetBuilder<'msg, #IFabTemplatedControl>, widget: WidgetBuilder<'msg, #IFabBrush>) =
        this.AddWidget(TemplatedControl.BorderBrushWidget.WithValue(widget.Compile()))

    [<Extension>]
    static member inline borderBrush(this: WidgetBuilder<'msg, #IFabTemplatedControl>, value: IBrush) =
        this.AddScalar(TemplatedControl.BorderBrush.WithValue(value))

    [<Extension>]
    static member inline borderBrush(this: WidgetBuilder<'msg, #IFabTemplatedControl>, value: string) =
        this.AddScalar(TemplatedControl.BorderBrush.WithValue(value |> Color.Parse |> ImmutableSolidColorBrush))

    [<Extension>]
    static member inline borderThickness(this: WidgetBuilder<'msg, #IFabTemplatedControl>, value: Thickness) =
        this.AddScalar(TemplatedControl.BorderThickness.WithValue(value))

    [<Extension>]
    static member inline cornerRadius(this: WidgetBuilder<'msg, #IFabTemplatedControl>, value: CornerRadius) =
        this.AddScalar(TemplatedControl.CornerRadius.WithValue(value))

    [<Extension>]
    static member inline fontFamily(this: WidgetBuilder<'msg, #IFabTemplatedControl>, value: FontFamily) =
        this.AddScalar(TemplatedControl.FontFamily.WithValue(value))

    [<Extension>]
    static member inline fontSize(this: WidgetBuilder<'msg, #IFabTemplatedControl>, value: float) =
        this.AddScalar(TemplatedControl.FontSize.WithValue(value))

    [<Extension>]
    static member inline fontStyle(this: WidgetBuilder<'msg, #IFabTemplatedControl>, value: FontStyle) =
        this.AddScalar(TemplatedControl.FontStyle.WithValue(value))

    [<Extension>]
    static member inline fontWeight(this: WidgetBuilder<'msg, #IFabTemplatedControl>, value: FontWeight) =
        this.AddScalar(TemplatedControl.FontWeight.WithValue(value))

    [<Extension>]
    static member inline fontStretch(this: WidgetBuilder<'msg, #IFabTemplatedControl>, value: FontStretch) =
        this.AddScalar(TemplatedControl.FontStretch.WithValue(value))

    [<Extension>]
    static member inline foreground(this: WidgetBuilder<'msg, #IFabTemplatedControl>, widget: WidgetBuilder<'msg, #IFabBrush>) =
        this.AddWidget(TemplatedControl.ForegroundWidget.WithValue(widget.Compile()))

    [<Extension>]
    static member inline foreground(this: WidgetBuilder<'msg, #IFabTemplatedControl>, value: IBrush) =
        this.AddScalar(TemplatedControl.Foreground.WithValue(value))

    [<Extension>]
    static member inline foreground(this: WidgetBuilder<'msg, #IFabTemplatedControl>, value: string) =
        this.AddScalar(TemplatedControl.Foreground.WithValue(value |> Color.Parse |> ImmutableSolidColorBrush))

    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabTemplatedControl>, value: Thickness) =
        this.AddScalar(TemplatedControl.Padding.WithValue(value))

[<Extension>]
type TemplatedControlExtraModifiers =
    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabTemplatedControl>, value: float) =
        TemplatedControlModifiers.padding(this, Thickness(value))

    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabTemplatedControl>, horizontal: float, vertical: float) =
        TemplatedControlModifiers.padding(this, Thickness(horizontal, vertical))

    [<Extension>]
    static member inline padding(this: WidgetBuilder<'msg, #IFabTemplatedControl>, left: float, top: float, right: float, bottom: float) =
        TemplatedControlModifiers.padding(this, Thickness(left, top, right, bottom))

    [<Extension>]
    static member inline borderThickness(this: WidgetBuilder<'msg, #IFabTemplatedControl>, value: float) =
        TemplatedControlModifiers.borderThickness(this, Thickness(value))

    [<Extension>]
    static member inline borderThickness(this: WidgetBuilder<'msg, #IFabTemplatedControl>, horizontal: float, vertical: float) =
        TemplatedControlModifiers.borderThickness(this, Thickness(horizontal, vertical))

    [<Extension>]
    static member inline borderThickness(this: WidgetBuilder<'msg, #IFabTemplatedControl>, left: float, top: float, right: float, bottom: float) =
        TemplatedControlModifiers.borderThickness(this, Thickness(left, top, right, bottom))
