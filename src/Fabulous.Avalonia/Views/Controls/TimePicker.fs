namespace Fabulous.Avalonia

open System
open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous

type IFabTimePicker =
    inherit IFabTemplatedControl

module TimePicker =
    let WidgetKey = Widgets.register<TimePicker>()

    let ClockIdentifier =
        Attributes.defineAvaloniaPropertyWithEquality TimePicker.ClockIdentifierProperty

    let MinuteIncrement =
        Attributes.defineAvaloniaPropertyWithEquality TimePicker.MinuteIncrementProperty

    let SelectedTime =
        Attributes.defineAvaloniaPropertyWithEquality TimePicker.SelectedTimeProperty

    let SelectedTimeChanged =
        Attributes.defineAvaloniaPropertyWithChangedEvent "TimePicker_SelectedTimeChanged" TimePicker.SelectedTimeProperty Nullable Nullable.op_Explicit

[<AutoOpen>]
module TimePickerBuilders =
    type Fabulous.Avalonia.View with

        static member inline TimePicker(time: TimeSpan, onValueChanged: TimeSpan -> 'msg) =
            WidgetBuilder<'msg, IFabTimePicker>(
                TimePicker.WidgetKey,
                TimePicker.SelectedTime.WithValue(time),
                TimePicker.SelectedTimeChanged.WithValue(ValueEventData.create time (fun args -> onValueChanged args |> box))
            )

[<Extension>]
type TimePickerModifiers =

    [<Extension>]
    static member inline clockIdentifier(this: WidgetBuilder<'msg, #IFabTimePicker>, value: string) =
        this.AddScalar(TimePicker.ClockIdentifier.WithValue(value))

    [<Extension>]
    static member inline minuteIncrement(this: WidgetBuilder<'msg, #IFabTimePicker>, value: int) =
        this.AddScalar(TimePicker.MinuteIncrement.WithValue(value))

    /// <summary>Link a ViewRef to access the direct TimePicker control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabTimePicker>, value: ViewRef<TimePicker>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))

[<Extension>]
type TimePickerExtraModifiers =
    [<Extension>]
    static member inline use24HourClock(this: WidgetBuilder<'msg, #IFabTimePicker>, value: bool) =
        this.AddScalar(TimePicker.ClockIdentifier.WithValue(if value then "24HourClock" else "12HourClock"))