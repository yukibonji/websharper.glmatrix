#load "tools/includes.fsx"
open IntelliFactory.Build

let bt =
    BuildTool().PackageId("WebSharper.GlMatrix", "3.0")
        .References(fun r -> [r.Assembly "System.Web"])
    |> fun bt -> bt.WithFramework(bt.Framework.Net40)

let main =
    bt.WebSharper.Extension("WebSharper.GlMatrix")
        .Embed(["gl-matrix-min.js"])
        .SourcesFromProject()

let test =
    bt.WebSharper.HtmlWebsite("WebSharper.GlMatrix.Tests")
        .SourcesFromProject()
        .References(fun r -> [r.Project main])

bt.Solution [
    main
    test

    bt.NuGet.CreatePackage()
        .Configure(fun c ->
            { c with
                Title = Some "WebSharper.GlMatrix-2.2.0"
                LicenseUrl = Some "http://websharper.com/licensing"
                ProjectUrl = Some "https://github.com/intellifactory/websharper.glmatrix"
                Description = "WebSharper Extensions for glMatrix 2.2.0"
                RequiresLicenseAcceptance = true })
        .Add(main)

]
|> bt.Dispatch
