using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SplashQuote : MonoBehaviour {
    float quoteCount = 104f;
	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = "\"" + getQuote() + "\"";
	}

    string getQuote() {

        switch ((int)Random.Range(0, quoteCount - 0.001f)) {
            case 0: return "We love chocolate. It is really tasty.";
            case 1: return "Wasting your time since 2016!";
            case 2: return "Winner of the 'Best splash screen on the market' Award.";
            case 3: return "Don't burn your dinner!";
            case 4: return "Always look behind you when typing passwords!";
            case 5: return "Chicken tastes like chicken. Coincidence? I think not.";
            case 6: return "Fire is hot. Trust me.";
            case 7: return "Did I forget to turn off the stove?";
            case 8: return "I'm hungry. What's for lunch?";
            case 9: return "Sometimes, the best defence is bacon.";
            case 10: return "How many more of these do I have to come up with?";
            case 11: return "Those lightbulbs won't change themselves!";
            case 12: return "Touchscreen? Wow! What a day to be alive!";
            case 13: return "This is the 13th possible message. Whoo! *Crosses fingers*";
            case 14: return "Bananas spoil slower when separated.";
            case 15: return "Don't feed Parakeets Avocado. Seriously.";
            case 16: return "Shouldn't you be studying?";
            case 17: return "Knock knock. (Say 'Who's there')";
            case 18: return "Jumping jacks are healthy!";
            case 19: return "Don't eat the yellow snow!";
            case 20: return "Hey John! Yeah you! I guessed your name! (Worth a try)";
            case 21: return "TOP SECRET: Fish fingers are not made of fingers";
            case 22: return "Dragon fruits look so cool!";
            case 23: return "Falcon PUNCH!";
            case 24: return "FUS-RO-DAH!";
            case 25: return "Han Solo Dies.";
            case 26: return "'Luke, I am your father' is misquoted. Should be: 'No. I am your father'";
            case 27: return "Batteries Included!";
            case 28: return "ALERT: DEFCON 5";
            case 29: return "Dive, Dive, Dive!";
            case 30: return "3 games walk into a bar...";
            case 31: return "Choo choo! I'm a train.";
            case 32: return "Occupy Mars.";
            case 33: return "Check your email.";
            case 34: return "Now Hiring! (maybe)";
            case 35: return "You are wearing a black shirt. (Worth a try)";
            case 36: return "Don't forget to flush";
            case 37: return "Making the world a funner place.";
            case 38: return "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            case 39: return "I can count to 100!";
            case 40: return "It is spelled 'a lot' not 'alot'";
            case 41: return "Don't do drugs!";
            case 42: return "Happy birthday!";
            case 43: return "Who farted?";
            case 44: return "Winter is coming.";
            case 45: return "You know nothing, Jon Snow.";
            case 46: return "They may take our lives, but they’ll never take our freedom!";
            case 47: return "I’ll be back.";
            case 48: return "Have you seen my bear Tibbers?";
            case 49: return "Gems are truly, truly, truly outrageous.";
            case 50: return "Chewie, we’re home.";
            case 51: return "Who let the dogs out?";
            case 52: return "Don’t talk to strangers!";
            case 53: return "Mitochondria is the powerhouse of the cell.";
            case 54: return "Bounce batteries to see if they are good or bad.";
            case 55: return "Tomato is a fruit.";
            case 56: return "Brush your teeth!";
            case 57: return "Don’t forget to floss!";
            case 58: return "Don’t forget an umbrella.";
            case 59: return "Ask for both beans at Chipotle.";
            case 60: return "Did you turn off all the lights?";
            case 61: return "Pluto isn’t a planet, RIP.";
            case 62: return "GGWP!";
            case 63: return "All hail the dark lord Teemo.";
            case 64: return "Don’t chase Singed!";
            case 65: return "Demacia!";
            case 66: return "Double rainbow, what does it mean?";
            case 67: return "Oh baby a triple!";
            case 68: return "RYUU GA WAGA TEKI WO KURAU!";
            case 69: return "Pikachu has fainted!";
            case 70: return "I am the one who knocks!";
            case 71: return "Say my name... Heisenberg";
            case 72: return "Wombo Combo!";
            case 73: return "PENTAKILL!";
            case 74: return "360 No-Scope!";
            case 75: return "Fabio’s hair is epic.";
            case 76: return "How was your day?";
            case 77: return "Everything’s gonna be alright.";
            case 78: return "Is your refrigerator running?";
            case 79: return "Why did the chicken cross the road?";
            case 80: return "Pen: *click click click click*";
            case 81: return "2 + 2 = fish.";
            case 82: return "1337!";
            case 83: return "I luv u!";
            case 84: return "My fingers hurt!";
            case 85: return "Carpal Tunnel is a serious disease.";
            case 86: return "In seven different flavors!";
            case 87: return "I broke my crayon again!";
            case 88: return "Brb (#2).";
            case 89: return "Magikarp used splash!";
            case 90: return "95% Organic!";
            case 91: return "Let it go!";
            case 92: return "The Lion King makes me cry.";
            case 93: return "Whoa, that’s heavy Doc!";
            case 94: return "We’re going to need a bigger boat.";
            case 95: return "One small step for man.";
            case 96: return "This. Is. Spartaaa!";
            case 97: return "_____ [GONE WRONG].";
            case 98: return "It’s a prank bro!";
            case 99: return "I need more ketchup!";
            case 100: return "Do you want to build a snowman?";
            case 101: return "Apple seeds are poisonous (true fact)";
            case 102: return "Giraffe tongues are purple.";
            case 103: return "So. Much. Swiping.";
        }
        return "Wow my switch statement failed and now you are seeing this.";
    }
	
}
