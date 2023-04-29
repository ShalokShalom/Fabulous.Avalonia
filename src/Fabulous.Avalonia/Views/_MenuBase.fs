namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous

type IFabMenuBase =
    inherit IFabSelectingItemsControl

module MenuBase =
    let MenuOpened =
        Attributes.defineEvent "MenuBase_MenuOpened" (fun target -> (target :?> MenuBase).MenuOpened)

    let MenuClosed =
        Attributes.defineEvent "MenuBase_MenuClosed" (fun target -> (target :?> MenuBase).MenuClosed)

[<Extension>]
type MenuBaseModifiers =
    [<Extension>]
    static member inline onMenuOpened(this: WidgetBuilder<'msg, #IFabMenuBase>, onMenuOpened: 'msg) =
        this.AddScalar(MenuBase.MenuOpened.WithValue(fun _ -> onMenuOpened |> box))

    /// <summary>Listen for the MenuOpened event</summary>
    /// <param name="this">Current widget</param>
    /// <param name="fn">Function to call when the event is raised</param>
    [<Extension>]
    static member inline onMenuClosed(this: WidgetBuilder<'msg, #IFabMenuBase>, onMenuClosed: 'msg) =
        this.AddScalar(MenuBase.MenuClosed.WithValue(fun _ -> onMenuClosed |> box))
