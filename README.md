> [!WARNING]  
> Work in progress. Right now it's a raw prototype.

A simple tool for play Wwise sounds in an editor.

# Why

AudioKinetic Wwise is quite a popular tool for working with sound. Wwise also provides extensions for Unity editor, for playing sounds. However, this extension requires Wwise application running. And if you are not a sound designer and do not have the corresponding software on your computer, it is impossible to play sound in the editor. So all team members should have Wwise installed if they need to test sound. You need to install specialized software in addition to the Unity Engine. This is not very convenient.

However, we already have the corresponding sound banks in the project, thanks to which the sound is in the runtime. So it would be great to run the sound in the editor without other software. This can be useful both for quick testing of sounds in a project and when creating custom extensions of the editor (for example, level editor or dialogs with playing sounds from there).
