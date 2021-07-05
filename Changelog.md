2021/07/05:
	Updates:
		- general restructure of code organization, move to Github, etc.
		- moved MUGEN process handling code into a separate project (https://github.com/ZiddiaMUGEN/MugenWatcher) for easier reuse
		- support for additional debug colors, customizable debug colors in all MUGEN versions
		- added several new trigger breakpoint types (note that some of these will cause a lot of lag unless you use `experimental breakpoints`):
			- Damage
			- NumHelper/NumHelper( xxx )
			- NumProjId( xxx )
			- NumExplod( xxx )
			- NumTarget
			- HasTarget
		- added projectile animations to the projectile debug list
	Bugfixes:
		- fixed position/velocity calculations in most cases for all versions (some cases may still be broken)
		- forced MUGEN to run in windowed mode when ran through SAK

2020/11/08 v2:
	Updates:
		- add an option for experimental non-hardware breakpoints to the Profile config, toggle this if you crash/have antivirus issues using regular breakpoints
	Bugfixes:
		- fix round-related data for MUGEN 1.0 (RoundNo, RoundTime, Win/Lose, etc)
			
2020/11/08:
	Updates:
		- enabled trigger-checking breakpoints for all MUGEN versions
		- relabeled to 'Trigger breakpoint status' to be explicit that it will pause the process
	Bugfixes:
		- fixed functionality of 'Bring other windows to front' button
		- fixed a bug where MUGEN would report as frozen when it really wasn't (related to wrong GameTime offset)
		- fixed some issues related to MUGEN scaling (but not all)
	Todo:
		- validate and fix offsets for 1.0 so that it can receive the same functionality as 1.1.
		
2020/10/10:
	Bugfixes:
		- fixed support for MUGEN 1.0 Debug Display + Explods. Some bugs might remain.
	
2020/09/22 v2:
	Updates:
		- added preliminary support for 1.1b1. Some functions will be broken, but most seem to work. Will fix these as I uncover them. Added a few to the Known Issues section.

2020/09/22:
	Updates:
		- found the Anim data pointers, so now it can identify whether current anim has no-clsn2 successfully.
		- added Clsn1 detection for anims as well, so that we can report on has-clsn1, added to var(40~59) tab
		- renamed Flags tab to AssertSpecial.
		- added support for local AssertSpecial in its tab in 1.1a4 (this needs a backport to 1.0/WIN later, just a matter of writing the address)
		- added LocalCoord reading from player info struct, added to var(40~59) tab
		- re-implemented StepFrame functionality for 1.1
	Bugfixes:
		- fixed an issue where the application would crash if Debug was enabled while on the Flags tab.
		- fixed an issue where NoFG flag failed to display in the Flags tab.
		- fixed an issue where the application would display trigger-related error messages on startup.
		- fixed (sort of) the Player/Helper Pos and Vel issues, however all co-ords and velocities are stage-relative and not screen-relative (i.e. co-ords increase forever as the stage scrolls). not clear how to convert to screen-relative coords.
		- fixed Projectiles showing very incorrect Pos values due to a bad conversion - this will still be wrong for Screen size not equal to 1280x720 as I have lazily implemented.
		- fixed an issue where data in var(40~59) tab wouldn't refresh (caused by me moving some info around).
	
2020/09/20:
	- initial release.
	
	
	
	
	
	
	
	
	