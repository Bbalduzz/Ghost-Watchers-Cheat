# Ghost Watchers Internal Cheat
Simple internal cheat for Ghost Watchers made in c#. I'll update this cheat regularly (source code not updated with the last release)
> Last cheat update: 03/01/2023

🎈 If u wanto to join the development, share ideas or u are just curious, join the discord: https://discord.gg/6AEdmkXUbS

### Features
- Ghost ESP
- Ghost infos (Type, Age, Mood, Distance, Rank)
- Additional Infos (Temp, Emp, etc..)
- Players ESP
- Cursed Item ESP
- House ESP
- Actions
  - give money and exp (a lot per click)
  - Unlock all achivements
  - Become the host
- exe Injector
- Eject by pressing "J"

### Preview
![gw](https://user-images.githubusercontent.com/81587335/185790097-bc7dba5a-9bbb-41c0-8f25-0b2f8d516796.png)
![gwesp](https://user-images.githubusercontent.com/81587335/185099206-80e97985-3f6a-4ae4-b26a-c70c56a2e646.png)

https://user-images.githubusercontent.com/81587335/210391010-96bf364d-8a3c-4f27-98f5-9496b095cefc.mp4

## Code refactor (19.07.2023)
Updated the old source to something more structurated and easier to upscale
### Added Python injector code
- to make it work add the `Ghost-Watcher-Internal.dll` in the same dir and run `py inject.py`


### How to run
## Added Injector
To use just download the zip folder in the [Releases](https://github.com/Bbalduzz/Ghost-Watchers-Cheat/releases/tag/GWESP) section, extract it and open `GW Injector.exe` with the game opened.
For future releases, the injector is made so that you can only change the `GhostWatchersESP.dll` file in the folder. Just download the single dll file, remove the one in the GWC filder and replace it with the new one. (DONT CHANGE NAMES TO THE FILES OR MOVE THEM IN OTHER FOLDERS)

https://user-images.githubusercontent.com/81587335/210766155-e8da3c02-dc2b-4e66-8f94-5d1f4f84f19c.mp4

### Other methods
~As for now I haven't made an injector~, so a monoinjector is needed.
1. Download **"GhostWatchersESP.dll"**
2. Download a **monoinjector** (I use [SharpMonoInjector](https://www.unknowncheats.me/forum/downloads.php?do=file&id=34970))
3. **Run** Ghost Watchers
4. **Run** SharpMonoInjector (smi_gui.exe)
5. In the Injector click refresh
6. Select the path of the downoaded GhostWatchersESP.dll
7. Set: `Class Name` = `Loader`
8. Set: `Method Name` = `Init`
9. Click Inject

### New Injection method (3/01/2k23)
> Due to SharpMonoInjector closing when scanning for processes, I changed the injection method. This means that if u can still use SharpMonoInjector fine, this is just a second alternative.

The chosen one: [MonoJabber](https://github.com/AWilliams17/MonoJabber), check out the original repo
1) In the [Releases](https://github.com/Bbalduzz/Ghost-Watchers-Cheat/releases/tag/GWESP) section download:
    - `GhostWatchersESP.dll`
    - `MemTools.dll`
    - `MonoJabber.exe`
    - `MonoLoaderDLL.dll`
    
And keep them in the same folder

2) Open the game
3) Open up the cmd where u got this 4 files and run `MonoJabber.exe "Ghost Watchers.exe" "<put_your_path>\GhostWatchersESP.dll" "GhostWatchersESP" "Loader" "Init"`

### Note
- The code needs a in-depth clean up and optimization (too lazy to do it) == DONE
- This project is still under developement, i'm working to add new things
- This is my very first project done in c#, there's gonna be **a lot** of errors/bugs/things that could be improved

## Support Me
- if u want to buy me a coffe: [☕️](https://www.buymeacoffee.com/Bbalduzz)
- Consider starring this project ⭐️ :)
