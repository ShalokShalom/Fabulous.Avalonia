namespace Gallery.Pages

open Avalonia.Controls.Primitives
open Avalonia.Layout
open Avalonia.Media
open Fabulous.Avalonia
open Fabulous

open type Fabulous.Avalonia.View
open Gallery

module ScrollBarPage =
    type Model = { ScrollValue: float }

    type Msg =
        | ValueChanged of float
        | ScrollBarChanged of ScrollEventArgs

    type CmdMsg = | NoMsg

    let mapCmdMsgToCmd cmdMsg =
        match cmdMsg with
        | NoMsg -> Cmd.none

    let init () = { ScrollValue = 0.0 }, []

    let update msg model =
        match msg with
        | ValueChanged value -> { model with ScrollValue = value }, []
        | ScrollBarChanged _ -> model, []

    let view model =
        VStack(spacing = 15.) {

            TextBlock($"Value: {model.ScrollValue}")

            ScrollBar(1., 240., model.ScrollValue, ValueChanged)
                .orientation(Orientation.Horizontal)
                .allowAutoHide(false)
                .background(SolidColorBrush(Colors.LightSalmon))
                .margin(10., 10., 0., 0.)
                .onScroll(ScrollBarChanged)
        }
