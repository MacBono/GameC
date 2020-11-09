							Homework G1&G2
Student: Maciej Bonowicz
DisplayRange: 1560-1579

1. General info
	Beside adding new classes to the program, I had to modify some of them in order to balance the game(for my usage) or to be implement several
new mechanics I  have added. I will try to explain every additional part according with the homework. Let's start with the Monsters.

2. Monsters
	**I have slightly modified Subject and Player classes to improve the combat. I will present it on an example: 
		BEFORE: A player is attacked by monster - the attack is supposed to reduce his Armor. He has 0 basic Armor and 20 from an item. So when
			the React is executed it sees that player would result having (0-20=)-20 armor, so would change the value to 0 and then add 20 from item.
		AFTER: I changed it in such a way that this situation doesn't happen - the armor is reduced during combat, but after goes back to normal value.
		So in Engine\Subject.cs and Engine\CharacterClasses\Player.cs I have changed all Properties important for combat (Health, Armor, Precision, etc.).
	*I have expanded Engine\Monsters\Monster.cs in a:
		- protected field stun (enables a stunning mechanic I've created)
		- modified the process of receiving damage: depenging on type it is reduced according to value of Armor(physical dmg) or Magic Power(magic dmg) of the Monster.

	I have added such classes to directory Engine\Monsters:
	- RedBlob.cs         //simple monster
	- AcidBlob.cs        //after reducing its Health to 50% it enrages and gets stronger
	- IronElemental.cs   //immune to stun
	- AirElemental.cs
	- EarthElemental.cs
	- FireElemental.cs
	- WaterElemental.cs
	
	Each Elemental has overriden React function in order to apply immunities. 
	The four-element Elementals are all covering a mechanic, that every 3rd attack is different. Each of them is immune to its element's type of attack.
	Information about immunity is present on the screen during battle.
	
	Here also worth to mention that I expanded the Engine\StatPackage.cs class with necessary parametric constructors.

	I have added such factories to directory Engine\Monsters\MonsterFactories:
	- BlobFactory         //produces RedBlob and after it dies an AcidBlob
	- ElementalFactory    //produces at first IronElemental and later (infinitely) creates one of four other elementals, chosen randomly 

3. Items
	I have created a new directory Engine\Items\EpicItems where I put all my new classes:
	- BurnProtectionAmulet.cs   // immunity to "burn" type damage and extra magic power
	- AntiPoisonRobe.cs         // immunity to "poison" type of damage
	- FridaySword.cs            // 50% chance to stun the enemy with any attack
	- VampireTooth.cs           // +10 precision, +10 strength and gives 25% chance to heal for half the damgae dealt
	
	I modified the items using methods ModifyDefensive(), ModifyOffensive() and ApplyBuffs().

	I have created a new factory Engine\Items\ItemFactories\EpicItemFactory.cs which generates only the *epic* items, which are the ones listed above.

4. Skills
	***I wanted to add each character class a unforgetable basic skill, that doesn't require any weapon and its damage improves only with level. They are both located in:
		- Engine\Skills\BasicSkills\Hadouken.cs  // It is a basic spell for Mage.
		- Engine\Skills\BasicSkills\Punch.cs     // It is a basic spell for Warrior.
		In order to achieve that I had to modify several deeper files:
		- Engine\GameSessionPublicLogic.cs - I expanded a TestForItemClass() method with a "none" weapon type case
		- Engine\Interactions\Interactions\InteractionFactories\SkillForgetFactory.cs - I modified it, so it's unable to forget the basic spells
		- Engine\GameSession.cs - just adding them along with other starting skills

	**I have added Weapon skills created by me to Engine\Skills\SkillFactories\BasicWeaponFactory.cs in order to be able to learn them as basic skills.
	*I have expanded every basic weapon move(located in Engine\Skills\BasicWeaponMoves) with it's decorator. 

	I have created a new directory Engine\Skills\NewWeaponMoves where I put all my new classes:
	- CloneAxe.cs 
	- CloneAxeDecorator.cs
	- LightSpear.cs 
	- LightSpearDecorator.cs
	- SharpenSword.cs
	- SharpenSwordDecorator.cs
	All the skills are just performing another attack, but each is assigned to only one weapon type.
	
	I have created new factories in directory Engine\Skills\SkillFactories :
	- AxeSpellFactory.cs    //allows 2-skill combo
	- SpearSpellFactory.cs  //allows 3-skill combo
	- SwordSpellFactory.cs  //allows 3-skill combo
	The idea was to allow spell combos for only one type of a weapon.

5. Gameplay
	To present all the added aspects and occuring events, I have added extra starting items and learned skills to each class in Engine\GameSession.cs.
	All factories and items were added to Engine\Index.cs.
