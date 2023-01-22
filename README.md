Tool development course assignment. Target grade: VG

# Tools made for the asteroids game:
I added a difficulty tool. In this tool you can set difficulty levels by changing parameters that are then stored in a scriptable object
and applied in runtime.
This tool uses a custom editor window to tweek the difficulty settings and apply them to the scriptable object.
It also has full undo/redo support.

![Screenshot 2023-01-15 112001](https://user-images.githubusercontent.com/57400375/212535258-63023386-f68d-406e-aa81-54607a5dd4ef.png)

## This tool utilizes:
- Scriptable Objects: SOs are utilized to store the configuration of the difficulty presets. This makes the process easier in a way that I have all the data I need in a central place and I can apply them in runtime with ease.
- Custom EditorWindow: The custom editor window is used to make creating, editing and applying the SOs. I designed the window in a way that everything is in a organized and central place which would make the tool easy to use for the designers.
- Serialized Objects/Properties: Using Serialized Objects/Properites makes hadnling errors and undo/redo much easier because a lot of the systems will be handled by unity.
- UnityPackage: The tool is exported to a package so adding it too the project is done in a few clicks.
