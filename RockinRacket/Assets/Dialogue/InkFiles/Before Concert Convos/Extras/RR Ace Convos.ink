INCLUDE ../globals.ink


//Naming - ex. H_H1_C1 (first letter (the H in this case) stands for the character (Haley in this case), the H1 stands for which hub it is in (Hub 1 in this case), and C1 is the dialogue choice)

-> H1

=== Ending === ///This is just a place for the conversations to route back to so there are no errors
This is a placeholder
    * [...]
        -> END



=== H1 ===
= Ace
#speaker: Ace #portrait: Ace_neutral
What up dude? 
    * [Not much]
        #speaker: Harvey #portrait: Harvey_neutral
        Not much, just wanted to see what you were doing.
        
        -> A_H1_C1
    * [About your drums...]
        #speaker: Harvey #portrait: Harvey_neutral
        About your drums. They still in playing condition?
        
        -> A_H1_C2
    * [End convo]
        -> Ending

= A_H1_C1
#speaker: Ace #portrait: Ace_neutral
Oh, I'm watching TV. Wanna join?
    * [But it's off]
        #speaker: Harvey #portrait: Harvey_neutral
        But it's off, how are you watching it?.
        
        #speaker: Ace #portrait: Ace_neutral
        I'm imagining the episodes instead. I can explain it to you if you want?
            You know what, I'll pass.
            
                -> END
    * [I might later...]
        #speaker: Harvey #portrait: Harvey_neutral
        I might later, when there's actually something on.
        
        #speaker: Ace #portrait: Ace_neutral
        Understandable. Just let me know if you do so I can make some room for you.
        
            -> END

//Fix this section
= A_H1_C2
#speaker: Ace #portrait: Ace_neutral
Heck yeah I do! Are you offering?
    * [Yeah]
        #speaker: Harvey #portrait: Harvey_neutral
        Yeah, I am. I can get one soon.
        
        #speaker: Ace #portrait: Ace_neutral
        You're the man!
        
            -> Ending
    * [Not now]
        #speaker: Harvey #portrait: Harvey_neutral
        Not now, but I may be able to get one later in time.
        
        #speaker: Ace #portrait: Ace_neutral
        Bummer. But I'm looking forward to it when you do.
        
            -> Ending






=== H2 ===
= Ace
#speaker: Ace #portrait: Ace_neutral
Yooo, Harvey. What up man?
    * [That drum fill sounds...]
        #speaker: Harvey #portrait: Harvey_neutral
        That drum fill sounds pretty good. Is that new?
        
        -> A_H2_C1
    * [Is everything alright with...]
        #speaker: Harvey #portrait: Harvey_neutral
        Is everything alright with your drums?
        
        -> A_H2_C2
    * [End convo]
        -> Ending


= A_H2_C1
#speaker: Ace #portrait: Ace_neutral
This? Heck yeah it is. I've been working on it for a few hours so far. I'm so close to getting it down.
    * [A few hours?]
        #speaker: Harvey #portrait: Harvey_neutral
        A few hours? Shouldn't you get some sleep?
        
        #speaker: Ace #portrait: Ace_neutral
        Well I've been drinking some energy drinks so I think I'll be fine.
        
            -> END
    * [What's there left to add?]
        #speaker: Harvey #portrait: Harvey_neutral
        What's there left to add to it? It already sounds really cool.
        
        #speaker: Ace #portrait: Ace_neutral
        Appreciate it man. I'm just trying to figure out where else I can add some more cymbals to.
        
            -> END

= A_H2_C2
#speaker: Ace #portrait: Ace_neutral
Not really. I've grown attached to this set. Although I do need some more sticks to use.
    * [Okay then]
        #speaker: Harvey #portrait: Harvey_neutral
        Okay then. I'll look into getting you some fancy new drumsticks.
        
        #speaker: Ace #portrait: Ace_neutral
        Heck yeah!
        
            -> Ending
    * [Oh. Okay]
        #speaker: Harvey #portrait: Harvey_neutral
        Oh. Okay. I was not expecting that response.
        
        #speaker: Ace #portrait: Ace_neutral
        I got some surprises in me every once in a while.
        
            -> Ending






=== H3 ===
= Ace
#speaker: Ace #portrait: Ace_neutral
Oho, the villian of the evening. Here to kick me while I'm down?
    * [Really? You too?]
        #speaker: Harvey #portrait: Harvey_neutral
        Really? You too? I hoped you would be the understanding one.
        
        -> A_H3_C1
    * [End convo]
        -> Ending


= A_H3_C1
#speaker: Ace #portrait: Ace_neutral
I can't just let tyranny run rampant, can I? So, what do you want?
    * [I'm trying...]
        #speaker: Harvey #portrait: Harvey_neutral
        I'm trying to make amends with you guys.
        
        #speaker: Ace #portrait: Ace_neutral
        I for one suggest you stop trying then. Take that little application of yours and run along now.
        
            -> END
    * [What can I do?]
        #speaker: Harvey #portrait: Harvey_neutral
        What can I do to make it up to you?
        
        #speaker: Ace #portrait: Ace_neutral
        How about you let us be really nice and helpful to you for a few weeks and then have you randomly learn that we were using you for ulterior motives? Then you can see what the pain feels like. It seems fair to me.
        
            -> END





=== H4 ===
= Ace
#speaker: Ace #portrait: Ace_neutral
I saw you perform, you really suck at it. How about I help you out next time?
    * [I thought you were mad?]
        #speaker: Harvey #portrait: Harvey_neutral
        I thought you were mad at me? Why would you do that?
        
        #speaker: Ace #portrait: Ace_neutral
        Ehhh, I couldn't stay mad at you forever. Plus I grew too used to working with you. I don't want to just switch back to not having you around, it'd be too much work.
        
        #speaker: Harvey #portrait: Harvey_neutral
                I'll take it.
                
                -> Ending
    * [I would really appreciate...]
        #speaker: Harvey #portrait: Harvey_neutral
        I would really appreciate the help. What made you change your mind?
        
        #speaker: Ace #portrait: Ace_neutral
        After thinking about what you said, it really started to seem like you had changed and only cared abour being our stage manager. You tried to do the right thing, but screwed it up. I can't be mad at you for that.
        
        #speaker: Harvey #portrait: Harvey_neutral
                Well thanks for understanding. I hope the others come around too.
                
                -> Ending



=== Convo_2 ===
= Ace
#speaker: Ace #portrait: Ace_neutral
Yo, what up bro?
    * [Clean up your room]
        #speaker: Harvey #portrait: Harvey_neutral
        Clean up your room please. It's filthy and stinks like crazy.
        
        -> A_Convo2_C1
    * [Can you help me?]
        #speaker: Harvey #portrait: Harvey_neutral
        Can you help me clean the kitchen?
        
        -> A_Convo2_C2
        
= A_Convo2_C1
#speaker: Ace #portrait: Ace_neutral
What? No. That would mess up the feng shui of my room. That'd be like me asking you to shave your tail.
    * [Oh don't be dramatic]
        #speaker: Harvey #portrait: Harvey_neutral
        Oh don't be dramatic. At least do something about the smell.
        
        #speaker: Ace #portrait: Ace_neutral
        Now, that, I can do. I just haven't had the energy to take any of my leftovers out to the trash.
        
            ** [Continue]
                -> Convo_2
    * [Well I've already done that]
        #speaker: Harvey #portrait: Harvey_neutral
        Well I've already done that. Remember when I was in highschool? I got gum stuck in there and didn't want to be left with a bald spot so I just shaved it all.
        
        #speaker: Ace #portrait: Ace_neutral
        Oh yeah, I forgot about that. That was pretty freaky to see. But still, I don't want to clean my room. It's my safe space just the way it is.
        
            ** [Continue]
                -> Convo_2

= A_Convo2_C2
#speaker: Ace #portrait: Ace_neutral
What's the special occasion?
    * [There isn't one]
        #speaker: Harvey #portrait: Harvey_neutral
        There isn't one. It's just extremely dirty.
        
        #speaker: Ace #portrait: Ace_neutral
        Bummer. I was really hoping there was one. Maybe like a stranger's birthday or even taco night? Something cool like that.
        
            ** [Continue]
                -> Convo_2
    * [The special occasion is dinner]
        #speaker: Harvey #portrait: Harvey_neutral
        The special occasion is dinner. We need to have space to actually make it.
        
        #speaker: Ace #portrait: Ace_neutral
        Or we could just order takeout? There's a great place down on 71st street that I know of. No? Dang...
        
            ** [Continue]
                -> Convo_2

-> Convo_2


