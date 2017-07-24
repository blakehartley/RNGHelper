Final Fantasy XII RNG Manipulator.

This tool is an upgrade on previous iterations of the FFXII RNG cure tracking methods, suitable for PS2/PS4.
This tool can be used to find your place in the list of "pseudo" random numbers FFXII uses, allowing you to force chest spawns, chest contents, steals, and the spawning of rare game by moving to the desired position in this list.

A) Character stats:
All three tools have three rows for character stat entry. Enter the stats of one, two, or three chracters (as well as the spell they'll be using and whether they have serenity)


B) Manipulation data:
Each tool requires different information to determine where the correct point in the RNG list is.
Chests:
- The appearance chance, gil chance, item 1 chance, and gil amount are all required to correctly manipulate chests.
	FFXII: Chest data can be found on ff12maps.com, or any other reference on chests.
	IZJS/TZA: Chest data can be found on online maps such as http://www.fftogether.com/forum/index.php?topic=2764.0 or through google. For IZJS/TZA, all chests have 50% Item 1 chance without the Diamond Armlet, and 95% Item 1 chance with the Diamond Armlet.
- RNG position is the index of the random number consumed after zoning that determines whether the chest spawns. This number is different for all chests, and needs to be determined through trial and error, unless someone has already determined it. It can vary in zones where weather changes.

Steal:
- All steals have the same probability, so no data is required for this tool.

Rare Game:
- Min/Max chance is the minimum and maximum chance that the rare game appears. For almost all rare game, min chance is 0, and max chance is the probability for the game to spawn as listed in references.
- RNG position is the index of the random number consumed after zoning that determines whether the game spawns. For Most rare games, this random number is the last consumed when zoning.


C) Cure entry:
Once all of the above data is entered, begin entering cures from the characters. For multi-character entry, cycle through all characters in order. Once correct position has been found, numbers on list/in the entry box will begin to predict your in game cures.
Continue will continue the current search, and Begin Search will start a new search with the current entry.
Consume can be used to skip through many numbers quickly. If the desired position is far away, punches (10) staff hits (11) gun shots (8) fire (2) can be used to move down the list more quickly.


D) Output:
Chests:
The first row after the most recent entry shows what will be drawn from the chest. Bold entries indicate positions where the chests will spawn; however, RNG position is how far away from the current position this entry should be to make the chest spawn.
Advance to appear: amount of RNG that needs to be consumed to make the chest appear. RNG position must be correct for this to work.
Advance for item: amount of RNG that needs to be consumed to draw the desired item.

Steals:
The first row after the most recent entry shows what will be stolen next.

Rare Game:
Advance to spawn: amount of RNG that needs to be consumed to make the rare game spawn. Rows are highlighted based on where rare 1 (blue), rare 2 (red), and both (purple) will spawn.


E) Notes:
The amount of RNG consumed when zoning can be determined by finding your position in the RNG list, zoning, curing more, and finding by eye how far down the list you have moved. Rare game RNG position is usually this amount minus one, and chest position is usually one of the later numbers consumed.
If you are unable to find your position on the list, something in the same zone as you may be consuming RNG, such as monster actions, npc movement, or character gambits.
Combo after punch #N is an output that tells you how many punches you have until the next combo. This is useful if you use punches to advance RNG, so you know if a coming punch will disturb the count.
Comments are always appreciated, and may be left on the github where we maintain the source code for this tool:
https://github.com/roostalol/RNGHelper

----------------------------------------------------------------------------------------------------

Tranquilite0's original notes:
Final Fantasy XII RNG Helper. Works for PS2 and PS4 versions of the game.

I first coded this a number of years ago, and I must admit that it is a bit of a mess. There are a number of half baked ideas (like that progress bar), and I did nothing to make it so that the UI thread doesn't stall when doing large numbers of calculations.
I refactored things a little when I added in support for The Zodiac Age.

Basic usage:
1. Enter in your stats (level, magick power, spell power, whether you have serenity licence, and Console/Platform).
2. Heal yourself
3. Report how much you were healed to the program using the RNG search section
4. Repeat steps 2 and 3 untill you are confident you are at the Correct RNG position.
5. Use the proceeding RNG values to manipulate chest drops/rare steals/etc. The % column is most likely what you want to use for this as many manipulable events use the rng value mod 100 to determine success or failure.

If you need any more information on how you can use this program to your advantage, I'm sure a little searching of the web will help.

There is also a section where you can provide the RNG position, and the number of positions you want to display, and it will give you a "cure list", which you can then copy into a spreadsheet if you want.

About the RNG Algorithm Used:

While the PS2 version used the old 1998 version of the Mersenne Twister algorithm, the PS4 remake uses the "new" (or at least newer) 2002 version of the MT algorithm. You can find links to the original source for these algorithms in the RNG1998.cs and RNG2002.cs files respectively.
Both the PS2 and PS4 initialize their respective algorithms with the same seed of 4537 (I'm so glad I didnt have to brute force the seed for FF12:TZA). The PS4/FF12:TZA seems to reinitialize the PRNG whenever you restart the app, so you dont need to restart the PS4 hardware to reset the PRNG.