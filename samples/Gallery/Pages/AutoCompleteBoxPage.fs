namespace Gallery.Pages

open System
open System.Threading
open System.Threading.Tasks
open Avalonia.Controls
open Avalonia.Data
open Avalonia.Data.Converters
open Fabulous
open Fabulous.Avalonia

open type Fabulous.Avalonia.View

module AutoCompleteBoxPage =

    type StateData =
        { Name: string
          Abbreviation: string
          Capital: string }

        override this.ToString() = this.Name

    type Model =
        { IsOpen: bool
          SelectedItem: string
          Text: string
          Items: string seq
          Capitals: StateData seq
          Custom: string seq }

    type Msg =
        | TextChanged of string
        | SelectionChanged of SelectionChangedEventArgs
        | OnPopulating of string
        | OnPopulated of System.Collections.IEnumerable
        | OnDropDownOpen of bool
        | MultiBindingLoaded of bool
        | CustomAutoBoxLoaded of bool

    let multiBindingBoxRef = ViewRef<AutoCompleteBox>()

    let customAutoCompleteBoxRef = ViewRef<AutoCompleteBox>()

    let init () =
        { IsOpen = false
          SelectedItem = "Item 2"
          Text = ""
          Items = [ "Item 1"; "Item 2"; "Item 3"; "Product 1"; "Product 2"; "Product 3" ]
          Capitals =
            seq {
                { Name = "Arkansas"
                  Abbreviation = "AR"
                  Capital = "Little Rock" }

                { Name = "California"
                  Abbreviation = "CA"
                  Capital = "Sacramento" }

                { Name = "Colorado"
                  Abbreviation = "CO"
                  Capital = "Denver" }

                { Name = "Connecticut"
                  Abbreviation = "CT"
                  Capital = "Hartford" }

                { Name = "Delaware"
                  Abbreviation = "DE"
                  Capital = "Dover" }

                { Name = "Florida"
                  Abbreviation = "FL"
                  Capital = "Tallahassee" }

                { Name = "Georgia"
                  Abbreviation = "GA"
                  Capital = "Atlanta" }

                { Name = "Hawaii"
                  Abbreviation = "HI"
                  Capital = "Honolulu" }

                { Name = "Idaho"
                  Abbreviation = "ID"
                  Capital = "Boise" }

                { Name = "Illinois"
                  Abbreviation = "IL"
                  Capital = "Springfield" }

                { Name = "Indiana"
                  Abbreviation = "IN"
                  Capital = "Indianapolis" }

                { Name = "Iowa"
                  Abbreviation = "IA"
                  Capital = "Des Moines" }

                { Name = "Kansas"
                  Abbreviation = "KS"
                  Capital = "Topeka" }

                { Name = "Kentucky"
                  Abbreviation = "KY"
                  Capital = "Frankfort" }

                { Name = "Louisiana"
                  Abbreviation = "LA"
                  Capital = "Baton Rouge" }

                { Name = "Maine"
                  Abbreviation = "ME"
                  Capital = "Augusta" }

                { Name = "Maryland"
                  Abbreviation = "MD"
                  Capital = "Annapolis" }

                { Name = "Massachusetts"
                  Abbreviation = "MA"
                  Capital = "Boston" }

                { Name = "Michigan"
                  Abbreviation = "MI"
                  Capital = "Lansing" }

                { Name = "Minnesota"
                  Abbreviation = "MN"
                  Capital = "St. Paul" }

                { Name = "Mississippi"
                  Abbreviation = "MS"
                  Capital = "Jackson" }

                { Name = "Missouri"
                  Abbreviation = "MO"
                  Capital = "Jefferson City" }

                { Name = "Montana"
                  Abbreviation = "MT"
                  Capital = "Helena" }

                { Name = "Nebraska"
                  Abbreviation = "NE"
                  Capital = "Lincoln" }

                { Name = "Nevada"
                  Abbreviation = "NV"
                  Capital = "Carson City" }

                { Name = "New Hampshire"
                  Abbreviation = "NH"
                  Capital = "Concord" }

                { Name = "New Jersey"
                  Abbreviation = "NJ"
                  Capital = "Trenton" }

                { Name = "New Mexico"
                  Abbreviation = "NM"
                  Capital = "Santa Fe" }

                { Name = "New York"
                  Abbreviation = "NY"
                  Capital = "Albany" }

                { Name = "North Carolina"
                  Abbreviation = "NC"
                  Capital = "Raleigh" }

                { Name = "North Dakota"
                  Abbreviation = "ND"
                  Capital = "Bismarck" }

                { Name = "Ohio"
                  Abbreviation = "OH"
                  Capital = "Columbus" }

                { Name = "Oklahoma"
                  Abbreviation = "OK"
                  Capital = "Oklahoma City" }

                { Name = "Oregon"
                  Abbreviation = "OR"
                  Capital = "Salem" }

                { Name = "Pennsylvania"
                  Abbreviation = "PA"
                  Capital = "Harrisburg" }

                { Name = "Rhode Island"
                  Abbreviation = "RI"
                  Capital = "Providence" }

                { Name = "South Carolina"
                  Abbreviation = "SC"
                  Capital = "Columbia" }

                { Name = "South Dakota"
                  Abbreviation = "SD"
                  Capital = "Pierre" }

                { Name = "Tennessee"
                  Abbreviation = "TN"
                  Capital = "Nashville" }

                { Name = "Texas"
                  Abbreviation = "TX"
                  Capital = "Austin" }

                { Name = "Utah"
                  Abbreviation = "UT"
                  Capital = "Salt Lake City" }

                { Name = "Vermont"
                  Abbreviation = "VT"
                  Capital = "Montpelier" }

                { Name = "Virginia"
                  Abbreviation = "VA"
                  Capital = "Richmond" }

                { Name = "Washington"
                  Abbreviation = "WA"
                  Capital = "Olympia" }

                { Name = "West Virginia"
                  Abbreviation = "WV"
                  Capital = "Charleston" }

                { Name = "Wisconsin"
                  Abbreviation = "WI"
                  Capital = "Madison" }

                { Name = "Wyoming"
                  Abbreviation = "WY"
                  Capital = "Cheyenne" }
            }
          Custom = [] }

    let buildAllSentences () =
        [ "Hello world"
          "No this is Patrick"
          "Never gonna give you up"
          "How does one patch KDE2 under FreeBSD" ]
        |> List.map(fun x -> x.Split(' '))
        |> List.toArray

    let lastWordContains (searchText: string, item: string) =
        let words =
            if searchText = null then
                Array.Empty<string>()
            else
                searchText.Split(' ')

        let options = buildAllSentences() |> Array.collect id

        for i in 0 .. words.Length do
            if (i < words.Length) then
                let word = words[i]

                if (word <> null) then
                    for j in 0 .. options.Length do
                        if word <> null && j < options.Length then
                            let option = options.[j]

                            if (option <> null) then

                                if (i = words.Length - 1) then

                                    options[j] <-
                                        if option.ToLower().Contains(word.ToLower()) then
                                            option
                                        else
                                            null

                                else
                                    let foo = option.Equals(word, StringComparison.InvariantCultureIgnoreCase)
                                    options[j] <- if foo then option else null

        options |> Array.exists(fun x -> x <> null && x = item)


    let appendWord (text: string, item: string) =
        if item <> null then
            let parts =
                if text <> null then
                    text.Split(' ')
                else
                    Array.Empty<string>()

            if (parts.Length = 0) then
                item
            else
                parts[parts.Length - 1] <- item
                String.Join(" ", parts)
        else
            String.Empty


    let update msg model =
        match msg with
        | TextChanged s -> { model with Text = s }
        | SelectionChanged _ -> model
        | OnPopulating _ -> model
        | OnPopulated _ -> model
        | OnDropDownOpen isOpen -> { model with IsOpen = isOpen }
        | MultiBindingLoaded _ ->
            let converter =
                FuncMultiValueConverter<string, string>(fun parts ->
                    let parts = parts |> Seq.toArray
                    let first = parts.[0]
                    let second = parts.[1]
                    String.Format("{0} ({1})", first, second))

            let binding = MultiBinding()
            binding.Converter <- converter
            binding.Bindings.Add(Binding("Name"))
            binding.Bindings.Add(Binding("Abbreviation"))

            multiBindingBoxRef.Value.ValueMemberBinding <- binding
            model

        | CustomAutoBoxLoaded _ ->
            let strings = buildAllSentences() |> Array.concat
            customAutoCompleteBoxRef.Value.ItemsSource <- strings
            customAutoCompleteBoxRef.Value.TextFilter <- AutoCompleteFilterPredicate(fun searchText item -> lastWordContains(searchText, item))
            customAutoCompleteBoxRef.Value.TextSelector <- AutoCompleteSelector(fun searchText item -> appendWord(searchText, item))
            model

    let getItemsAsync (_: string) (_: CancellationToken) : Task<seq<obj>> =
        task {
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

    let view model =
        VStack() {
            TextBlock("A control into which the user can input text")

            UniformGrid() {
                VStack() {
                    TextBlock("MinimumPrefixLength: 1")

                    AutoCompleteBox("", model.Capitals)
                        .waterMark("Select an item")
                        .minimumPrefixLength(1)
                }

                VStack() {
                    TextBlock("MinimumPrefixLength: 3")

                    AutoCompleteBox("", model.Items)
                        .minimumPrefixLength(3)
                        .waterMark("Select an item")
                }

                VStack() {
                    TextBlock("MinimumPopulateDelay: 1s")

                    AutoCompleteBox("", model.Items)
                        .waterMark("Select an item")
                        .minimumPopulateDelay(TimeSpan.FromSeconds(1.0))
                }

                VStack() {
                    TextBlock("MaxDropDownHeight: 60")

                    AutoCompleteBox("", model.Items)
                        .waterMark("Select an item")
                        .maxDropDownHeight(60.0)
                }

                VStack() {
                    TextBlock("Watermark")
                    AutoCompleteBox("", model.Items).waterMark("Select an item")
                }

                VStack() {
                    TextBlock("Disabled")
                    AutoCompleteBox("", model.Items).waterMark("Select an item").isEnabled(false)
                }

                VStack() {
                    TextBlock("Multi-Binding")

                    AutoCompleteBox("", model.Capitals)
                        .waterMark("Select an item")
                        .reference(multiBindingBoxRef)
                        .filterMode(AutoCompleteFilterMode.Contains)
                        .onLoaded(MultiBindingLoaded)
                }

                VStack() {
                    TextBlock("AsyncBox")

                    AutoCompleteBox("", getItemsAsync)
                        .waterMark("Select an item")
                        .filterMode(AutoCompleteFilterMode.Contains)
                }

                VStack() {
                    TextBlock("Custom AutoComplete")

                    AutoCompleteBox("", model.Custom)
                        .waterMark("Select an item")
                        .reference(customAutoCompleteBoxRef)
                        .filterMode(AutoCompleteFilterMode.None)
                        .onLoaded(CustomAutoBoxLoaded)
                }

                VStack() {
                    TextBlock("With Validation Errors")

                    AutoCompleteBox("", model.Items)
                        .waterMark("Select an item")
                        .name("ValidationErrors")
                        .filterMode(AutoCompleteFilterMode.None)
                        .dataValidationErrors([ Exception() ])
                }
            }
        }
