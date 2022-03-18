# Game Gear Micro Save Manager


![An image of GGM Save Manager's main form](https://github.com/eddebaby/GGMSaveManager/blob/main/img/MainForm.png?raw=true)

* Uses .net Framework 4.6.1 (Coded in C# with Visual Studio 2017)

This tool allows you to manage your saves for the Game Gear Micro (GGM).

Feature overview
================

* Open a Game Gear Micro save data file...
* Create a new (blank) Game Gear Micro save data file...
* Save a Game Gear Micro save data file...
* Load a list of the 6 games in use, which is used by the UI... 
* Copy a slot in a Game Gear Micro save data file...
* Paste a slot in a Game Gear Micro save data file...
* Clear a slot in a Game Gear Micro save data file...
* Export the SRAM data from a slot in a Game Gear Micro save data file...
* Import an SRAM file in to a slot in a Game Gear Micro save data file...


Advanced features (useful for moving games around and correcting user error):
-----------------------------------------------------------------------------
* Change the Game ID of a slot in a Game Gear Micro save data file...
* Change the save slot number of a slot in a Game Gear Micro save data file...
* Change the version number of a slot in a Game Gear Micro save data file...
* Change the SRAM size of a slot in a Game Gear Micro save data file...
* Recalculate the hash of a corrupt slot in a Game Gear Micro save data file...

Usage
=====

You need to transfer the save data from your GGM to your PC*, then run this tool and open the file you transferred.

Transfer the save data back to the GGM, after making changes.

*Augen's GGM CFW Tool provides a simple way to transfer save data to/from the GGM.

Or..
 - start GGM whilst pressing the button inside the battery compartment whilst plugged in to PC
 - run ggmicro_bootkit_win
 - run this via putty: mount /dev/nandd /mnt/UDISK
                      (or mount /dev/nande /mnt/UDISK for Augen's CFW)
 - copy save.bin with (e.g.) filezilla
 - for original firmware: save.bin is in /usr/games/save
 - for Augen's CFW: save.bin is in /mnt/UDISK/system/save

You can also export SRAM data from the GGM save data, for use with: emulators/flash carts/cart flashers.

Expected file types
===================

Expects either a **.ggmsav** file from Augen's GGM CFW Tool, or a **save.bin** file extracted from the Game Gear Micro (these both are the the same 7.75MB file)

For the list of 6 games:
Expects a **romlist.txt** file from Augen's GGM CFW Tool, which is just a list of 6 .gg files (So you can easily create your own)

See the "example data" folder for examples of both file types.

List of Save Slots
==================

Each of the 62 save slots available are put in a list, where each line represents a single slot, which can be either SRAM or a save state.


for a slot with SRAM data, the format is like this:

  Slot Number : SRAM [SRAM Size] (Version Number) - Game Name/Number


for a slot with a save state, the format is like this:

  Slot Number : State *Save State Number* (Version Number) - Game Name/Number


Use the "Load Game Names" feature to replace 'Game 1' (etc) with the names of the games you are using.

Clicking a line in the list will select the corresponding save slot; this activates the function buttons for that slot.

You can Copy and Paste between 2 different open save data files.

 Line colour/boldness
 --------------------

 - If the line is in **bold**, then the corresponding slot will be loaded by the GGM for that Save ID.

 - If the line is not in bold, then the corresponding slot is old save data, and will not be loaded by the GGM. 
   Increasing the version number to be the current highest (turning it bold) will cause the GGM to use this slot by preference.

 - If the line is in Red, then the slot is corrupt (hash does not match data) This slot will be rejected by the GGM.


Notes
=====
 * save ID is not changed when importing SRAM
 * SRAM size is not changed when importing SRAM
 * see "save format.txt" for details on how the Game Gear Micro stores the SRAM and save state data for it's games.
