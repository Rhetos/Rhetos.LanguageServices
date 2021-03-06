# Rhetos Language Service changelog

## v0.9.7-preview

* Refreshing projects in solution after build will no longer impact VS UI

## v0.9.6-preview

* Changed XML documentation display format
* Minor wording changes in LSP warning
* Extension will now monitor solution projects for changes in Rhetos generated files and will refresh projects accordingly to fix IntelliSense
* [BUGFIX] Hover while Visual Studio is loading will no longer generate LSP exception message
* Upgraded to latest Rhetos 4.0 build

## v0.9.5-preview

* Language Server will now use Rhetos project DLLs instead of built application DLLs
* Added asynchronous checking for Rhetos project configuration changes and appropriate warnings
* Completion at parameter positions will now offer all non-keyword tokens found in current document
* Completion will now correctly handle concepts with `ConceptParentAttribute`
* Configuration keys for overriding Rhetos project location have been changed to `rhetosProjectRootPath` (from `rhetosAppRootPath`)
* [BUGFIX] XML documentation will now correctly be displayed for concepts in plugins

## v0.9.3-preview

* Improved signature help/active parameter for several non-common scenarios
* Keyword info implementation class is now displayed with full name (including namespace)
* More detailed logging during Rhetos app context initialization
* Full log file path displayed during initialization
* Changed C# grammar definition to allow better syntax highlight behavior when using anonymous delegates in C# snippets
* [BUGFIX] Signature help not available while typing last parameter
* [BUGFIX] Quoted strings breaking position detection for arguments
* [BUGFIX] File include directive (`<>`) not working
* [BUGFIX] Autocomplete not working at end-of-file
