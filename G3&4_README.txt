							Homework G3&G4
Student: Maciej Bonowicz
DisplayRange: 1560-1579


1. Interactions
	I have added such classes to directory Engine\Interactions\NewInteractions:
	- BobInteraction.cs					//new interaction with Bob - provides a quest connected with ItemUpgradeInteraction and that affects MagicBobEncounter's strategy choice
	- ItemUpgradeInteraction.cs         //enables player to upgrade his items
	- MagicBobEncounter.cs				//Interaction with strategy depending on former events with BobInteraction
	- IMagicBobStrategy.cs				//Interface for strategy classes
	- MagicBobNormalStrategy.cs			//Basic strategy
	- MagicBobFriendlyStrategy.cs		//Beneficial strategy  -  set when things go well with BobInteraction
	- MagicBobMeanStrategy.cs			//Unwanted strategy  -  set when things go bad with BobInteraction, but also depends on how did the MagicBobNormalStrategy went

	To directory Engine\Interactions\InteractionFactories I have added:
	- ItemUpgradeInteractionFactory.cs  //Basic factory for ItemUpgradeInteractionFactory  -  could be connected with Bob and Magic Bob interactions' factory
	- BoBMagicBobInteracionFactory.cs	//Factory that creates together Bob and Magic Bob always together 

	In my case, Bob and Magic Bob are not connected like Gyrim and Hyrim. It doesn't matter which Bob or Magic Bob you choose, they all act like one and the same Interaction. I accomplished it using two newly created lists in Player class,
	which contain information about the Quests and their Executions. It also simplifies the possibility to check whether the relation between Interactions is visible.
	
	

2. Gameplay
	To present all the added aspects and occuring events, I have added extra starting items and learned skills to each class in Engine\GameSession.cs.
	All factories and interactions were added to Engine\Index.cs.
	I have also modified or added a few methods in the game files to achieve desired results.
