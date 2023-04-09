namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabSplitButton =
    inherit IFabContentControl

module SplitButton =
    let WidgetKey = Widgets.register<SplitButton>()

    let Clicked =
        Attributes.defineEvent "SplitButton_Clicked" (fun target -> (target :?> SplitButton).Click)

    let Flyout = Attributes.defineAvaloniaPropertyWidget SplitButton.FlyoutProperty

[<AutoOpen>]
module SplitButtonBuilders =
    type Fabulous.Avalonia.View with

        static member inline SplitButton<'msg>(text: string, onClicked: 'msg) =
            WidgetBuilder<'msg, IFabSplitButton>(
                SplitButton.WidgetKey,
                ContentControl.ContentString.WithValue(text),
                SplitButton.Clicked.WithValue(fun _ -> box onClicked)
            )

        static member inline SplitButton(onClicked: 'msg, content: WidgetBuilder<'msg, #IFabControl>) =
            WidgetBuilder<'msg, IFabSplitButton>(
                SplitButton.WidgetKey,
                AttributesBundle(
                    StackList.one(SplitButton.Clicked.WithValue(fun _ -> box onClicked)),
                    ValueSome [| ContentControl.ContentWidget.WithValue(content.Compile()) |],
                    ValueNone
                )
            )

[<Extension>]
type SplitButtonModifiers =
    [<Extension>]
    static member inline flyout(this: WidgetBuilder<'msg, #IFabSplitButton>, content: WidgetBuilder<'msg, #IFabFlyoutBase>) =
        this.AddWidget(SplitButton.Flyout.WithValue(content.Compile()))

    /// <summary>Link a ViewRef to access the direct SplitButton control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabSplitButton>, value: ViewRef<SplitButton>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))