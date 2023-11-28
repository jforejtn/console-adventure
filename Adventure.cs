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

		private List<string> quitWords = new List<string> { "q", "quit", "exit" };

		public void HandleInput()
		{
			while (true)
			{
				string input = Console.ReadLine();
				if (CurrentScene.Choices.ContainsKey(input))
				{
					string nextSceneId = CurrentScene.Choices[input];
					if (Scenes.ContainsKey(nextSceneId))
					{
						CurrentScene = Scenes[nextSceneId];
						break;
					}
				}
				else if (quitWords.Contains(input.ToLower()))
				{
					CurrentScene = null;
					break;
				}
				else
				{
					Console.WriteLine($"Your input '{input}' did not match any of the choices.\n");
					var quitWordsChoice = string.Join(" / ", quitWords.Select(word => $"'{word}'"));
					Console.WriteLine($"If you're trying to quit the game, enter {quitWordsChoice}");
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
