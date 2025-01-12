namespace Fabulous.Avalonia

open Avalonia
open Avalonia.Media
open Fabulous

type IFabLineSegment =
    inherit IFabPathSegment

module LineSegment =
    let WidgetKey = Widgets.register<LineSegment>()

    let Point = Attributes.defineAvaloniaPropertyWithEquality LineSegment.PointProperty

[<AutoOpen>]
module LineSegmentBuilders =

    type Fabulous.Avalonia.View with

        /// <summary>Creates a LineSegment widget.</summary>
        /// <param name="point">The point to draw the line to.</param>
        static member inline LineSegment<'msg>(point: Point) =
            WidgetBuilder<'msg, IFabLineSegment>(LineSegment.WidgetKey, LineSegment.Point.WithValue(point))
