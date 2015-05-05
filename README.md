# UE4DiffAssistTool
This tool was crated to assist in the process of running a _diff_ on two different unreal assets.
This is best used to see changes between 2 files over time.

## FAQ

### How to use it?
- Drag and drop the latest (or the version you're using of) UE4Editor.exe on the first textbox.
- Drag and drop the older file you're willing to diff on the second textbox.
- Drag and drop the newer file you're willing to diff on the third textbox.
- Click on Diff button
- Wait until UE4Editor Loads
- Pick a project (hopefully, pick the project this file is related to)
- Wait until the Diff appears.

### How it works?
It runs UE4Editor.exe with the following command line:
UE4Editor.exe -diff new_file old_file

### Why creating this tool?
It's annoying to run that command-line when there are long paths involved, so, I created a simple tool in C# to aid me in this process.
My main use for it, for now, is to check the asset changes in the awesome [Tom Looman's Survival Game][1] which is stored in Git, which makes harder for the engine to automatically diff them.

### There's a better way of doing that!
Yeah, probably there is. IMO, the best way available is to integrate a context menu into windows that, when right-clicking a .uasset file, would allow us to select the first, then the second and diff them.
Sadly, this takes a lot of time to create, and I decided I was going for a simpler approach.

### I want to help!
Submit your patches! :+1:

[1]: http://www.tomlooman.com/survival-sample-game-for-ue4/
