
//Naming - ex. H_H1_C1 (first letter (the H in this case) stands for the character (Haley in this case), the H1 stands for which hub it is in (Hub 1 in this case), and C1 is the dialogue choice)

=== Ending === ///This is just a place for the conversations to route back to so there are no errors
This is a placeholder
    * [...]
        -> END

=== H1 ===
= Haley
Haley: "Need something Harv?"
 * "Yeah, I was wondering[..."] if I did alright during that performance? I feel like it was bad, but the crowd seemed to like it so I'm confused."
        -> H_H1_C1
            
 * "I wanted check in on[..."] your equipment. Everything in working order?"
        -> H_H1_C2
 * [End convo]
        -> Ending

= H_H1_C1
Haley: "Don't worry about it. It was your first time really managing everything so of course it felt a bit rough."
    * "Thank goodness[."]. I was freaking out over here because I though I did bad."
            -> Haley
    * "When you put it that way[..."], it makes a lot of sense. I'll probably look back on this and laugh about it in the future"
            -> Haley

= H_H1_C2
"That would be really great! Mine has started making some funky noises anyways."
    * "I'll work on it["] then because that's kinda important."
        "Sweet! Thanks so much!"
            -> Haley
    * "I can later.["] Let me check with the others first."
        "Of course! I totally understand!"
            -> Haley



-> Haley

=== H2 ===
= Haley
Haley: "Hey Harv! What can I do for you?"
 * "About that venue[..."], was it good or what?"
        -> H_H2_C1
 * "Is your mic[..."] still in working condition?"
        -> H_H2_C2
 * [End convo]
        -> Ending


= H_H2_C1
Haley: "I'll admit, it was a lot better than I thought it would be."
    * "Great to hear[!"]. I'll keep it in mind if we every want to play there again."
        Haley: "Oh for sure, it was great!"
            -> Haley
    * "Thank goodness[."]. You were worried about it before so I was hoping you liked it."
        Haley: "You gotta admit, it did sound sketchy. But it worked out in the end and the crowd loved the vibe. That's all that matters."
            -> Haley

= H_H2_C2
"I don't think I actually need one right now, but I'd be happy to get one if you are offering?"
    * "Give me some time["] and I might be able to. Gotta have good equipment to help you perform."
        "Ain't that the truth!"
            -> Haley
    * "Maybe not["] now that I think about it. Let me check with the others though."
        "It's all good! Just let me know."
            -> Haley



-> Haley

=== H3 ===
= Haley
Haley: "What do you want Harvey?"
 * "Can we please talk[?"]? I know I screwed up."
        -> H_H3_C1
            
 * [End convo]
        -> Ending


= H_H3_C1
Haley: "What is there to talk about? Maybe about how you were just using us?"
    * "I know it seemed like that, but[..."] I promise that I wasn't doing that the entire time."
        Haley: "Even if I choose to believe you, the fact is that you still used us even if it was for a short period of time."
            -> Haley
    * "I was in the beginning, but[..."] I swear I stopped caring about the application soon after. I grew to love being with, and playing gigs with, you guys."
        Haley: "Well no matter the case, you should have told us about it sooner. You hurt all of us Harvey. You've caused pain that you probably can't heal."
            //Change this line?
            -> Haley
            

=== H4 ===
= Haley
Haley: "I thought long and hard about doing this, but it seems like everyone else wants you back too, so who am I to disagree? Want to officially rejoin the band Harvey?"
    * "Heck yeah I do.["] Thank you so much sis!"
        Haley: "Just please promise me you won't use us again like that?"
        "You have my word."
        -> Ending
    * "Yes, I do.["] Thank you so much for putting your faith in me again."
        Haley: "Just don't let me, or the others, down again."
        "I will do my best to avoid that. And if I do, let's hope it's just for forgetting to turn on the stage lights."
        -> Ending

=== Convo_2 ===
= Haley
Haley: "Oh hey Harv! While you're here, are you busy later today?"
    * "Not really["], why do you ask?"
        -> H_Convo2_C1
    * "Unfortunately, I am["]. I'm trying to find new venues. Did you need something from me?"
        -> H_Convo2_C2


= H_Convo2_C1
Haley: "Can you take us to the arcade? We all wannaa go and you're the only one of us who can drive so..."
    * "Oh for sure!["] I'm a pro at the racing game there."
        Haley: "I don't know... Ace is pretty good at it too. Let's go see who the champ is!"
            ** [Continue]
                -> Convo_2
    * "How about a movie instead?["] Stinker 3 just came out and I was thinking of seeing it soon."
        Haley: "None of us really like the series though. We can just go to the arcade another time then and you can watch the movie on your own."
            ** [Continue]
                -> Convo_2

= H_Convo2_C2
Haley: "Dang. I was gonna see if you wanted to hang out with us tonight."
    * "Oh. I'm sorry.["] I might be able to once I'm done."
        Haley: "Don't worry about it. We know you're busy, and you're helping us, so it'd be crappy for us to complain about it."
            ** [Continue]
                -> Convo_2
    * "How about a raincheck?["] I'll take you guys out to do something fun later this week instead."
        Haley: "That'll work! I'll go talk to the others so we can start planning something super duper fun."
            ** [Continue]
                -> Convo_2

-> Ending
