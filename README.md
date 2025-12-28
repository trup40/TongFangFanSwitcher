# TongFangFanSwitcher

Simple fan speed toggle utility for TongFang GM7MP0P based laptops.

![Platform](https://img.shields.io/badge/platform-Windows-blue)
![License](https://img.shields.io/badge/license-MIT-green)
![.NET](https://img.shields.io/badge/.NET-8.0-purple)

## 🎯 Supported Devices

This tool works on laptops using the **TongFang GM7MP0P chassis**, including:

- Monster (various models)
- XMG
- Schenker
- Eluktronics
- Other TongFang GM7MP0P rebrands

## ⚡ Features

- Toggle between **Normal** and **Turbo** fan modes
- Single executable, no installation needed
- Lightweight SLIM: (~700KB)
- Full: (~70MB)
- Can be assigned to keyboard shortcuts (Fn+F key combinations)

## 📥 Download

**[Download SLIM TongFangFanSwitcher.exe](https://github.com/trup40/TongFangFanSwitcher/releases/latest/download/TongFangFanSwitcher_SLIM.exe)**
**[Download FULL TongFangFanSwitcher.exe](https://github.com/trup40/TongFangFanSwitcher/releases/latest/download/TongFangFanSwitcher.exe)**

## 🚀 Usage

1. Download `TongFangFanSwitcher.exe`
2. **Run as Administrator** (required for WMI access)
3. The fan mode will toggle automatically:
   - **Normal (27648)** → **Turbo (27712)**
   - **Turbo (27712)** → **Normal (27648)**

### Create Keyboard Shortcut (Optional)

1. Right-click `TongFangFanSwitcher.exe` → **Create shortcut**
2. Right-click shortcut → **Properties**
3. Set **Shortcut key** (e.g., `Ctrl+Alt+F`)
4. Click **Advanced** → Check **Run as administrator**
5. Click **OK**

## ⚙️ Technical Details

- **EC Address**: `1873 (0x751)`
- **Normal Speed**: `27648 (0x6C00)`
- **Turbo Speed**: `27712 (0x6C40)`

The tool uses Windows Management Instrumentation (WMI) to access the ACPI Embedded Controller memory.

## ⚠️ Warning

**USE AT YOUR OWN RISK!**

- This tool directly accesses hardware registers
- Incorrect usage may cause hardware damage
- Only use on supported TongFang GM7MP0P chassis
- May void warranty

**Not responsible for any damage caused by this software.**

## 🛠️ Building from Source

Requirements:
- .NET 8.0 SDK or later
- Windows OS