namespace Gallery.Pages

open Avalonia.Controls
open Avalonia.Media
open Fabulous.Avalonia
open Fabulous

open type Fabulous.Avalonia.View
open Gallery

module GridSplitterPage =
    type Model = { Nothing: bool }

    type Msg = DoNothing

    type CmdMsg = | NoMsg

    let mapCmdMsgToCmd cmdMsg =
        match cmdMsg with
        | NoMsg -> Cmd.none

    let init () = { Nothing = true }, []

    let update msg model =
        match msg with
        | DoNothing -> model, []

    let view _ =
        VStack(16.) {
            (Grid(coldefs = [ Star; Pixel(4); Star ], rowdefs = [ Star ]) {
                Rectangle()
                    .height(200.)
                    .fill(SolidColorBrush(Colors.Blue))
                    .gridRow(0)
                    .gridColumn(0)

                GridSplitter(GridResizeDirection.Columns)
                    .background(SolidColorBrush(Colors.White))
                    .gridRow(0)
                    .gridColumn(1)

                Rectangle()
                    .height(200.)
                    .fill(SolidColorBrush(Colors.Red))
                    .gridRow(0)
                    .gridColumn(2)
            })
                .height(200.)

            (Grid(coldefs = [ Star ], rowdefs = [ Star; Pixel(4); Star ]) {
                Rectangle()
                    .fill(SolidColorBrush(Colors.Blue))
                    .gridRow(0)
                    .gridColumn(0)

                GridSplitter(GridResizeDirection.Rows)
                    .background(SolidColorBrush(Colors.White))
                    .gridRow(1)
                    .gridColumn(0)

                Rectangle()
                    .fill(SolidColorBrush(Colors.Red))
                    .gridRow(2)
                    .gridColumn(0)
            })
                .height(200.)
        }
