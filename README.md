# Swiss Army Knife
SAK converted and updated for current MUGEN versions. Supports WIN, 1.0, 1.1a4, 1.1b1

The original SAK was developed by an unknown Japanese developer, and was translated to English by Vans. I have only updated this tool for newer MUGEN versions and added/fixed some features.

New features compared to the original SAKWM include:
- More debug colors + customizable debug colors
- Support for non-hardware breakpoints to dodge antivirus
- Trigger breakpoint support for 1.0+
- Char folder iteration (temp disabled due to antivirus freaking out)
- Full AssertSpecial flag output
- Clsn1 detection
- LocalCoord display

Microsoft.Samples.Debugging.dll compiled from https://github.com/ZiddiaMUGEN/Microsoft.Samples.Debugging (forked from https://github.com/SymbolSource/Microsoft.Samples.Debugging with minor bugfixes/compatibility changes)
MugenWatcher.dll compiled from https://github.com/ZiddiaMUGEN/MugenWatcher

## Overview

Swiss Army Knife is a utility used for debugging and developing MUGEN characters. The idea is to reduce development effort and speed up development process by providing all the info you could ever want to see in a single display (without needing to set up and swap between potentially many DisplayToClipboard statements to get the same info).

It uses the excellent Microsoft.Samples.Debugging library as well as normal subprocess libraries to read and write data in the Mugen process. This allows gathering info on constants such as Alive, Life, Power, and Palno, temporary data such as Damage, existence of Clsn1/Clsn2, HitBy values, and AssertSpecial flags. It also allows insertion of commands, code, and data, enabling the tool to force MUGEN to pause, automatically step frame-by-frame at fixed intervals, or perform other cool tricks like changing the debug text color for easy reading.

On top of this, a trigger breakpoint system is offered to help debug even more closely. Under this system, MUGEN will automatically be frozen when a particular condition (e.g. `P1,Alive = 0`) is met, allowing review of the states and data which caused this condition.

