using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
	public class Adventure
	{
		public Scene? InitialScene = null;
		public Scene? CurrentScene = null;
		public Dictionary<string, Scene> Scenes = new();

		public void HandleInput()
		{
			while (true)
			{
				char input = Console.ReadKey(intercept: true).KeyChar;
				if (CurrentScene.Choices.ContainsKey(input))
				{
					Console.Clear();
					string nextSceneId = CurrentScene.Choices[input];
					if (Scenes.ContainsKey(nextSceneId))
					{
						CurrentScene = Scenes[nextSceneId];
						break;
					}
				}
				else if (input == 'q')
				{
					CurrentScene = null;
					break;
				}
				else
				{
					Console.WriteLine($"Your input '{input}' did not match any of the choices.\n");
					Console.WriteLine($"If you're trying to quit the game, type 'q'.");
				}
			}
		}

		public void Start()
		{
			CurrentScene = InitialScene;

			while (CurrentScene != null)
			{
				Console.WriteLine(CurrentScene.Description);
				HandleInput();
			}
		}
	}
}
