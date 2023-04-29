namespace Gallery.Pages

open System
open Avalonia
open Avalonia.Styling
open Avalonia.Media
open Fabulous.Avalonia
open Fabulous

open type Fabulous.Avalonia.View

module Animations1 =
    type Model = { Value: int }

    type Msg = DoNoting

    let init () = { Value = 0 }

    let update msg model =
        match msg with
        | DoNoting -> model

    let borderTestStyle (this: WidgetBuilder<'msg, IFabBorder>) =
        this
            .child(
                Image(ImageSource.fromString "avares://Gallery/Assets/Icons/fabulous-icon.png", Stretch.UniformToFill)
                    .margin(5)
            )
            .size(200., 200.)
            .borderThickness(2.)
            .borderBrush(SolidColorBrush(Colors.Black))

    let view _ =
        Grid() {
            Border()
                .style(borderTestStyle)
                .renderTransform(Rotate3DTransform(0., 0., 0., 0., 0., -100, 200.))
                .background(SolidColorBrush(Colors.DarkRed))
                .styles() {
                Animations() {
                    (Animation(TimeSpan.FromSeconds(3.)) {
                        KeyFrames(
                            [ Setter(Rotate3DTransform.AngleXProperty, 0.)
                              Setter(Visual.ZIndexProperty, 4) ]
                        )
                            .cue(0.)

                        KeyFrames(
                            [ Setter(Rotate3DTransform.AngleXProperty, 90.)
                              Setter(Visual.ZIndexProperty, 1) ]
                        )
                            .cue(0.25)

                        KeyFrames(
                            [ Setter(Rotate3DTransform.AngleXProperty, 360.)
                              Setter(Visual.ZIndexProperty, 4) ]
                        )
                            .cue(1.)
                    })
                        .repeatForever()
                }
            }

            Border()
                .style(borderTestStyle)
                .renderTransform(Rotate3DTransform(0., 0., 0., 0., 0., -100, 200.))
                .background(SolidColorBrush(Colors.DarkGreen))
                .gridRow(0)
                .gridColumn(0)
                .styles() {
                Animations() {
                    (Animation(TimeSpan.FromSeconds(3.)) {
                        KeyFrame(Rotate3DTransform.AngleXProperty, 90.).cue(0.)
                        KeyFrame(Visual.ZIndexProperty, 1).cue(0.)
                        KeyFrame(Rotate3DTransform.AngleXProperty, 180.).cue(0.25)
                        KeyFrame(Visual.ZIndexProperty, 1).cue(0.25)
                        KeyFrame(Rotate3DTransform.AngleXProperty, 360.).cue(0.75)
                        KeyFrame(Visual.ZIndexProperty, 4).cue(0.75)
                        KeyFrame(Rotate3DTransform.AngleXProperty, 450.).cue(1.)
                        KeyFrame(Visual.ZIndexProperty, 1).cue(1.)
                    })
                        .repeatForever()
                }
            }

            Border()
                .style(borderTestStyle)
                .renderTransform(Rotate3DTransform(0., 0., 0., 0., 0., -100, 200.))
                .background(SolidColorBrush(Colors.DarkBlue))
                .gridRow(0)
                .gridColumn(0)
                .styles() {
                Animations() {
                    (Animation(TimeSpan.FromSeconds(3.)) {
                        KeyFrame(Rotate3DTransform.AngleXProperty, 180.).cue(0.)
                        KeyFrame(Visual.ZIndexProperty, 1).cue(0.)
                        KeyFrame(Rotate3DTransform.AngleXProperty, 360.).cue(0.5)
                        KeyFrame(Visual.ZIndexProperty, 4).cue(0.5)
                        KeyFrame(Rotate3DTransform.AngleXProperty, 450.).cue(0.75)
                        KeyFrame(Visual.ZIndexProperty, 1).cue(0.75)
                        KeyFrame(Rotate3DTransform.AngleXProperty, 540.).cue(1.)
                        KeyFrame(Visual.ZIndexProperty, 1).cue(1.)
                    })
                        .repeatForever()
                }
            }

            Border()
                .style(borderTestStyle)
                .renderTransform(Rotate3DTransform(0., 0., 0., 0., 0., -100, 200.))
                .background(SolidColorBrush(Colors.Orange))
                .gridRow(0)
                .gridColumn(0)
                .styles() {
                Animations() {
                    (Animation(TimeSpan.FromSeconds(3.)) {
                        KeyFrame(Rotate3DTransform.AngleXProperty, 270.).cue(0.)
                        KeyFrame(Visual.ZIndexProperty, 1).cue(0.)
                        KeyFrame(Rotate3DTransform.AngleXProperty, 360.).cue(0.25)
                        KeyFrame(Visual.ZIndexProperty, 4).cue(0.25)
                        KeyFrame(Rotate3DTransform.AngleXProperty, 450.).cue(0.5)
                        KeyFrame(Visual.ZIndexProperty, 1).cue(0.5)
                        KeyFrame(Rotate3DTransform.AngleXProperty, 630.).cue(1.)
                        KeyFrame(Visual.ZIndexProperty, 1).cue(1.)
                    })
                        .repeatForever()
                }
            }
        }
