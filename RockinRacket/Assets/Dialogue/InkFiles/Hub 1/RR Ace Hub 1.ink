INCLUDE ../globals.ink

-> H1

=== H1 ===
= Ace
#speaker: Ace #portrait: harvey_chill_normal #portrait: ace_speaking_normal
What up dude? 
    * [Not much]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_normal
        Not much, just wanted to see what you were up to in here. I heard some laughing so I was curious.
        
        -> A_H1_C1
    * [About your drums...]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_normal
        Your drums looked pretty beat up during the concert. How are they keeping together and working?
        
        -> A_H1_C2
    * [End convo]
        -> END

= A_H1_C1
#speaker: Ace #portrait: harvey_chill_normal #portrait: ace_speaking_normal
For sure. I was just watching an episode of "The Real Horsewives of Beverly Hills." It's HILARIOUS. They just found out that Cassandra spent 10k on a purse. They are freaking out about it. Care to join the watch-party?
    * [But it's off]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_normal
        Dude, the TV's off. How in the world are you watching it?
        
        #speaker: Ace #portrait: harvey_chill_normal #portrait: ace_speaking_normal
        Well I actually watched it last night, so I'm just imagining the episode instead. I can catch you up to speed if you want?
        
           
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_normal
        Huh. I think I'll pass for now...
        
        #speaker: Ace #portrait: harvey_chill_normal #portrait: ace_speaking_normal
        Suit yourself bro. Oo, this is my favorite part!
            
            -> END
    * [I might later...]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_normal
        Nah, I'm good. I prefer "Dr. Doelittle." I love the interactions between her and her patients.
        
        
        #speaker: Ace #portrait: harvey_chill_normal #portrait: ace_speaking_normal
        Oh yeah, for sure. She's AMAZING. I'll let you know if it comes on and we could watch it together.
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_normal
        Sounds good to me. Have fun!
        
            -> END

//Fix this section
= A_H1_C2
#speaker: Ace #portrait: harvey_chill_normal #portrait: ace_speaking_normal
Honestly dude, no freaking clue. I put tape on it whenever I can and that seems to work.
    * [You need a new one]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_normal
        You BADLY need a new one then man. One that's shiny and attractive.
        
        
        #speaker: Ace #portrait: harvey_chill_normal #portrait: ace_speaking_normal
        Now you're speaking my language. Brand new drumsets are the bomb dot com.
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_normal
        That they are bro. That they are.
        
            -> END
    * [TAPE!?]
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_normal
        You're telling me that those drums are being held together by TAPE!? That's gotta be an accident just waiting to happen.
        
        
        #speaker: Ace #portrait: harvey_chill_normal #portrait: ace_speaking_normal
        Hey man, you may not have faith in them, but I do. My drums are a well-oiled machine in my mind. Sure, I may need a new drumset eventually, but for now it works great!
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_normal
        You are driving me insane dude.
        
            -> END
            
            



