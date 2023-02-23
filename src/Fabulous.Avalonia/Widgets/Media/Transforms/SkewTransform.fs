namespace Fabulous.Avalonia

open Avalonia.Media
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabSkewTransform =
    inherit IFabTransform

module SkewTransform =

    let WidgetKey = Widgets.register<SkewTransform>()

    let AngleX =
        Attributes.defineAvaloniaPropertyWithEquality SkewTransform.AngleXProperty

    let AngleY =
        Attributes.defineAvaloniaPropertyWithEquality SkewTransform.AngleYProperty

[<AutoOpen>]
module SkewTransformBuilders =
    type Fabulous.Avalonia.View with

        static member SkewTransform(angleX: float, angleY: float) =
            WidgetBuilder<'msg, IFabSkewTransform>(SkewTransform.WidgetKey, SkewTransform.AngleX.WithValue(angleX), SkewTransform.AngleY.WithValue(angleY))

        static member SkewTransform(angleX: float) =
            WidgetBuilder<'msg, IFabSkewTransform>(SkewTransform.WidgetKey, SkewTransform.AngleX.WithValue(angleX))

        static member SkewTransform() =
            WidgetBuilder<'msg, IFabSkewTransform>(SkewTransform.WidgetKey, AttributesBundle(StackList.empty(), ValueNone, ValueNone))
