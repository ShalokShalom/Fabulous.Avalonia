namespace Gallery.Pages

open System
open System.Collections.Generic
open System.ComponentModel
open System.Reflection
open System.Threading
open System.Threading.Tasks
open Avalonia
open Avalonia.Controls
open Avalonia.Dialogs
open Avalonia.Input
open Fabulous
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
          CurrentFolderBoxText: string
          PickerLastResultsVisible: string
          BookmarkContainer: string
          IgnoreTextChanged: bool
          LastSelectedDirectory: IStorageFolder
          Results: string seq }

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
        | CurrentFolderBoxChanged of string

        | DialogOpened of string array

    let getWellKnownFolder = WellKnownFolder.GetValues<WellKnownFolder>() |> Array.toSeq

    let init () =
        { UseFilters = false
          ForceManagedDialog = false
          OpenMultiple = false
          CurrentFolderBox = getWellKnownFolder
          PickerLastResultsVisible = ""
          BookmarkContainer = ""
          IgnoreTextChanged = false
          CurrentFolderBoxText = ""
          LastSelectedDirectory = null
          Results = [] }

    let pickerLastResultsRef = ViewRef<TextBlock>()

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

    let getFilters isChecked =
        let fileDialogFilter1 = new FileDialogFilter()
        fileDialogFilter1.Name <- "Text files (.txt)"

        let fileDialogFilter2 = new FileDialogFilter()
        fileDialogFilter2.Name <- "All files"
        fileDialogFilter2.Extensions <- List<string>([ ".*" ])

        if isChecked then
            List<FileDialogFilter>([])
        else
            List<FileDialogFilter>([ fileDialogFilter1; fileDialogFilter2 ])

    let getFileTypes isChecked =
        if not isChecked then
            None
        else
            let item = FilePickerFileType("Binary Log")
            item.Patterns <- [ "*.binlog"; "*.buildlog" ]
            item.MimeTypes <- [ "application/binlog"; "application/buildlog" ]
            item.AppleUniformTypeIdentifiers <- [ "public.data" ]
            Some([ FilePickerFileTypes.All; FilePickerFileTypes.TextPlain; item ])

    let showOpenFile useFilters openMultiple =
        let uri =
            Assembly.GetEntryAssembly().GetModules()
            |> Seq.head
            |> fun m -> m.FullyQualifiedName

        let initialFileName = if uri = null then null else System.IO.Path.GetFileName(uri)

        let initialDirectory =
            if uri = null then
                null
            else
                System.IO.Path.GetDirectoryName(uri)

        let result = new OpenFileDialog()
        result.Title <- "Open file"
        result.Filters <- getFilters(useFilters)
        result.Directory <- initialDirectory
        result.InitialFileName <- initialFileName
        result.AllowMultiple <- openMultiple

        task {
            let! res = result.ShowAsync(getWindow())
            return DialogOpened res
        }

    let update msg model =
        match msg with
        | DecoratedWindow -> model, Cmd.none
        | DecoratedWindowDialog -> model, Cmd.none
        | Dialog -> model, Cmd.none
        | DialogNoTaskbar -> model, Cmd.none
        | OwnedWindow -> model, Cmd.none
        | OwnedWindowNoTaskbar -> model, Cmd.none
        | SelectBoth -> model, Cmd.none
        | UseFiltersChanged b -> { model with UseFilters = b }, Cmd.none
        | ForceManagedDialogChanged b -> { model with ForceManagedDialog = b }, Cmd.none
        | OpenMultipleChanged b -> { model with OpenMultiple = b }, Cmd.none
        | SelectFolder -> model, Cmd.none
        | OpenFile ->
            model, Cmd.ofTaskMsg(showOpenFile model.UseFilters model.OpenMultiple)
        | SaveFile -> model, Cmd.none
        | OpenFileBookmark -> model, Cmd.none
        | OpenFolderBookmark -> model, Cmd.none
        | OpenMultiple -> model, Cmd.none
        | CurrentFolderBoxLoaded _ -> model, Cmd.none
        | BookmarkContainerChanged s -> { model with BookmarkContainer = s }, Cmd.none
        | OpenedFileContentChanged s ->
            { model with
                PickerLastResultsVisible = s }, Cmd.none

        | CurrentFolderBoxChanged text ->
            let folderEnumValid, folderEnum = Enum.TryParse<WellKnownFolder>(text)

            if model.IgnoreTextChanged then
                model, Cmd.none
            else if folderEnumValid then
                let res =
                    getStorageProvider().TryGetWellKnownFolderAsync(folderEnum)
                    |> Async.AwaitTask
                    |> Async.RunSynchronously

                { model with
                    LastSelectedDirectory = res }, Cmd.none
            else
                let folderLinkValid, folderLink = Uri.TryCreate(text, UriKind.Absolute)

                if not folderLinkValid then
                    let _ = Uri.TryCreate("file://" + text, UriKind.Absolute)
                    model, Cmd.none
                else if folderLink <> null then
                    let res =
                        getStorageProvider().TryGetFolderFromPathAsync(folderLink)
                        |> Async.AwaitTask
                        |> Async.RunSynchronously

                    { model with
                        LastSelectedDirectory = res }, Cmd.none
                else
                    model, Cmd.none
        | DialogOpened items -> { model with Results = items }, Cmd.none

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

            AutoCompleteBox(model.CurrentFolderBoxText, CurrentFolderBoxChanged)
                .waterMark("Write full path/uri or well known folder name")
            //.onLoaded(CurrentFolderBoxLoaded)

            TextBlock("Last picker results:")
                .isVisible(false)
                .reference(pickerLastResultsRef)

            ListBox(model.Results, (fun x -> TextBlock(x)))

            TextBox("", BookmarkContainerChanged).watermark("Select a folder or file")

            TextBox("", OpenedFileContentChanged)
                .watermark("Picked file content")
                .maxLines(10)

        }
