using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SplashQuote : MonoBehaviour {
    float quoteCount = 43f;
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
        }
        return "Wow my switch statement failed and now you are seeing this.";
    }
	
}
