An older mod I'm reviving:

Downloads

Spacedock: http://spacedock.info/mod/1029/Bolt-On Screenshot System (BOSS) Continued
Source code: https://github.com/linuxgurugamer/BOSS_Remastered
License:  GPLv3

 

The new version for KSP 1.4.1 has new dependencies

New Dependencies

Click Through Blocker
ToolbarController
CKAN has been updated to install the dependencies, if needed

Patreon.png

https://www.patreon.com/linuxgurugamer

This plugin allows you to take screenshots within KSP at a higher resolution than your current screen resolution, using a technique called 'supersampling'.

Press 'z' to take a screenshot and 'p' to hide the UI. These are both rebindable in the config.xml file found next to your screenshots.

Important:  Note that on Windows, GeForce Experience users may want to remap the screenshot key, or remap the keys in the GeForce Experience. That app's default for screenshots is also Alt-F1, so you may wind up posting to your Gallery rather than saving to your KSP/Screenshots folder.

 
http://i.imgur.com/WWcTB1l.png

Reviewed by Kottabos Games
https://youtu.be/8gx7-mpcL3Y

Screenshots can be found in "/GameData/screenshots".

A massive thanks to SyNik4l for adding in many of the much desired features and giving the code a good clean-up.

**Important Information**

Using this plugin can require a lot of RAM. Thus, burst fire mode is restricted to a supersample value of 1 - a.k.a no supersampling at all.

However, you can still use supersampling without restriction when not using burst fire.

I recommend not going higher than 3 or 4; you may be able to pull off 5, but it may crash/freeze your game for a second. If the game freezes, you are not missing any frames. So just let the game sit while it processes.

I have added a limiter on the screenshot key/button if your super sampling value is set higher than 1, mainly due to the KSP ram limit. This is to prevent multiple screenshots being taken too quickly causing you to go over the KSP ram limit and crash your game. The plugin checks for the image to finish processing.

Then allows you to take more screenshots. You can override this limiter by toggling on the 'override limiter' button on the help window.(WARNING: By doing this. You are pretty much limited to 2 screenshots at a time(because it will crash if you take more). Though, you may still crash your game by doing it, everyone's computer is different, and everyone is using different Add-Ons/Plugins alongside this. In the end, it could work better for some than others. Just know by overriding that limiter, you are subject to crashes.

**Burst Fire mode**: As explained above this is purely for automatically taking screenshots over a period of time. So no supersampling is allowed while this is toggled on. Thus you cannot change your supersampling value from one if the burst fire window is open. To use it

          1. Toggle burst fire
          2. Set interval(this field does allow point values - just make sure if for example you want .3 - You enter 0.3)
          3. Set time frame to take screenshots - only whole numbers.
          4. Hide UI with 'P' or with toolbar button
          5. Press screenshot hotkey.
          6. Do something cool to take pictures of!
