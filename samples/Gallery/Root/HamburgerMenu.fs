namespace Gallery.Root

open Avalonia
open Avalonia.Controls

open Avalonia.Controls.Primitives
open Avalonia.Layout
open Avalonia.Media
open Fabulous
open Fabulous.StackAllocatedCollections
open Fabulous.StackAllocatedCollections.StackList
open Fabulous.Avalonia
open Gallery
open Types
open System
open Avalonia.Animation.Easings

open type Fabulous.Avalonia.View

[<AutoOpen>]
module EmptyPanelBuilders =
    type Fabulous.Avalonia.View with

        static member EmptyPanel<'msg>() =
            WidgetBuilder<'msg, IFabPanel>(Panel.WidgetKey, AttributesBundle(StackList.empty(), ValueNone, ValueNone))

module HamburgerMenu =
    let createListItem (text: string, isSelected: bool) =
        ListBoxItem(text, isSelected)
            .verticalContentAlignment(VerticalAlignment.Center)
            .horizontalAlignment(HorizontalAlignment.Stretch)
            .verticalAlignment(VerticalAlignment.Stretch)
            .fontSize(14.)
            .fontWeight(FontWeight.Normal)
            .minHeight(0.)
            .height(40.)
            .background(Brushes.Transparent)
            .padding(12., 0., 4., 0.)
            .margin(4., 0., 8., 0.)
            .cornerRadius(8.)
            .clipToBounds(false)

    let settingsButtonStyle (this: WidgetBuilder<'msg, IFabButton>) =
        this
            .horizontalContentAlignment(HorizontalAlignment.Stretch)
            .verticalContentAlignment(VerticalAlignment.Center)
            .horizontalAlignment(HorizontalAlignment.Stretch)
            .verticalAlignment(VerticalAlignment.Stretch)
            .fontWeight(FontWeight.Normal)
            .minHeight(0.)
            .height(36.)
            .background(Brushes.Transparent)
            .padding(12., 0., 4., 0.)
            .margin(4., 0., 8., 0.)
            .cornerRadius(8.)
            .clipToBounds(false)

    let paneContent model =
        Grid(rowdefs = [ Auto; Star; Auto ], coldefs = [ Star ]) {
            View.EmptyPanel().height(36)

            (ListBox() {
                createListItem("AcrylicPage", false)
                createListItem("AdornerLayerPage", false)
                createListItem("AutoCompleteBoxPage", false)
                createListItem("ButtonsPage", false)
                createListItem("BrushesPage", false)
                createListItem("ButtonSpinnerPage", false)
                createListItem("BorderPage", false)
                createListItem("CalendarPage", false)
                createListItem("CalendarDatePickerPage", false)
                createListItem("CanvasPage", false)
                createListItem("CheckBoxPage", false)
                createListItem("CarouselPage", false)
                createListItem("ComboBoxPage", false)
                createListItem("CompositionPage", false)
                createListItem("ContextMenuPage", false)
                createListItem("ContextFlyoutPage", false)
                createListItem("ClippingPage", false)
                createListItem("ClipboardPage", false)
                createListItem("DockPanelPage", false)
                createListItem("DialogsPage", false)
                createListItem("DragAndDropPage", false)
                createListItem("DropDownButtonPage", false)
                createListItem("DrawingPage", false)
                createListItem("EffectsPage", false)
                createListItem("ExpanderPage", false)
                createListItem("FlyoutPage", false)
                createListItem("GesturesPage", false)
                createListItem("GeometriesPage", false)
                createListItem("GridPage", false)
                createListItem("GridSplitterPage", false)
                createListItem("ImagePage", false)
                createListItem("LabelPage", false)
                createListItem("LayoutTransformControlPage", false)
                createListItem("LineBoundsDemoControlPage", false)
                createListItem("ListBoxPage", false)
                createListItem("MenuFlyoutPage", false)
                createListItem("MaskedTextBoxPage", false)
                createListItem("MenuPage", false)
                createListItem("NumericUpDownPage", false)
                createListItem("NotificationsPage", false)
                createListItem("OpenGLPage", false)
                createListItem("ProgressBarPage", false)
                createListItem("PanelPage", false)
                createListItem("PathIconPage", false)
                createListItem("PointersPage", false)
                createListItem("PopupPage", false)
                createListItem("PageTransitionsPage", false)
                createListItem("RepeatButtonPage", false)
                createListItem("RadioButtonPage", false)
                createListItem("RefreshContainerPage", false)
                createListItem("SelectableTextBlockPage", false)
                createListItem("SplitButtonPage", false)
                createListItem("SliderPage", false)
                createListItem("ShapesPage", false)
                createListItem("ScrollBarPage", false)
                createListItem("SplitViewPage", false)
                createListItem("StackPanelPage", false)
                createListItem("StylesPage", false)
                createListItem("ScrollViewerPage", false)
                createListItem("ToggleSplitButtonPage", false)
                createListItem("TextBlockPage", false)
                createListItem("TextBoxPage", false)
                createListItem("TickBarPage", false)
                createListItem("ToggleSwitchPage", false)
                createListItem("ToggleButtonPage", false)
                createListItem("ToolTipPage", false)
                createListItem("TabControlPage", false)
                createListItem("TabStripPage", false)
                createListItem("TransformsPage", false)
                createListItem("ThemeAwarePage", false)
                createListItem("UniformGridPage", false)
                createListItem("ViewBoxPage", false)
            })
                .selectionMode(SelectionMode.Single)
                .onSelectionChanged(OnSelectionChanged)
                .horizontalAlignment(HorizontalAlignment.Stretch)
                .gridRow(1)

            Button("Settings", Settings)
                .style(settingsButtonStyle)
                .gridRow(2)
                .flyout(
                    Flyout(
                        VStack() {
                            (ComboBox() {
                                ComboBoxItem("None")
                                ComboBoxItem("BorderOnly")
                                ComboBoxItem("Full")
                            })
                                .horizontalAlignment(HorizontalAlignment.Stretch)
                                .placeholderText("Decorations")
                                .onSelectionChanged(DecorationsOnSelectionChanged)

                            ComboBox(model.ThemeVariants, (fun i -> TextBlock(i.ToString())))
                                .horizontalAlignment(HorizontalAlignment.Stretch)
                                .placeholderText("Themes")
                                .onSelectionChanged(ThemeVariantsOnSelectionChanged)

                            ComboBox(model.FlowDirections, (fun x -> TextBlock(x.ToString())))
                                .horizontalAlignment(HorizontalAlignment.Stretch)
                                .placeholderText("FlowDirections")
                                .onSelectionChanged(FlowDirectionsOnSelectionChanged)

                            (ComboBox() {
                                ComboBoxItem("Fluent")
                                ComboBoxItem("Simple")
                            })
                                .horizontalAlignment(HorizontalAlignment.Stretch)
                                .selectedIndex(0)

                            ComboBox(model.TransparencyLevels, (fun x -> TextBlock(x.ToString())))
                                .horizontalAlignment(HorizontalAlignment.Stretch)
                                .placeholderText("TransparencyLevels")
                                .onSelectionChanged(TransparencyLevelsOnSelectionChanged)

                            (ComboBox() {
                                ComboBoxItem("Normal")
                                ComboBoxItem("Minimized")
                                ComboBoxItem("Maximized")
                                ComboBoxItem("FullScreen")
                            })
                                .horizontalAlignment(HorizontalAlignment.Stretch)
                                .selectedIndex(0)
                        }
                    )
                        .showMode(FlyoutShowMode.Standard)
                        .placement(PlacementMode.RightEdgeAlignedTop)
                )
        }

    let mainView model =
        Grid() {
            SplitView(
                paneContent(model),
                (Dock() {
                    let headerLeftMargin = if model.IsPanOpen then 12. else 52.

                    Border(
                        TextBlock(model.HeaderText)
                            .classes([ "h3" ])
                            .verticalAlignment(VerticalAlignment.Center)
                            .margin(Thickness(headerLeftMargin, 0., 0., 0.))
                            .transition(
                                ThicknessTransition(TextBlock.MarginProperty, TimeSpan.FromSeconds(1.))
                                    .easing(Easing.Parse("0.1, 0.9, 0.2, 1.0"))
                            )
                    )
                        .dock(Dock.Top)
                        .background(Brushes.Transparent)

                    let cornerRadius = if model.IsPanOpen then 8. else 0.

                    Border(
                        Border(
                            match model.Navigation.CurrentPage with
                            // ScrollBarPageModel does not work when wrapped in a ScrollViewer
                            | ScrollBarPageModel _ -> AnyView(NavigationState.view SubpageMsg model.Navigation.CurrentPage)
                            | _ ->
                                AnyView(
                                    ScrollViewer(NavigationState.view SubpageMsg model.Navigation.CurrentPage)
                                        .verticalScrollBarVisibility(ScrollBarVisibility.Auto)
                                        .horizontalScrollBarVisibility(ScrollBarVisibility.Auto)
                                        .background(Brushes.Transparent)
                                        .padding(Thickness(16.))
                                )
                        )
                            .margin(Thickness(4., 0., 0., 0.))
                            .cornerRadius(CornerRadius(cornerRadius, 0., 0., 0.))
                            .boxShadow("0 0 1 1 #2000")
                            .transition(CornerRadiusTransition(Border.CornerRadiusProperty, TimeSpan.FromSeconds(1.)))
                    )
                })
                    .background(Brushes.Transparent)
            )
                .isPresented(model.IsPanOpen, OpenPanChanged)
                .displayMode(SplitViewDisplayMode.Inline)
                .panePlacement(SplitViewPanePlacement.Left)
                .paneBackground(Brushes.Transparent)

            ToggleButton(
                model.IsPanOpen,
                OpenPanChanged,
                PathIcon(Paths.Path3)
                    .foreground(ThemeAware.With(Brush.Parse("#99000000"), Brush.Parse("#99FFFFFF")))
            )
                .width(40.)
                .height(32.)
                .margin(4., 2., 0., 0.)
                .padding(0.)
                .horizontalAlignment(HorizontalAlignment.Left)
                .verticalAlignment(VerticalAlignment.Top)
                .horizontalContentAlignment(HorizontalAlignment.Center)
                .cornerRadius(4.)
        }
