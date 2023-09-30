INCLUDE globals.ink


//Naming - ex. H_H1_C1 (first letter (the H in this case) stands for the character (Haley in this case), the H1 stands for which hub it is in (Hub 1 in this case), and C1 is the dialogue choice)

-> H1

=== Ending === ///This is just a place for the conversations to route back to so there are no errors
This is a placeholder
->END



=== H1 ===
= Ace
Ace: "What up dude?" #speaker: Ace #portrait: ace_neutral
    * "Not much[."], just wanted to see what you were doing."
        -> A_H1_C1
    * "About your drums[..."] They still in playing condition?"
        -> A_H1_C2
    * [End convo]
        -> Ending

= A_H1_C1
Ace: "Oh, I'm watching TV. Wanna join?"
    * "But it's off["], how are you watching it?."
        Ace: "I'm imagining the episodes instead. I can explain it to you if you want?"
            "You know what, I'll pass."
                -> Ace
    * "I might later[..."] when there's actually something on."
        Ace: "Understandable. Just let me know if you do so I can make some room for you."
            -> Ace

= A_H1_C2
"Heck yeah I do! Are you offering?"
    * "Yeah["], I am. I can order one soon."
        "You're the man!"
            -> Ending
    * "Not now["] but I may be able to get one later in time."
        "Bummer. But I'm looking forward to it when you do."
            -> Ending






=== H2 ===
= Ace
Ace: "Yooo, Harvey. What up man?"
    * "That drum fill sounds[..."] pretty good. Is that new?"
        -> A_H2_C1
    * "Is everything alright with[..."] your drums?"
        -> A_H2_C2
    * [End convo]
        -> Ending


= A_H2_C1
Ace: "This? Heck yeah it is. I've been working on it for a few hours so far. I'm so close to getting it down."
    * "A few hours[?"]? Shouldn't you get some sleep?"
        Ace: "Well I've been drinking some neergy drinks so I think I'll be fine."
            -> Ace
    * "What's there left to add[?"] to it? It already sounds really cool."
        Ace: "Appreciate it man. I'm just trying to figure out where else I can add some more cymbals to."
            -> Ace

= A_H2_C2
"Not really. I've grown attached to this set. Although I do need some more sticks to use."
    * "Okay then["], I'll look into getting you some fancy new drumsticks."
        "Heck yeah!"
            -> Ending
    * "Oh. Okay.["] I was not expecting that response."
        "I got some surprises in me every once in a while."
            -> Ending






=== H3 ===
= Ace
Ace: "Oho, the villian of the evening. Here to kick me while I'm down?"
    * "Really? You too?["] I hoped you would be the understanding one."
        -> A_H3_C1
    * [End convo]
        -> Ending


= A_H3_C1
Ace: "I can't just let tyranny run rampant, can I? So, what do you want?"
    * "I'm trying[..."] to make amends with you guys."
        Ace: "I for one suggest you stop trying then. Take that little application of yours and run along now."
            -> Ace
    * "What can I do[?"] to make it up to you?"
        Kurt: How about you let us be really nice and helpful to you for a few weeks and then have you randomly learn that we were using you for ulterior motives? Then you can see what the pain feels like. It seems fair to me."
            -> Ace





=== H4 ===
= Ace
Ace: "I saw you perform, you really suck at it. How about I help you out next time?"
    * "I thought you were mad[..."] at me? Why would you do that?"
        Ace: "Ehhh, I couldn't stay mad at you forever. Plus I grew too used to working with you. I don't want to just switch back to not having you around, it'd be too much work."
                "I'll take it."
                -> Ending
    * "I would really appreciate[..."] the help. What made you change your mind?"
        Ace: "After thinking about what you said, it really started to seem like you had changed and only cared abour being our stage manager. You tried to do the right thing, but screwed it up. I can't be mad at you for that."
                "Well thanks for understanding. I hope the others come around too."
                -> Ending



=== Convo_2 ===
= Ace
Ace: "Yo, what up bro?"
    * "Clean up your room["] please. It's filthy and stinks like crazy."
        -> A_Convo2_C1
    * "Can you help me[..."] clean the kitchen?"
        -> A_Convo2_C2
        
= A_Convo2_C1
Ace: "What? No. That would mess up the feng shui of my room. That'd be like me asking you to shave your tail."
    * "Oh don't be dramatic["]. At least do something about the smell."
        Ace: "Now ,that, I can do. I just haven't had the energy to take any of my leftovers out to the trash."
            ** [Continue]
                -> Convo_2
    * "Well I've already done that["]. Remember when I was in highschool? I got gum stuck in there and didn't want to be left with a bald spot so I just shaved it all."
        Ace: "Oh yeah, I forgot about that. That was pretty freaky to see. But still, I don't want to clean my room. It's my safe space just the way it is."
            ** [Continue]
                -> Convo_2

= A_Convo2_C2
Ace: "What's the special occasion?"
    * "There isn't one["], it's just extremely dirty."
        Ace: "Bummer. I was really hoping there was one. Maybe like a stranger's birthday or even taco night? Something cool like that."
            ** [Continue]
                -> Convo_2
    * "The special occasion is dinner["]. We need to have space to actually make it."
        Ace: "Or we could just order takeout? There's a great place down on 71st street that I know of. No? Dang..."
            ** [Continue]
                -> Convo_2

-> Convo_2

