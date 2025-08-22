# Braid Flyhack Trainer

A memory manipulation trainer for the indie puzzle-platformer game "Braid" that enables flight functionality, allowing players to freely navigate through the game world by modifying the player character's position and movement status in real-time.

![Flyhack Interface](https://github.com/nayutalienx/braid_flyhack/blob/master/braid_flyhack_trainer/resources/flyhack.PNG?raw=true)

## Features

- **Real-time Flight Control**: Move the player character freely in all directions
- **Global Hotkeys**: Control flight even when the game window is not focused
- **Memory-Safe**: Uses proper Windows API calls for process memory manipulation
- **Automatic Process Detection**: Automatically finds and attaches to the running Braid process
- **Simple Interface**: Minimal GUI showing control instructions

## Controls

| Key | Action |
|-----|--------|
| `I` | Move Up |
| `K` | Move Down |
| `J` | Move Left |
| `L` | Move Right |

## Requirements

- **Operating System**: Windows (Windows Forms application)
- **.NET Framework**: .NET 6.0 or later with Windows support
- **Game**: Braid must be running
- **Dependencies**: MouseKeyHook library (automatically installed via NuGet)

## Installation & Usage

1. **Clone the repository**:
   ```bash
   git clone https://github.com/nayutalienx/braid-flyhack.git
   cd braid-flyhack
   ```

2. **Build the project**:
   ```bash
   dotnet restore
   dotnet build --configuration Release
   ```

3. **Run Braid**: Start the Braid game before launching the trainer

4. **Launch the trainer**:
   ```bash
   cd braid_flyhack_trainer/bin/Release/net6.0-windows
   ./braid_flyhack_trainer.exe
   ```

5. **Use the controls**: Press `I`, `J`, `K`, `L` keys to control flight movement

## How It Works

The trainer uses several key components:

### Memory Manipulation
- **BraidMemoryParser**: Scans and manipulates the Braid process memory
- **Process Detection**: Automatically locates the running `braid.exe` process
- **Pointer Resolution**: Finds the player entity pointer using multiple offset patterns
- **Coordinate Modification**: Directly modifies X/Y position values in memory

### Movement System
- **Status Management**: Temporarily sets player status to "flying" during Y-coordinate changes
- **Global Hooks**: Uses system-wide keyboard hooks to capture input
- **Smooth Movement**: Moves player in configurable increments (100 units by default)

### Technical Details
- **Memory Offsets**: 
  - X Position: `+0x14`
  - Y Position: `+0x18` 
  - Status: `+0x10`
- **Flying Status Value**: `9.4f` (discovered via reverse engineering)
- **Walk Status Value**: `2.0f`
- **Movement Step**: `100` units per keypress

## Project Structure

```
braid_flyhack_trainer/
├── memory/
│   ├── MemoryParser.cs          # Base memory manipulation class
│   └── BraidMemoryParser.cs     # Braid-specific memory handler
├── service/
│   └── KeymapService.cs         # Keyboard input handling
├── handler/
│   ├── KeyboardHandler.cs       # Global keyboard hooks
│   └── ProcessHandler.cs        # Process management utilities
├── processor/
│   └── KeyEventProcessor.cs     # Key event interface
├── resources/
│   └── flyhack.PNG             # Application screenshot
├── Form1.cs                     # Main application window
└── Program.cs                   # Application entry point
```

## Security & Compatibility

### Anti-Virus Warnings
This application may trigger anti-virus software due to:
- Process memory manipulation
- Global keyboard hooks
- Dynamic process attachment

These are legitimate features required for game trainer functionality.

### Game Compatibility
- Tested with standard Braid installations
- May require updates for different game versions
- Memory offsets might need adjustment for modified game builds

## Development

### Building from Source
```bash
# Restore dependencies
dotnet restore

# Build debug version
dotnet build

# Build release version
dotnet build --configuration Release
```

### Dependencies
- **MouseKeyHook 5.6.0**: Provides global keyboard hook functionality
- **.NET 6.0 Windows**: Required for Windows Forms and P/Invoke operations

## Disclaimers

⚠️ **Important Notices**:

- **Single-Player Only**: This tool is intended for single-player gameplay enhancement only
- **Educational Purpose**: Provided for educational and research purposes
- **Game Ownership**: You must own a legitimate copy of Braid to use this trainer
- **Use at Your Own Risk**: Memory manipulation may cause game instability
- **No Warranty**: This software is provided as-is without any warranties

## Legal

This project is an independent tool and is not affiliated with or endorsed by Number None Inc. or Jonathan Blow, the creators of Braid. Braid is a trademark of its respective owners.

## Contributing

Contributions are welcome! Please feel free to:
- Report bugs or issues
- Suggest improvements
- Submit pull requests
- Add support for other game versions

## License

This project is provided for educational and research purposes. Please respect the intellectual property rights of the original game creators.
