﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
	using KeyboardChoice = string;
	using NextSceneId = string;

	public class Scene
	{
		public string Description = "";

		public Dictionary<KeyboardChoice, NextSceneId> Choices = new();

		public Scene(string description, Dictionary<string, string> choices)
		{ 
			Description = description;
			Choices = choices;
		}
	}
}
