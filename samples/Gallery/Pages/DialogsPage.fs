namespace Gallery.Pages

open System
open System.ComponentModel
open System.Threading
open System.Threading.Tasks
open Avalonia
open Avalonia.Controls
open Avalonia.Dialogs
open Avalonia.Input
open Avalonia.Platform.Storage
open Fabulous.Avalonia
open Avalonia.Platform.Storage
open Avalonia.Platform.Storage.FileIO

open type Fabulous.Avalonia.View
// https://github.com/AvaloniaUI/Avalonia/blob/65b3994d871f93757869dede2e945693b22c4060/samples/ControlCatalog/Pages/DialogsPage.xaml
module DialogsPage =
    type Model =
        { UseFilters: bool
          ForceManagedDialog: bool
          OpenMultiple: bool
          CurrentFolderBox: WellKnownFolder seq
          PickerLastResultsVisible: string
          BookmarkContainer: string }

    type Msg =
        | DecoratedWindow
        | DecoratedWindowDialog
        | Dialog
        | DialogNoTaskbar
        | OwnedWindow
        | OwnedWindowNoTaskbar
        | OpenMultiple
        | SelectFolder
        | OpenFile
        | SaveFile
        | OpenFileBookmark
        | OpenFolderBookmark
        | SelectBoth

        | UseFiltersChanged of bool
        | ForceManagedDialogChanged of bool
        | OpenMultipleChanged of bool
        | CurrentFolderBoxLoaded of bool

        | BookmarkContainerChanged of string
        | OpenedFileContentChanged of string

    let getWellKnownFolder = WellKnownFolder.GetValues<WellKnownFolder>() |> Array.toSeq

    let init () =
        { UseFilters = false
          ForceManagedDialog = false
          OpenMultiple = false
          CurrentFolderBox = getWellKnownFolder
          PickerLastResultsVisible = ""
          BookmarkContainer = "" }

    let getWindow () : Window =
        let window = (Application.Current :?> FabApplication).MainWindow
        let topLevel = TopLevel.GetTopLevel(window) :?> Window

        if window = null then
            raise(NullReferenceException("Invalid Owner"))
        else
            topLevel

    let getTopLevel () : TopLevel =
        let window = (Application.Current :?> FabApplication).MainWindow
        let topLevel = TopLevel.GetTopLevel(window)

        if topLevel <> null then
            topLevel
        else
            raise(NullReferenceException("Invalid Owner"))

    let getStorageProvider () : IStorageProvider = getTopLevel().StorageProvider

    let getItemsAsync (_: string) (_: CancellationToken) : Task<seq<obj>> =
        task {
            let _ = getStorageProvider().TryGetWellKnownFolderAsync(WellKnownFolder.Desktop)

            return
                seq {
                    "Async Item 1"
                    "Async Item 2"
                    "Async Item 3"
                    "Async Product 1"
                    "Async Product 2"
                    "Async Product 3"
                }
        }

    let update msg model =
        match msg with
        | DecoratedWindow -> model
        | DecoratedWindowDialog -> model
        | Dialog -> model
        | DialogNoTaskbar -> model
        | OwnedWindow -> model
        | OwnedWindowNoTaskbar -> model
        | SelectBoth -> model
        | UseFiltersChanged b -> { model with UseFilters = b }
        | ForceManagedDialogChanged b -> { model with ForceManagedDialog = b }
        | OpenMultipleChanged b -> { model with OpenMultiple = b }
        | SelectFolder -> model
        | OpenFile -> model
        | SaveFile -> model
        | OpenFileBookmark -> model
        | OpenFolderBookmark -> model
        | OpenMultiple -> model
        | CurrentFolderBoxLoaded _ -> model
        | BookmarkContainerChanged s -> { model with BookmarkContainer = s }
        | OpenedFileContentChanged s ->
            { model with
                PickerLastResultsVisible = s }

    let view model =
        VStack(spacing = 15.) {
            TextBlock("Windows:")

            Expander(
                "Dialogs",
                VStack(4.) {
                    Button("Decorated window", DecoratedWindow)
                    Button("Decorated window (dialog)", DecoratedWindowDialog)
                    Button("Dialog", Dialog)
                    Button("Dialog (No taskbar icon)", DialogNoTaskbar)
                    Button("Owned window", OwnedWindow)
                    Button("Owned window (No taskbar icon)", OwnedWindowNoTaskbar)
                }
            )

            TextBlock("Pickers:").margin(0., 20., 0., 0.)

            CheckBox("Use filters", model.UseFilters, UseFiltersChanged)

            Expander(
                "FilePicker API",
                VStack(4.) {
                    CheckBox("Force managed dialog", model.ForceManagedDialog, ForceManagedDialogChanged)
                    CheckBox("Open multiple", model.OpenMultiple, OpenMultipleChanged)
                    Button("Select Folder", SelectFolder)
                    Button("Open File", OpenFile)
                    Button("Save File", SaveFile)
                    Button("Open File Bookmark", OpenFileBookmark)
                    Button("Open Folder Bookmark", OpenFolderBookmark)
                }
            )

            Expander(
                "Legacy OpenFileDialog",
                VStack(4.) {
                    Button("OpenFile", OpenFile)
                    Button("OpenMultipleFiles", OpenMultiple)
                    Button("SaveFile", SaveFile)
                    Button("SelectFolder", SelectFolder)
                    Button("OpenBoth", SelectBoth)
                }
            )

            AutoCompleteBox("Write full path/uri or well known folder name", model.CurrentFolderBox)
                .onLoaded(CurrentFolderBoxLoaded)

            TextBlock("Last picker results:").isVisible(false)

            // ItemsControl

            TextBox("", BookmarkContainerChanged).watermark("Select a folder or file")

            TextBox("", OpenedFileContentChanged)
                .watermark("Picked file content")
                .maxLines(10)

        }
