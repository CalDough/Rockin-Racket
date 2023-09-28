VAR Talked_to_siblings_Hub_1 = false
VAR Talked_to_siblings_Hub_2 = false
VAR Talked_to_siblings_Hub_3 = false
VAR Talked_to_siblings_Hub_4 = false
VAR Siblings_rejoin = 0


// If pose changes then have a hastag line in here with the action wanted to happen with it. Then when the script sees that hastag line, it can load whatever sprite or something that is needed.

-> Start_lvl_1

=== Start_lvl_1 ===
-> Cine_1 -> Lvl_1 -> Cine_2 -> Hub_1


= Cine_1

Cine 1:

The room is dark with little light seeping through the curtains. The rooms looks to be a mess. Harvey is still asleep on his bed, snuggled underneath a blanket with his tail poking out.

Haley: "Get up you big grouch, we need you!"

Harvey: "Ughhh. What do you want Haley?"

Haley: "Please don't tell me you forgot about the gig today. You promised you would start helping us."

Harvey: "Oh crap. I'll be there in a second, I gotta get dressed."

Haley: "Well hurry up, it starts in 20 minutes."

Harvey: "ok, ok. I get it. I'll be down in a minute."

Harvey stops at his dresser, which is covered in letters and paper, to grab an application of some kind. The hobbies section and volunteer work sections are left unfilled. He quickly puts it in his pocket and runs out the door.

 * [...]
        ->->



= Lvl_1
This is level 1, "Garage Band". Nothing is here in this text version.
 * [...]
        ->->

= Cine_2
Cine 2:

The band and Harvey just finished their first gig together. It was a bit messy, but the crowd seemed to like it.

Kurt: "Did you see that guy jumping around? He was super into it!"

MJ: "Heck yeah I did, it must've been because of my sick riffs."

Ace: "Or mine. You never know."

Haley: "I'm just glad we made it through that. It was still a bit rough, but you really helped save it Harv."

Harvey: "Honestly, I'm surprised too. There was a lot going on all at once."

Haley: "Don't worry, it's always like that at first, you'll get used to it."

Kurt: "Can we head in now? I'm getting hungry."

Haley: "Agreed."

 * [...]
        ->->



=== Hub_1 ===
{ Talked_to_siblings_Hub_1 == false:
    You head back inside the house after your performance in the garage. It didn't go as you expected, but it was still successful.
 - else:
    What to do next?
}

 * [Talk to my siblings]
    -> Hub_1_Talk_to_siblings
 
 + [Set up another garage concert]
    -> Lvl_1_again
 
 * [Head to the next venue]
    -> Start_lvl_2
    
 //* [Skip ahead to level 4 (for testing)]
    //-> Start_lvl_4


=== Hub_1_Talk_to_siblings ===
Who do you wish to talk to? 
 * [Haley (singer)]
    -> Haley
 * [Kurt (bass guitar)]
    -> Kurt
 * [Ace (drums)]
    -> Ace
 * [MJ (lead guitar)]
    -> MJ
 * [Nevermind]
    ~ Talked_to_siblings_Hub_1 = true
    -> Hub_1





= Haley
Haley: "Need something Harv?"
 * "Yeah, I was wondering[..."] if I did alright during that performance? I feel like it was bad, but the crowd seemed to like it so I'm confused."
        -> Haley_option1
            
 * "I wanted check in on[..."] your equipment. Everything in working order?"
        {~ -> Haley_option2_1 | -> Haley_option2_2}
 * [End convo]
        -> Hub_1_Talk_to_siblings

= Haley_option1
Haley: "Don't worry about it. It was your first time really managing everything so of course it felt a bit rough."
    * "Thank goodness[."]. I was freaking out over here because I though I did bad."
            -> Haley
    * "When you put it that way[..."], it makes a lot of sense. I'll probably look back on this and laugh about it in the future"
            -> Haley

= Haley_option2_1
"That would be really great! Mine has started making some funky noises anyways."
    * "I'll work on it["] then because that's kinda important."
        "Sweet! Thanks so much!"
            -> Haley
    * "I can later.["] Let me check with the others first."
        "Of course! I totally understand!"
            -> Haley

= Haley_option2_2
"


-> Haley




= Kurt
Kurt: "Oh, h-h-h-hey Harvey. Do you n-n-need something?"
    * "Just checking in[."]. Are you looking for something?"
        -> Kurt_option1
    * "Yeah, I wanted to check[..."] if your bass guitar was okay?"
        { ~ -> Kurt_option2_1 | -> Kurt_option2_2}
    * [End convo]
        -> Hub_1_Talk_to_siblings

= Kurt_option1
Kurt: "Y-y-yeah, I am. I lost that l-l-little case I put my guitar p-p-picks in. I thought I left if on the shelf but it's n-n-not there anymore."
    * "That sucks[."] I'm sure it's bound to show back up eventually."
        Kurt: "I know, I'm j-j-just stressed about it."
            -> Kurt
    * "Have you thought about checking[...] in with MJ? Maybe she took it?"
        Kurt: "You have a p-p-point. Thanks!"
            -> Kurt

= Kurt_option2_1
"I don't think I absolutely need one right now, but it would be awesome if I did. It would sound so much better."
    * "I can get it["] for you here soon."
        "Best manager in the making right here."
            -> Kurt
    * "I'll think about it.["] I obviously don't want to prioritize yours if someone else needs new gear more than you."
        "It's all good. Thanks for thinking about me though."
            -> Kurt

= Kurt_option2_2

-> Kurt




= Ace
Ace: "What up dude?"
    * "Not much[."], just wanted to see what you were doing."
        -> Ace_option1
    * "About your drums[..."] They still in playing condition?"
        -> Ace_option2
    * [End convo]
        -> Hub_1_Talk_to_siblings

= Ace_option1
Ace: "Oh, I'm watching TV. Wanna join?"
    * "But it's off["], how are you watching it?."
        Ace: "I'm imagining the episodes instead. I can explain it to you if you want?"
            "You know what, I'll pass."
                -> Ace
    * "I might later[..."] when there's actually something on."
        Ace: "Understandable. Just let me know if you do so I can make some room for you."
            -> Ace

= Ace_option2
"Heck yeah I do! Are you offering?"
    * "Yeah["], I am. I can order one soon."
        "You're the man!"
            -> Hub_1_Talk_to_siblings
    * "Not now["] but I may be able to get one later in time."
        "Bummer. But I'm looking forward to it when you do."
            -> Hub_1_Talk_to_siblings





= MJ
MJ: "What do you need? I'm busy."
    * "Sorry to interrupt[..."], I just wanted to see what you got going on."
        -> MJ_option1
    * "How's your guitar[?"]? Is it doing well?"
        -> MJ_option2
    * [End convo]
        -> Hub_1_Talk_to_siblings

= MJ_option1
MJ: "I'm practicing some songs."
    * "Practicing on a Sunday[?"]? Shouldn't you be resting?"
        MJ: "Rest is for the weak. And I'm not weak."
            -> MJ
    * "How about taking[..."] a break?"
        MJ: "I'll take a break when I'm done. Can I get back to it now?"
            -> MJ

= MJ_option2
"I could use one. You buying?"
    * "I got you.["] Give me a bit and you'll have it."
        "Thanks."
            -> MJ
    * "Maybe later.["] I need to rack up some more cash before I buy it."
        "I won't get my hopes up too much then."
            -> MJ






=== Lvl_1_again ===
This is where you would replay level 1 again.

 * [...]
    -> Hub_1
















=== Start_lvl_2 ===
-> Cine_3 -> Lvl_2 -> Cine_4 -> Hub_2

= Cine_3
Cine 3:

The band is sitting in their house's den, all watching TV. Harvey rushes in with a piece of paper in his hand.

Harvey: "Guys! You are not gonna believe what I have right here."

Kurt: "Tickets for a vacation?"

Ace: "Paid off medical bills?"

MJ: "A lease for a new house?"

Haley: "Checks for large amounts of money?"

Harvey: "So maybe those are all better guesses than the truth, but no. I got us permission to use that old barn in the woods for our next concert!"

Haley: "That's great and all but are you talking about the run down one?"

Harvey: "I mean, it's been repaired and decorated, so it's not that bad anymore. But it's a way better venue than our garage, right? Let's at least give it a shot."

Haley: "Might as well."

Harvey: "Sweet. In the meantime, who wants to help hand out flyers?"

 * [...]
        ->->


= Lvl_2
You would be starting the 2nd level here ("Barnyard Bash").

 * [...]
        ->->

= Cine_4

Cine 4:

Harvey: "That was sick guys, great job!"

Ace: "Give yourself some credit too dude."

MJ: "Can't believe I'm saying this but Ace's right. I didn't think you had it in you."

Haley: "Yeah, I hate to say it Harv, but you usually fail to stick around with anything this long."

Harvey: "That's not true-"

Kurt: "Baseball, hocky, theater, painting, writing books -"

Harvey: "Okay, I get it Kurt. But it's different this time, this is actaully fun."

Haley: "Let's hope you keep that attitude then because it's only gonna get more hectic at the rate the crowds are growing"

Harvey: "You got a point. Ah, I forgot to shut out the lights. You guys go on ahead to the van and I'll be there in a second."

Harvey pulls out the application and stares at it with a questioning face. After a second, he stuffs it back into his pocket.


 * [...]
        ->->





=== Hub_2 ===
{ Talked_to_siblings_Hub_2 == false:
    After your performance at the abandoned barn, you rejoin the band at your house.
 - else:
    What to do next?
}

 * [Talk to my siblings]
    -> Hub_2_Talk_to_siblings
 
 + [Set up another barn concert]
    -> Lvl_2_again
 
 * [Head to the next venue]
    -> Start_lvl_3




=== Hub_2_Talk_to_siblings ===

// Need to change the dialogue

Who do you wish to talk to? 
 * [Haley (singer)]
    -> Haley
 * [Kurt (bass guitar)]
    -> Kurt
 * [Ace (drums)]
    -> Ace
 * [MJ (lead guitar)]
    -> MJ
 * [Nevermind]
    ~ Talked_to_siblings_Hub_2 = true
    -> Hub_2


= Haley
Haley: "Hey Harv! What can I do for you?"
 * "About that venue[..."], was it good or what?"
        -> Haley_option1
 * "Is your mic[..."] still in working condition?"
        {~ -> Haley_option2_1 | -> Haley_option2_2}
 * [End convo]
        -> Hub_2_Talk_to_siblings


= Haley_option1
Haley: "I'll admit, it was a lot better than I thought it would be."
    * "Great to hear[!"]. I'll keep it in mind if we every want to play there again."
        Haley: "Oh for sure, it was great!"
            -> Haley
    * "Thank goodness[."]. You were worried about it before so I was hoping you liked it."
        Haley: "You gotta admit, it did sound sketchy. But it worked out in the end and the crowd loved the vibe. That's all that matters."
            -> Haley

= Haley_option2_1
"I don't think I actually need one right now, but I'd be happy to get one if you are offering?"
    * "Give me some time["] and I might be able to. Gotta have good equipment to help you perform."
        "Ain't that the truth!"
            -> Haley
    * "Maybe not["] now that I think about it. Let me check with the others though."
        "It's all good! Just let me know."
            -> Haley

= Haley_option2_2


-> Haley



= Kurt
Kurt: "Hi there H-h-harvey. Can I help you?"
    * "Got any advice for me[?"]? I feel like I'm starting to get the hang of being a stage manager, but I wanted to know if there was anything else to know?"
        -> Kurt_option1
    * "Is your bass okay[?"] or is there something i should know about?"
        { ~ -> Kurt_option2_1 | -> Kurt_option2_2}
    * [End convo]
        -> Hub_2_Talk_to_siblings

= Kurt_option1
Kurt: "I d-d-don't think so. Just p-p-pay attention to both the st-st-stage and the audience. Make the experience a-a-amazing for them."
    * "You have a good point[."] It's just a bit hectic, ya know?"
        Kurt: "We d-d-did warn you. But you're doing f-f-fine so far so don't worry about i-i-it."
            -> Kurt
    * "They can be really energetic[..."] and loud sometimes so I doubt I'll miss anything from them."
        Kurt: "Agreed. I worry about the d-d-day they are louder than u-u-us though because that would b-b-be t-t-terrifying."
            -> Kurt

= Kurt_option2_1
"Now that you mention it, yeah. The neck of it is starting to crack."
    * "I'll take care of it["] and get a new one here soon then."
        "You're the best!"
            -> Kurt
    * "Give me some time["]. Funds are low right now so hopefully it can last a bit longer."
        "It should last a few more gigs as long as I'm careful"
            -> Kurt


= Kurt_option2_2

-> Kurt


= Ace
Ace: "Yooo, Harvey. What up man?"
    * "That drum fill sounds[..."] pretty good. Is that new?"
        -> Ace_option1
    * "Is everything alright with[..."] your drums?"
        -> Ace_option2
    * [End convo]
        -> Hub_2_Talk_to_siblings


= Ace_option1
Ace: "This? Heck yeah it is. I've been working on it for a few hours so far. I'm so close to getting it down."
    * "A few hours[?"]? Shouldn't you get some sleep?"
        Ace: "Well I've been drinking some neergy drinks so I think I'll be fine."
            -> Ace
    * "What's there left to add[?"] to it? It already sounds really cool."
        Ace: "Appreciate it man. I'm just trying to figure out where else I can add some more cymbals to."
            -> Ace

= Ace_option2
"Not really. I've grown attached to this set. Although I do need some more sticks to use."
    * "Okay then["], I'll look into getting you some fancy new drumsticks."
        "Heck yeah!"
            -> Hub_2_Talk_to_siblings
    * "Oh. Okay.["] I was not expecting that response."
        "I got some surprises in me every once in a while."
            -> Hub_2_Talk_to_siblings







= MJ
MJ: "Need something?"
    * "Actually, yeah. Could you[..."] lower the volume a bit?"
        -> MJ_option1
    * "Is your guitar[..."] holding up properly?"
        -> MJ_option2
    * [End convo]
        -> Hub_2_Talk_to_siblings

= MJ_option1
MJ: "Is it bothering you?"
    * "It is. I don't mind[..."] you practicing, but some of us are trying to get some sleep. So can you turn it down now?"
        MJ: "Fine. But next time just wait until I'm done before you go to bed."
            -> MJ
    * "Well considering you're in the garage[..."], making the sounds echo and become louder, the yes. It is bothering me."
        MJ: "Noted. I'll turn it down then."
            -> MJ

= MJ_option2
"I could use one. You buying?"
    * "I got you.["] Give me a bit and you'll have it."
        "Thanks."
            -> MJ
    * "Maybe later.["] I need to rack up some more cash before I buy it."
        "I won't get my hopes up too much then."
            -> MJ




=== Lvl_2_again ===
This is where you would replay the level again. But since I'm not in charge of that, I'm keeping that out for now.
 * [...]
    -> Hub_2
















=== Start_lvl_3 ===
-> Cine_5 -> Lvl_3 -> Cine_6 -> Hub_3


= Cine_5

Cine 5: 

While eating together at the dinner table, Harvey pulls out a small box and hands it to Haley.

Haley: "What is this?"

Harvey: "It's an early birthday present for you guys."

Kurt: "Why give it to us now then?"

Harvey: "You'll see. Open it Haley."

Haley: "Tickets? Wait, TICKETS TO THE BATTLE OF THE BANDS! WE'RE GONNA GET TO SEE THAT?"

Harvey: "Even better. You're gonna be playing in it."

MJ: "Shut up right now. You're lying."

Harvey: "Nope. We just barely qualified and I was able to cover the entrance fee as performers. We better start practicing now if we want a shot at winning."

Ace: "Let's get to it then!"

 * [...]
        ->->

= Lvl_3
Here is where you would play level 3 "Battle of the Bands".

 * [...]
        ->->


= Cine_6

Cine 6: 

Haley: "Holy crap, I can't belive we won the first match! That was crazy."

Kurt: "It was so crazy that I feel like puking."

MJ: "Please don't."

Harvey: "Don't celebrate too early, we still have a few match-ups before we win it all."

Ace: "Hey Harvey, you dropped something. Let me get it for you."

Harvey: "Thank- wait don't!"

Ace: "Application to the University of [Insert name here]... Needs hobby information and volunteer work. Are you leaving us?"

Haley: "Volunteer work, is that supposed to be you helping us? Are you using us?"

MJ: "You just wanted to help us so you could have a better shot at getting into college? What the heck dude!"

Harvey: "It's not what you think, I-"

Haley: "Zip it, we don't wanna hear it. That makes so much sense now as to why you actually stuck with it this time, so you can put it on a resume."

Kurt: "But, I thought you liked this. Working with us was just a lie?"

Haley: "I'm not gonna let you use us like this anymore. We don't want you help anymore, so just leave us alone."

Harvey: "But we still have matches to play?"

Haley: "No, WE have matches to play. YOU have to go back home. You're officially out of the band."

Harvey: "But-"

The band members walk off, leaving Harvey alone while he clutches the application.

Harvey: "But I don't want to go to college anymore. I found my purpose."


 * [...]
        ->->



=== Hub_3 ===
{ Talked_to_siblings_Hub_3 == false:
    After your performance, you rejoin the band at your house.
 - else:
    What to do next?
}

 * [Talk to my siblings]
    -> Hub_3_Talk_to_siblings
 
 + [Set up another barn concert]
    -> Lvl_3_again
 
 * [Head to the next venue]
    -> Start_lvl_4




=== Hub_3_Talk_to_siblings ===

// Need to change the dialogue. They have found out about the "betrayal" at this point and need to show that.

Who do you wish to talk to? 
 * [Haley (singer)]
    -> Haley
 * [Kurt (bass guitar)]
    -> Kurt
 * [Ace (drums)]
    -> Ace
 * [MJ (lead guitar)]
    -> MJ
 * [Nevermind]
    ~ Talked_to_siblings_Hub_3 = true
    -> Hub_3


= Haley
Haley: "What do you want Harvey?"
 * "Can we please talk[?"]? I know I screwed up."
        -> Haley_option1
            
 * [End convo]
        -> Hub_3_Talk_to_siblings


= Haley_option1
Haley: "What is there to talk about? Maybe about how you were just using us?"
    * "I know it seemed like that, but[..."] I promise that I wasn't doing that the entire time."
        Haley: "Even if I choose to believe you, the fact is that you still used us even if it was for a short period of time."
            -> Haley
    * "I was in the beginning, but[..."] I swear I stopped caring about the application soon after. I grew to love being with, and playing gigs with, you guys."
        Haley: "Well no matter the case, you should have told us about it sooner. You hurt all of us Harvey. You've caused pain that you probably can't heal."
            //Change this line?
            -> Haley




= Kurt
Kurt: "P-p-please don't tell me you're here t-t-to hurt me again?"
    * "No, I'm not[..."] Kurt. I wanted to say that I'm sorry for the pain I caused you. I know what I did was bad, but you have to believe me when I say that I changed."
        -> Kurt_option1
    * [End convo]
        -> Hub_3_Talk_to_siblings


= Kurt_option1
Kurt: "A-a-apoligizing won't heal the p-p-pain. I'm more upset with m-m-myself for believing you when you offered t-t-to help us. You hate that k-kind of thing."
    * "This time was different.["] I really started to love helping you guys. It gave me something to look forward to."
        Kurt: "Well n-n-now that's gone, so way to g-g-go."
            -> Kurt
    * "Please don't beat yourself up[..."] about it, it's my fault. I didn't want to risk telling you guys so I thought if I ignored it then it would all be okay."
        Kurt: "You're m-m-mistake was keeping the ap-p-plication. You should have t-t-tossed it out."
            -> Kurt





= Ace
Ace: "Oho, the villian of the evening. Here to kick me while I'm down?"
    * "Really? You too?["] I hoped you would be the understanding one."
        -> Ace_option1
    * [End convo]
        -> Hub_3_Talk_to_siblings


= Ace_option1
Ace: "I can't just let tyranny run rampant, can I? So, what do you want?"
    * "I'm trying[..."] to make amends with you guys."
        Ace: "I for one suggest you stop trying then. Take that little application of yours and run along now."
            -> Ace
    * "What can I do[?"] to make it up to you?"
        Kurt: How about you let us be really nice and helpful to you for a few weeks and then have you randomly learn that we were using you for ulterior motives? Then you can see what the pain feels like. It seems fair to me."
            -> Ace




= MJ
MJ: "Look who it is. The betrayer."
    * "How many times do I[..."] need to apologize and explain myself to you guys?"
        -> MJ_option1
    * [End convo]
        -> Hub_3_Talk_to_siblings

= MJ_option1
MJ: "At least once more for me. How could you do something like that to us? To me?"
    * "I was originally hoping that[..."] the experience would help me get into college. But ever since the gig at the barn, I loved playing with you guys and college seemed less desirable."
        MJ: "You could have told us sooner about it. I'm sure we would have understood. But no, instead we had to figure it out ourselves after we grew attached to having you around helping us."
            -> MJ
    * "I wanted to tell you guys[..."], but I just got so caught up in working. It made me forget all about the application until it fell out of my pocket. I swear I didn't mean to hurt you guys."
        MJ: "Whether you meant to or not, it doesn't change the fact that you did. You broke our trust in you. That's something that's hard to fix, you know."
        -> MJ



=== Lvl_3_again ===
This is where you would replay the level again. But I don't have anything here right now for that.

// Should they be able to replay level, after that story beat? Should they be able to replay any level after it? How will that work or is it not that big of a deal?"

-> Hub_3















=== Start_lvl_4 ===
-> Cine_7 -> END //Lvl_4 -> Cine_8 -> Hub_4


= Cine_7

Cine 7:

Harvey finds the band members scattered around the house and brings them all to the den. They are not happy.

Harvey: "I know you guys hate my guts right now and are upset because you lost at battle of the bands, but can you at least hear me out?"

MJ: "Why, so you can lie to us again?"

Harvey: "No, I won't. I want to explain."

Haley: "Fine. But only because you owe us one."

Harvey: "Yes, I was originally planning on using this experience to help me get into college. None of the other things I have done would be useful, so I figured being a stage manager would. But after helping you guys, being a part of the team, I really enjoyed myself."

Kurt: "So that means-"

Harvey: "Yeah, I was gonna give up on the idea of college and continue working with you guys. I soon realized that being a manager to you guys was gonna make me a lot more happy than a college experience would."

Haley: "So what do you want from us?"

Harvey: "I want a shot at regaining my place on the band. I want to work with you guys."

Ace: "Not that easy dude."

Haley: "Ace is right. It doesn't matter that you changed your mind. The fact that you still had the application meant some part of you wanted to still betray us. You should have tossed it out a lot sooner. You'll have to find some miracle to get back in the band."

Ace: "Those are hard to find. Trust me, I've tried."

Harvey: "Fine. Then I'll find one so I can rejoin you guys. I already have an idea too."

Haley: "We'll see about that."

 * [...]
        ->->



/* = Lvl_4
This is where you would play level 4 "Hellhound Hill".

// How is this level actually going to be played? Is Harvey trying to play the instruments himself AND managing the stage? Figure it out because it may change up the story beats.

 * [...]
        ->->



= Cine_8

Cine 8:

Harvey just played a gig all on his own in front of a small crowd. It was a total mess.

Harvey: "Jeez, I am exhausted. How do they make it look so easy when performing? Gotta give credit where credit's due. Anyways, I really hope this plan works. If not then I've made a huge fool of myself for nothing."


 * [...]
        ->->


=== Hub_4 ===
{ Talked_to_siblings_Hub_4 == false:
    Afterwards, you head back to the house.
 - else:
    What to do next?
}

 + [Talk to my siblings]
    -> Hub_4_Talk_to_siblings
 
 + [Play "Hellhound Hill" again]
    -> Lvl_4_again
    
 * [To cine 9 for testing]
    -> Cine_9
    



=== Hub_4_Talk_to_siblings ===


{
    - Siblings_rejoin == 1:
        
        + [Ace (drums)]
            -> Ace
            
        + [Nevermind]
            ~ Talked_to_siblings_Hub_4 = true
            -> Hub_4    
    - Siblings_rejoin == 2:
        
        + [Ace (drums)]
            -> Ace
            
        + [Kurt (bass guitar)]
            -> Kurt
            
        + [Nevermind]
            ~ Talked_to_siblings_Hub_4 = true
            -> Hub_4  
    - Siblings_rejoin == 3:
        
        + [Ace (drums)]
            -> Ace
            
        + [Kurt (bass guitar)]
            -> Kurt
            
        + [MJ (lead guitar)]
            -> MJ
            
        + [Nevermind]
            ~ Talked_to_siblings_Hub_4 = true
            -> Hub_4  
    - Siblings_rejoin >= 4:
        + [Ace (drums)]
            -> Ace
            
        + [Kurt (bass guitar)]
            -> Kurt
            
        + [MJ (lead guitar)]
            -> MJ
            
        + [Haley (singer)]
            -> Haley
            
        + [Nevermind]
            ~ Talked_to_siblings_Hub_4 = true
            -> Hub_4  
    - else:
        Doesn't seem like anyone is here. Maybe I can show my dedication by replaying level 4?
            + [leave]
                -> Hub_4
}





= Haley
Haley: "I thought long and hard about doing this, but it seems like everyone else wants you back too, so who am I to disagree? Want to officially rejoin the band Harvey?"
    * "Heck yeah I do.["] Thank you so much sis!"
        Haley: "Just please promise me you won't use us again like that?"
        "You have my word."
        -> Hub_4
    * "Yes, I do.["] Thank you so much for putting your faith in me again."
        Haley: "Just don't let me, or the others, down again."
        "I will do my best to avoid that. And if I do, let's hope it's just for forgetting to turn on the stage lights."
        -> Hub_4

= Kurt
Kurt: "Ace said you g-g-guys could use some h-h-help."
    * "I appreciate it[..."], but was that the only reason?"
        Kurt: "Well, I c-c-couldn't stay mad at you f-f-forever. You're our big b-b-brother."
        "Ahh come here. Let me give you a big hug."
        -> Hub_4 
    * "We really can use it.["] Was that all it took to change you mind though?"
        Kurt: "P-p-pretty much. I missed having you a-a-around, even if you were only g-g-gone for a short while."
        "What would I ever do without you Kurt?"
        -> Hub_4

= Ace
Ace: "I saw you perform, you really suck at it. How about I help you out next time?"
    * "I thought you were mad[..."] at me? Why would you do that?"
        Ace: "Ehhh, I couldn't stay mad at you forever. Plus I grew too used to working with you. I don't want to just switch back to not having you around, it'd be too much work."
        "I'll take it."
        -> Hub_4
    * "I would really appreciate[..."] the help. What made you change your mind?"
        Ace: "After thinking about what you said, it really started to seem like you had changed and only cared abour being our stage manager. You tried to do the right thing, but screwed it up. I can't be mad at you for that."
        "Well thanks for understanding. I hope the others come around too."
        -> Hub_4

= MJ
MJ: "You're one stubborn brother and stage manager. I respect that about you. I guess what I'm trying to say is that I want to work with you again."
    * I would as well[..."] MJ, so get in here!"
        MJ: "No more ulterior motives though. Deal?"
        "Consider it done. Where do I sign?"
        -> Hub_4
    * Thanks... I guess?["] That was a strange start to that statement, but I'll take it."
        MJ: "We're still missing Haley, huh? I'm sure she'll come around. It just hit her pride pretty hard."
        "I really hope she comes back around. Until then, let's just play some music."
        -> Hub_4


=== Lvl_4_again ===

{
    - Siblings_rejoin >= 4:
        Now that everyone rejoined thanks to your tenacity, you have a much better performance.
            -> Cine_9
    - else:
        This is where you would replay level 4.
        ~ Siblings_rejoin = Siblings_rejoin + 1
        -> Hub_4

}


=== Cine_9 ===
With the band now regaining some of their trust and faith in you, they decided to help you out as you have helped them out. Rejoined with your family, you all can now move on to bigger and better things.

Cine 9:

After embarrassing himself countless times, Harvey was finally welcomed back into the band.

Haley: "You got guts to try and perform our sets without us even being there."

MJ: "Agreed, with training you may even be as good as us."

Harvey: "No thank you, I have had my fill with playing instruments. Got the calluses to prove it. I'll just stick with being your stage manager."

Ace: "Boy do we need ya."

Kurt: "Now that that's all settled, what do we do now?

Harvey: "You know, that's a really good point. We should probably figure that out." */

-> END

