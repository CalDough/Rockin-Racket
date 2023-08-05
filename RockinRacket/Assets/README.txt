Current Project Organization
OutsideRockinRacketFolder>FMOD/ This is all the audio stuff

OutsideAssetsFolder>RockinRacket/Player/Files/SavedData
I'm saving player data very close for ease of access

Assets
>AdapativePerformance //Ignore
>Animations //Animation files for sprites and controllers
>Controls //Unitys new input system control files
>Editor //This is for scripts that modify viewing things in editor
>Ink //Default ink folder, has the basics for how it works
>InkInfo // Read above
>Plugins //This has stuff for FMOD, don't bother it
>Prefabs //All level prefabs, mini games, character gameobjects, etc 
>Resources //This is for on demand loading for certain things such as images
>Scenes //All scenes for the game, has some important prefabs for singletons
>ScriptableObjects //Contains all scriptables objects in organized folders
>Scripts
	>Animals //C# classes for animal attendees and the band
	>Audio //C# classes for audio (must know FMOD to use)
	>Concert //C# classes for managing the concert and its events, game state, and venues
	>Inventory //C# classes for the inventory and items
	>Levels //C# classes for an overworld level movement like mario
	>Story //C# classes for story related scripting
	>UserInterface //C# classes for the UI
	Attribute //C# enum for some important types
	GameManager //C# class for global variables across all scenes
	TimeEvent //C# class for global events like pausing

>Settings
>Sprites //All sprite data that is not loaded on demand
>TextMeshPro //Default Text Mesh Pro assets
