B.O.S.S.
Bolt On Screenshot System

/*
 * Plugin Owner - Ted
 * Contributors - Ted/SyNik4l
 * Last Update - 1/19/2014
 * Contact: synik4l@gmail.com
 * Forum Thread: http://forum.kerbalspaceprogram.com/threads/34631-0-23-Bolt-On-Screenshot-System-(BOSS)-v2-1-2
*/

Important
Using this mod requires alot of ram. Thus burst fire mode is restricted to no supersampling. Although you can still use supersampling when not using burst fire.
I recommend not going higher than 3 or 4. I also do not recommend pressing your screenshot button multiple times within 30 seconds of each other(while set on 3 or 4).
Or you may crash your game. Currently there is nothing I can do about this. Its because of the ram limit of KSP. And how much ram it takes to super sample an image.
Just leave your screenshots folder open.Then when you see the image pop up in there. You know you are okay to take another.(

Description
This plugin allows you to take screenshots within KSP at a higher resolution than your current screen resolution, using a technique called 'supersampling'. 
Press 'z' to take a screenshot(default) and 'p' to hide the UI. The screenshots are placed in /Gamedata/BOSS/PluginData/BOSS/


Plans: 
1. I'll add a check for the image file and disable the button in between. To prevent crashing when supersampling value is set high.
2. I'm trying to figure out how to fix the problem with the supersampling killing all of KSP's memory. Thus crashing it. So i've written an external script to handle antialiasing(super sampling).
	As it seems Unity does not like system.drawing because they expect all drawing to be done by directX and opengl. So I cannot call my script from KSP. I'm trying to figure out a way around this,
	in a memory conservative way.
	


Changelog
v2.1.3 - Forked by SyNiK4L - added choosing of skins, burst shot mode, help window, cleaned up code. Added saving of supersampling value(be careful with this).
added to toolbar(http://forum.kerbalspaceprogram.com/threads/60863-0-23-0-Toolbar-1-4-0-Common-API-for-draggable-resizable-buttons-toolbar). 

v2.1.2 - Fixed up the issues with hiding UI, made it persistent and changed it to the p key.
v2.1.1 - Fixed an issue in the paths given to the screenshot method. Screenshots should actually save now...
v2.1 - Added in the ability to hide the BOSS UI with the F2 key.
v2.0 - Updated for 0.23 compatibility. Refactored and removed a lot of code; mainly to do with requiring a part and then method of text field input.
v0.2.3 - Updated for 0.21 compatibility.
v0.2.2 - Fixed issues with multiple Bolts being on the same craft and thus causing BOSS to bug out.
v0.2.1 - Reverted to old internal structure for GUI, will progress with it at another date.
v0.2 - Changed to PartModule. Updated for 0.18. Changed GUI. Fixed various little bugs.
v0.1.2 - Prevented spamming of the debug log. Fixed non-updating text field.
v0.1.1 - Added in Settings Persistence.
v0.1 - Public Release


/*   The Bolt-On Screenshot System is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

The Bolt-On Screenshot System is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with The Bolt-On Screenshot System.  If not, see <http://www.gnu.org/licenses/>.*/