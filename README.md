# FreeMove
Move directories (even from one drive to another) freely without breaking installations or shortcuts

This is a version of the original FreeMove from [Luca](https://github.com/imDema/FreeMove).

With this version you can also move files (not only folders) and you may set a shell integration which enables a context menu to files and folders.

If a program installs on C:\ by default or you want to move the installation folder of a program to somewhere else without breaking it you can use this program.
## How It works
1. The files are moved to the new location
2. A directory junction is created from the old location to the new one. This way trying to access a file from its old location will simply redirect to the new one
## Usage
Just run the executable and use the GUI or use the Shell Integration

The program has gained administrator privileges, which grants to it better working
## Screenshots
![Screenshot](http://imgur.com/mhsGFyb.png)
