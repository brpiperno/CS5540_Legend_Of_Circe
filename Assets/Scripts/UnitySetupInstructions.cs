/*
Give the GameObject for Circe the tag "Player".
Give the GameObject for the opponent the tag "Enemy".

Create an empty GameObject called BattleManager. Add the BattleManager script to this object.
Drag the GameObjects for the player and the opponent to their respective fields in the BattleManager script.

Drag the EmotionSystem script onto both the GameObjects for Circe and the opponent. Attach the Circe Spell Cloud and Enemy Spell Cloud 
<<<<<<< HEAD
prefabs to both scripts. Set the coordinates: Circe cloud's position is (0.5, 1.5, -6), and rotation is (-90, 0, 0). Enemy cloud's position is (2.7, 1.5, 0),
rotation is (0, 0, 0)
=======
prefabs to both scripts
>>>>>>> ben

Drag the VisualController script onto the BattleManager GameObject. Add the objects

Drag each of the "arrow selected" Game Objects under Canvas to the appropriate fields in the Visual Controllers in Circe, the opponent, and the Battle Manager

Drag the "Strum" audio to the SFX fields in each Visual Controller

<<<<<<< HEAD
Credits:
Potion images from an asset pack by Josué Rocha
Sound effects by Dustyroom
=======
Set the slope limit for Circe's character controller to 60
Set move speed in the Third Person Controller script to 2.

Credits:
Potion images from an asset pack by Josué Rocha
Sound effects by Dustyroom
Additional tree assets from a pack by past12pm
Spider model by Dante's anvil
Mushroom assets from Papersy
Butterfly asset from Acorn Bringer
Throne room background is from https://stablediffusionweb.com/ko/image/5081178-ancient-greek-throne-room
Helios art is from https://www.youtube.com/watch?v=SfjekCNkA5A
Mushroom 2D asset from reddit user Street-Return2
Butterfly 2D asset from pixilart.com user abruptbird
Spider 2D asset from pixilart.com user amytoorMarn
Grief Mushroom ingreident 2D asset from redit user Aggravating-Carob580
Font is from https://www.fontspace.com/aiden-font-f25745
*/


/*  UI COORDINATES

Space prompt - position - (0, -425, 0)
For all of the text objects - do the shift alt anchor thing and set it to center center, then set each to the specified positions below.
Then in the Paragraph settings, select the 5th option (aligned in the middle vertically). Set all to font size 24
Enemy Grief bar - position - (550, 400, 0)
Enemy Love bar - position - (550, 370, 0)
Enemy Wrath bar - position - (550, 340, 0)
Enemy Mirth bar - position - (550, 310, 0)
Enemy Mirth text - position - (620, 312, 0)
Enemy Grief text - position - (620, 402, 0)
Enemy Love text - position - (620, 372, 0)
Enemy Wrath text - position - (620, 328, 0)
Win conditions - position - (-500, 350, 0)
	Size - (600, 125)
	Font size 30

Circe:
Font size 24:
Grief text - (-170, -498, 0)
Love text - (-170, -528, 0)
Wrath text - (-170, -572, 0)
Mirth text - (-170, -588, 0)
Grief bar - (-240, -500, 0)
Love bar - (-240, -530, 0)
Wrath bar - (-240, -560, 0)
Mirth bar - (-240, -590, 0)
Arrow group - position - (50, -440, 0)
Unselected arrows - position - (0, -540, 0)
	Size - (175, 175)
For the next four arrows, change the Scale to (1, 1, 1)
Up arrow selected - position - (-50, -58, 0)
	size - (50, 70)
Left arrow selected - position - (-103, -109, 0)
	size - (65, 56)
Right arrow selected - position - (4, -109, 0)
	size - (65, 56)
Down arrow selected - position - (-50, -158, 0)
	size - (50, 70)

TriggerBattlePanel in the Navigation scene - size - (2500, 500)
	Start Battle Text - change pos Y to 200
	Size - (1000, 100)
	Font size - 35
    Drag LUMOS font to it (in Assets)
*/

/*  MEDEA BATTLE ANIMATION COORDINATES

Player Animations
Position - (-118, 0.6, 97.8)
Rotation - (-90, -80.606, 0)

Medea Animations
Position - (-122, 1.32, 99.5)
Rotation - (0, -75, 0)
>>>>>>> ben
*/