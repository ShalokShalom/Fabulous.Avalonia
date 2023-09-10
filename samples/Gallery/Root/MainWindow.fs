namespace Gallery.Root

open Avalonia.Controls

open Avalonia.Media
open Fabulous.Avalonia
open Types
open type Fabulous.Avalonia.View

module MainWindow =
    let createMenu model =
        NativeMenu() {
            NativeMenuItem("Edit")
                .menu(
                    NativeMenu() {
                        NativeMenuItem("Close Pan", DoNothing)
                        NativeMenuItem("Open Pan", DoNothing)
                        NativeMenuItemSeparator()

                        NativeMenuItem("After separator", DoNothing)
                            .toggleType(NativeMenuItemToggleType.CheckBox)
                    }
                )
        }

    let trayIcon () =
        TrayIcon(WindowIcon(ImageSource.fromString "avares://Gallery/Assets/Icons/logo.ico"), "Avalonia Tray Icon Tooltip")
            .menu(
                NativeMenu() {
                    NativeMenuItem("Settings")
                        .menu(
                            NativeMenu() {
                                NativeMenuItem("Option 1", DoNothing)
                                    .toggleType(NativeMenuItemToggleType.Radio)
                                    .isChecked(true)

                                NativeMenuItem("Option 2", DoNothing)
                                    .toggleType(NativeMenuItemToggleType.Radio)
                                    .isChecked(true)

                                NativeMenuItemSeparator()

                                NativeMenuItem("Option 3", DoNothing)
                                    .toggleType(NativeMenuItemToggleType.CheckBox)
                                    .isChecked(true)

                                NativeMenuItem("Restore defaults", DoNothing)
                                    .icon(ImageSource.fromString "avares://Gallery/Assets/Icons/logo.ico")

                                NativeMenuItem("Disabled option", DoNothing)
                                    .isEnabled(false)
                            }
                        )

                    NativeMenuItem("Exit", DoNothing)
                }
            )

    let view (model: Model) =
        DesktopApplication(
            Window(Panel() { HamburgerMenu.mainView model })
                .title("Fabulous Gallery")
                .menu(createMenu model)
                .width(1024.)
                .height(800.)
                .icon(WindowIcon(ImageSource.fromString "avares://Gallery/Assets/Icons/logo.ico"))
        )
            .trayIcon(trayIcon())
            .styles(
                [ "avares://Gallery/Styles/DefaultTheme.xaml"
                  "avares://Gallery/Styles/TextStyles.xaml" ]
            )
//Enable this only for debugging purposes
//.debugOverlays(RendererDebugOverlays.Fps ||| RendererDebugOverlays.DirtyRects)
