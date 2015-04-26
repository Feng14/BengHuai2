--------------------------------------------------
              NGUI: HUD Text
 Copyright © 2012-2014 Tasharen Entertainment
                Version 1.11
http://www.tasharen.com/forum/index.php?topic=997.0
--------------------------------------------------

Thank you for buying NGUI HUD Text!

This version of HUDText has been tested with NGUI 3.4.9
If you have NGUI 2.7.0 or earlier, delete the Examples and Scripts folders,
then import the contents of the hudtext_ngui270.unitypackage instead.

----------------------------------------------
!! IMPORTANT NOTE !!
----------------------------------------------

Upon importing this package into a brand-new project, you will get compile errors
unless that project already has NGUI present! You'll need to import NGUI as well.
If you don't have NGUI, but still want to use HUDText, then I am guessing you didn't
read the package's description! But... you can still use HUDText. Get in touch with me
via support@tasharen.com and I will hook you up.

----------------------------------------------

Usage:
1. Attach the HUDText script to a game object underneath your UIRoot and set the font it should use.
2. To make it follow an object drawn with another camera, attach UIFollowTarget to the same object and set its target.
3. From code, use HUDText's Add() function to add new floating text entries.

You can also tweak the splines on the HUDText script, changing the motion of the text as you see fit.

Video: http://www.youtube.com/watch?v=diql3UP1KQM

----------------------------------------------
Example Usage:
----------------------------------------------

HUDText hudText = GetComponent<HUDText>();

// This will show damage of 123 in red, and the message will immediately start moving.
hudText.Add(-123f, Color.red, 0f);

// This will show "Hello World!" and make it stay on the screen for 1 second before moving
hudText.Add("Hello World!", Color.white, 1f);

// If you don't want your numeric damage values to be added up, pass them as a string instead:
float myDamage = 123f;
hudText.Add(myDamage.ToString(), Color.red, 1f);

----------------------------------------------

If you have any questions, suggestions, comments or feature requests, please
drop by the forums, found here: http://www.tasharen.com/forum/index.php?topic=997.0
