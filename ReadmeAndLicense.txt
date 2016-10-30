Bolt On Screenshot System
==

ChangeLog for Continued version

3.0.0	updated for 1.2
		Moved config data into BOSS/PluginData
		Changed config style from xml to cfg
		Added ToolbarWrapper.cs
		Added ability to use either standard toolbar or Blizzy toolbar
		Changed screenshot save directory to standard screenshots directory

====== Following is from the old version ================

Plugin Owner - Ted
Contributors - Ted/SyNik4l
Last Update - 2014-05-20
Contact: synik4l@gmail.com/ted@squad.com.mx
Forum Thread: http://forum.kerbalspaceprogram.com/threads/34631-0-23-Bolt-On-Screenshot-System-(BOSS)-v2-1-2
Github Repo: https://github.com/SyNiK4L/BOSS or https://github.com/ZedsTed/BOSS_Remastered


Description
--
This plugin allows you to take screenshots within KSP at a higher resolution than your current screen resolution, using a technique called 'supersampling'. 
Press 'z' to take a screenshot(default) and 'p' to hide the UI. The screenshots are placed in /Gamedata/BOSS/PluginData/BOSS/

Important
--
Using this mod requires a lot of RAM. Thus, burst fire mode is restricted to a supersample value of 1 - a.k.a no supersampling at all. 
However, you can still use supersampling without restriction when not using burst fire.

I recommend not going higher than 3 or 4; you may be able to pull off 5, but it may crash/freeze your game for a second. If the game freezes, you are not missing any frames. So just let the game sit while it processes. 
I have added a limiter on the screenshot key/button if your super sampling value is set higher than 1, mainly due to the KSP ram limit. This is to prevent multiple screenshots being taken too quickly causing you to go over the KSP ram limit and crash your game. The plugin checks for the image to finish processing.
Then allows you to take more screenshots. You can override this limiter by toggling on the 'override limiter' button on the help window.(WARNING: By doing this. You are pretty much limited to 2 screenshots at a time(because it will crash if you take more). Though, you may still crash your game by doing it, everyone's computer is different, and everyone is using different Add-Ons/Plugins alongside this. In the end, it could work better for some than others. Just know by overriding that limiter, you are subject to crashes. 

Burst Fire mode: As explained above this is purely for automatically taking screenshots over a period of time. So no supersampling is allowed while this is toggled on. Thus you cannot 
change your supersampling value from one if the burst fire window is open. To use it
1. Toggle burst fire
2. Set interval(this field does allow point values - just make sure if for example you want .3 - You enter 0.3)
3. Set time frame to take screenshots - only whole numbers.
4. Hide UI with 'P' or with toolbar button
4. Press screenshot hotkey.
5. Do something cool to take pictures of!




Plans
--
Change the screenshot key field to capture the key pressed instead of having the user type the keycode.
Rebuild the whole thing in Trigger Au's framework. For cleaniness and fun.
Recommendations are welcome.
	
Known Bugs
--
If you type an invalid key in the ss key field. It show invalid. If you try to delete invalid by pressing backspace it brings it back.
Thus you have to highlight the text and press and valid key. I'm going to try to find a better way to not allow a bad key. But allow you to have control over the input box.
As you normally should. Fix coming soon.

Changelog
--
v2.1.6 - Updated for 0.23.5 compatibility.
v2.1.5 - Fixed linux/osx, problem writing to file bug. Fixed bug where user could change not the screenshot key.
v2.1.4 - Fixed a couple random persistence bugs. UI should now persist throuh scene changes.
v2.1.3 - Forked by SyNiK4L - added choosing of skins, burst shot mode, help window, cleaned up code a lot. Added saving of supersampling value.
added to toolbar(http://forum.kerbalspaceprogram.com/threads/60863-0-23-0-Toolbar-1-4-0-Common-API-for-draggable-resizable-buttons-toolbar).)
fixed crashing issue by limiting the user to 1 screenshot while the unity engine is still antialiasing the image.

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

Keys
--
None	Not assigned (never returned as the result of a keystroke).
Backspace	The backspace key.
Delete	The forward delete key.
Tab	The tab key.
Clear	The Clear key.
Return	Return key.
Pause	Pause on PC machines.
Escape	Escape key.
Space	Space key.
Keypad0	Numeric keypad 0.
Keypad1	Numeric keypad 1.
Keypad2	Numeric keypad 2.
Keypad3	Numeric keypad 3.
Keypad4	Numeric keypad 4.
Keypad5	Numeric keypad 5.
Keypad6	Numeric keypad 6.
Keypad7	Numeric keypad 7.
Keypad8	Numeric keypad 8.
Keypad9	Numeric keypad 9.
KeypadPeriod	Numeric keypad '.'.
KeypadDivide	Numeric keypad '/'.
KeypadMultiply	Numeric keypad '*'.
KeypadMinus	Numeric keypad '-'.
KeypadPlus	Numeric keypad '+'.
KeypadEnter	Numeric keypad enter.
KeypadEquals	Numeric keypad '='.
UpArrow	Up arrow key.
DownArrow	Down arrow key.
RightArrow	Right arrow key.
LeftArrow	Left arrow key.
Insert	Insert key key.
Home	Home key.
End	End key.
PageUp	Page up.
PageDown	Page down.
F1	F1 function key.
F2	F2 function key.
F3	F3 function key.
F4	F4 function key.
F5	F5 function key.
F6	F6 function key.
F7	F7 function key.
F8	F8 function key.
F9	F9 function key.
F10	F10 function key.
F11	F11 function key.
F12	F12 function key.
F13	F13 function key.
F14	F14 function key.
F15	F15 function key.
Alpha0	The '0' key on the top of the alphanumeric keyboard.
Alpha1	The '1' key on the top of the alphanumeric keyboard.
Alpha2	The '2' key on the top of the alphanumeric keyboard.
Alpha3	The '3' key on the top of the alphanumeric keyboard.
Alpha4	The '4' key on the top of the alphanumeric keyboard.
Alpha5	The '5' key on the top of the alphanumeric keyboard.
Alpha6	The '6' key on the top of the alphanumeric keyboard.
Alpha7	The '7' key on the top of the alphanumeric keyboard.
Alpha8	The '8' key on the top of the alphanumeric keyboard.
Alpha9	The '9' key on the top of the alphanumeric keyboard.
Exclaim	Exclamation mark key '!'.
DoubleQuote	Double quote key '""'.
Hash	Hash key '#'.
Dollar	Dollar sign key '$'.
Ampersand	Ampersand key '&'.
Quote	Quote key '.
LeftParen	Left Parenthesis key '('.
RightParen	Right Parenthesis key ')'.
Asterisk	Asterisk key '*'.
Plus	Plus key '+'.
Comma	Comma ',' key.
Minus	Minus '-' key.
Period	Period '.' key.
Slash	Slash '/' key.
Colon	Colon ':' key.
Semicolon	Semicolon ';' key.
Less	Less than '<' key.
Equals	Equals '=' key.
Greater	Greater than '>' key.
Question	Question mark '?' key.
At	At key '@'.
LeftBracket	Left square bracket key '['.
Backslash	Backslash key '\'.
RightBracket	Right square bracket key ']'.
Caret	Caret key '^'.
Underscore	Underscore '_' key.
BackQuote	Back quote key '`'.
A	'a' key.
B	'b' key.
C	'c' key.
D	'd' key.
E	'e' key.
F	'f' key.
G	'g' key.
H	'h' key.
I	'i' key.
J	'j' key.
K	'k' key.
L	'l' key.
M	'm' key.
N	'n' key.
O	'o' key.
P	'p' key.
Q	'q' key.
R	'r' key.
S	's' key.
T	't' key.
U	'u' key.
V	'v' key.
W	'w' key.
X	'x' key.
Y	'y' key.
Z	'z' key.
Numlock	Numlock key.
CapsLock	Capslock key.
ScrollLock	Scroll lock key.
RightShift	Right shift key.
LeftShift	Left shift key.
RightControl	Right Control key.
LeftControl	Left Control key.
RightAlt	Right Alt key.
LeftAlt	Left Alt key.
LeftCommand	Left Command key.
LeftApple	Left Command key.
LeftWindows	Left Windows key.
RightCommand	Right Command key.
RightApple	Right Command key.
RightWindows	Right Windows key.
AltGr	Alt Gr key.
Help	Help key.
Print	Print key.
SysReq	Sys Req key.
Break	Break key.
Menu	Menu key.
Mouse0	First (primary) mouse button.
Mouse1	Second (secondary) mouse button.
Mouse2	Third mouse button.
Mouse3	Fourth mouse button.
Mouse4	Fifth mouse button.
Mouse5	Sixth mouse button.
Mouse6	Seventh mouse button.
JoystickButton0	Button 0 on any joystick.
JoystickButton1	Button 1 on any joystick.
JoystickButton2	Button 2 on any joystick.
JoystickButton3	Button 3 on any joystick.
JoystickButton4	Button 4 on any joystick.
JoystickButton5	Button 5 on any joystick.
JoystickButton6	Button 6 on any joystick.
JoystickButton7	Button 7 on any joystick.
JoystickButton8	Button 8 on any joystick.
JoystickButton9	Button 9 on any joystick.
JoystickButton10	Button 10 on any joystick.
JoystickButton11	Button 11 on any joystick.
JoystickButton12	Button 12 on any joystick.
JoystickButton13	Button 13 on any joystick.
JoystickButton14	Button 14 on any joystick.
JoystickButton15	Button 15 on any joystick.
JoystickButton16	Button 16 on any joystick.
JoystickButton17	Button 17 on any joystick.
JoystickButton18	Button 18 on any joystick.
JoystickButton19	Button 19 on any joystick.
Joystick1Button0	Button 0 on first joystick.
Joystick1Button1	Button 1 on first joystick.
Joystick1Button2	Button 2 on first joystick.
Joystick1Button3	Button 3 on first joystick.
Joystick1Button4	Button 4 on first joystick.
Joystick1Button5	Button 5 on first joystick.
Joystick1Button6	Button 6 on first joystick.
Joystick1Button7	Button 7 on first joystick.
Joystick1Button8	Button 8 on first joystick.
Joystick1Button9	Button 9 on first joystick.
Joystick1Button10	Button 10 on first joystick.
Joystick1Button11	Button 11 on first joystick.
Joystick1Button12	Button 12 on first joystick.
Joystick1Button13	Button 13 on first joystick.
Joystick1Button14	Button 14 on first joystick.
Joystick1Button15	Button 15 on first joystick.
Joystick1Button16	Button 16 on first joystick.
Joystick1Button17	Button 17 on first joystick.
Joystick1Button18	Button 18 on first joystick.
Joystick1Button19	Button 19 on first joystick.
Joystick2Button0	Button 0 on second joystick.
Joystick2Button1	Button 1 on second joystick.
Joystick2Button2	Button 2 on second joystick.
Joystick2Button3	Button 3 on second joystick.
Joystick2Button4	Button 4 on second joystick.
Joystick2Button5	Button 5 on second joystick.
Joystick2Button6	Button 6 on second joystick.
Joystick2Button7	Button 7 on second joystick.
Joystick2Button8	Button 8 on second joystick.
Joystick2Button9	Button 9 on second joystick.
Joystick2Button10	Button 10 on second joystick.
Joystick2Button11	Button 11 on second joystick.
Joystick2Button12	Button 12 on second joystick.
Joystick2Button13	Button 13 on second joystick.
Joystick2Button14	Button 14 on second joystick.
Joystick2Button15	Button 15 on second joystick.
Joystick2Button16	Button 16 on second joystick.
Joystick2Button17	Button 17 on second joystick.
Joystick2Button18	Button 18 on second joystick.
Joystick2Button19	Button 19 on second joystick.
Joystick3Button0	Button 0 on third joystick.
Joystick3Button1	Button 1 on third joystick.
Joystick3Button2	Button 2 on third joystick.
Joystick3Button3	Button 3 on third joystick.
Joystick3Button4	Button 4 on third joystick.
Joystick3Button5	Button 5 on third joystick.
Joystick3Button6	Button 6 on third joystick.
Joystick3Button7	Button 7 on third joystick.
Joystick3Button8	Button 8 on third joystick.
Joystick3Button9	Button 9 on third joystick.
Joystick3Button10	Button 10 on third joystick.
Joystick3Button11	Button 11 on third joystick.
Joystick3Button12	Button 12 on third joystick.
Joystick3Button13	Button 13 on third joystick.
Joystick3Button14	Button 14 on third joystick.
Joystick3Button15	Button 15 on third joystick.
Joystick3Button16	Button 16 on third joystick.
Joystick3Button17	Button 17 on third joystick.
Joystick3Button18	Button 18 on third joystick.
Joystick3Button19	Button 19 on third joystick.
Joystick4Button0	Button 0 on forth joystick.
Joystick4Button1	Button 1 on forth joystick.
Joystick4Button2	Button 2 on forth joystick.
Joystick4Button3	Button 3 on forth joystick.
Joystick4Button4	Button 4 on forth joystick.
Joystick4Button5	Button 5 on forth joystick.
Joystick4Button6	Button 6 on forth joystick.
Joystick4Button7	Button 7 on forth joystick.
Joystick4Button8	Button 8 on forth joystick.
Joystick4Button9	Button 9 on forth joystick.
Joystick4Button10	Button 10 on forth joystick.
Joystick4Button11	Button 11 on forth joystick.
Joystick4Button12	Button 12 on forth joystick.
Joystick4Button13	Button 13 on forth joystick.
Joystick4Button14	Button 14 on forth joystick.
Joystick4Button15	Button 15 on forth joystick.
Joystick4Button16	Button 16 on forth joystick.
Joystick4Button17	Button 17 on forth joystick.
Joystick4Button18	Button 18 on forth joystick.
Joystick4Button19	Button 19 on forth joystick.


Licensing
--
The Bolt-On Screenshot System is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

The Bolt-On Screenshot System is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with The Bolt-On Screenshot System.  If not, see <http://www.gnu.org/licenses/>.
