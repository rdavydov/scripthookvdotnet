/**
 * Copyright (C) 2015 crosire
 *
 * This software is  provided 'as-is', without any express  or implied  warranty. In no event will the
 * authors be held liable for any damages arising from the use of this software.
 * Permission  is granted  to anyone  to use  this software  for  any  purpose,  including  commercial
 * applications, and to alter it and redistribute it freely, subject to the following restrictions:
 *
 *   1. The origin of this software must not be misrepresented; you must not claim that you  wrote the
 *      original  software. If you use this  software  in a product, an  acknowledgment in the product
 *      documentation would be appreciated but is not required.
 *   2. Altered source versions must  be plainly  marked as such, and  must not be  misrepresented  as
 *      being the original software.
 *   3. This notice may not be removed or altered from any source distribution.
 */

using System;
using System.IO;
using System.Reflection;
using WinForms = System.Windows.Forms;

abstract class ScriptHookVDotNet
{
	static GTA.ScriptDomain Domain = null;

	public static bool Init()
	{
		if (Domain != null)
		{
			GTA.ScriptDomain.Unload(ref Domain);
		}

		var settings = GTA.ScriptSettings.Load(Path.ChangeExtension(Assembly.GetExecutingAssembly().Location, ".ini"));
		Domain = GTA.ScriptDomain.Load(settings.GetValue(String.Empty, "ScriptsLocation", "scripts"));

		if (Domain != null)
		{
			Domain.Start();

			return true;
		}

		return false;
	}
	public static void Tick()
	{
		if (Domain != null)
		{
			Domain.DoTick();
		}
	}

	public static void KeyboardMessage(WinForms.Keys key, bool status, bool statusCtrl, bool statusShift, bool statusAlt)
	{
		if (Domain != null)
		{
			Domain.DoKeyboardMessage(key, status, statusCtrl, statusShift, statusAlt);
		}
	}
}