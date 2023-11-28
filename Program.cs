using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Xml;

namespace ConsoleAdventure
{
	internal class Program
	{
		static Adventure LoadAdventureFromFile(string path)
		{
			if (File.Exists(path))
			{
				var adventure = new Adventure();
				XmlDocument doc = new XmlDocument();
				doc.Load(path);
				XmlElement adventureElement = doc.DocumentElement;

				string initialSceneId = adventureElement.Attributes.GetNamedItem("initialSceneId").Value;

				XmlNodeList sceneElements = adventureElement.SelectNodes("child::scene");
				foreach (XmlNode sceneElement in sceneElements)
				{
					string sceneId = sceneElement.Attributes.GetNamedItem("id").Value;

					XmlNode descriptionElement = sceneElement.SelectSingleNode("description");
					string description = descriptionElement.InnerText;

					XmlNodeList choiceElements = sceneElement.SelectNodes("descendant::choice");
					var sceneChoices = new Dictionary<string, string>();
					foreach (XmlNode choiceElement in choiceElements)
					{
						string key = choiceElement.Attributes.GetNamedItem("key").Value;
						string leadsTo = choiceElement.Attributes.GetNamedItem("leadsTo").Value;
						sceneChoices.Add(key, leadsTo);
					}
					var scene = new Scene(description, sceneChoices);
					adventure.Scenes.Add(sceneId, scene);
				}

				if (adventure.Scenes.ContainsKey(initialSceneId))
				{
					adventure.InitialScene = adventure.Scenes[initialSceneId];
					return adventure;
				}
				else
				{
					throw new Exception($"The adventure {path} does not define valid initial scene!");
				}
			}
			else
			{
				throw new Exception($"Could not load game data in {path}!");
			}
		}

		static void Main(string[] args)
		{
			try
			{
				var adventure = LoadAdventureFromFile("ZombieHorror.xml");
				adventure.Start();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
