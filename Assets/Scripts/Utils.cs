using UnityEngine;

namespace Utils
{
	// Enum for the different KeyCode values for activation
	public enum ActivationKey
	{
		Fire1 = KeyCode.Alpha1,
		Fire2 = KeyCode.Alpha2,
		Fire3 = KeyCode.Alpha3,
		Fire4 = KeyCode.Alpha4,
		Fire5 = KeyCode.Alpha5,
		Fire6 = KeyCode.Alpha6,
		Fire7 = KeyCode.Alpha7,
		Fire8 = KeyCode.Alpha8,
		Fire9 = KeyCode.Alpha9,
		Fire0 = KeyCode.Alpha0
	}

	// Define the colors for the characters
	public static class CharacterColor
	{
		public static Color ThomasColor = new Color(0.6509434f, 0.2515722f, 0.2364275f);
		public static Color JohnColor = new Color(0.8196079f, 0.7137255f, 0);
		public static Color ClaireColor = new Color(0.259523f, 0.2940988f, 0.6792453f);
	}

	// Enum for the different types/states of objects in the world
	public enum WorldObjectType
	{
		Inactive,
		White,
		Thomas,
		John,
		Claire
	}
}