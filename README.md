# RSF

App main purpose is to compare files and get duplicates (for now it's only for images).
It checks every image file in folder and hashes it, and compares it to another files.Results are stored in Json folder to speed up next runs.
MainApp file contains "core" of the app while ResultsWindow is for displaying repeated images and managing them.

To complile use Visual Studio 2015 and uses Json.NET.
