INCLUDE ../globals.ink

-> H2

=== H2 ===
= Ace
#speaker: Ace #portrait: ace_speaking_normal #portrait: harvey_chill_normal-alt
Harv-man! What can I do for you?

    * [Your drum fills sound great.]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: ace_chill_normal
        Those drum fills you did earlier were amazing. Is it something new or have you been working on it for a while?
        
        -> A_H2_C1
    
    * [How you doing with the band?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: ace_chill_um
        How's it going with the band? Doesn't seem like any of you have clawed each others' eyes out yet, so everything must be going smoothly?
        
        -> A_H2_C2

= A_H2_C1
#speaker: Ace #portrait: ace_speaking_normal-alt2 #portrait: harvey_chill_normal-alt
Oh yeah, relatively new. I tried it out a few nights ago during practice and everyone else loved it! 

#speaker: Ace #portrait: ace_speaking_normal #portrait: harvey_chill_intrigued
Keep your ears open next time too and you might be even more surprised by what I'm cooking up now.
    
    * [Already making a new one?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: ace_chill_normal
        You're already planning out a new drum fill to use? How long have you been working on it?
        
        #speaker: Ace #portrait: ace_speaking_tired #portrait: harvey_chill_normal
        Ehh, past couple of hours. Not including my snack breaks though, so maybe a bit longer. It's gonna be a long night for me.
        
        -> END
    
    * [How many are you working on?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: ace_chill_okiedokie
        So wait a minute. You had a new one, and now you're already working on a new one? How many of those do you have down?
        
        #speaker: Ace #portrait: ace_speaking_normal-alt1 #portrait: harvey_chill_normal-alt
        You're not ready for that kind of knowledge bro. It's like I'm playing chess with my drums right now if you get what I mean. So many things locked in this brain of mine right now.
        
        -> END

= A_H2_C2
#speaker: Ace #portrait: ace_speaking_normal #portrait: harvey_chill_normal-alt
We're pretty tight right now. No rivalries here, or at least not that I know of. 

#speaker: Ace #portrait: ace_speaking_normal-alt1 #portrait: harvey_chill_normal
Now that you mention it though, the other day MJ used Haley's hairbrush without asking and I thought we were gonna need a new lead guitarist.
    
    * [Oh boy.]
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_exasperated
        I really thought MJ would have learned her lesson by now. This crap happens like every month.
        
        #speaker: Ace #portrait: ace_speaking_normal-alt2 #portrait: harvey_chill_normal
        Hey man, that's her problem. That's why I always hide mine so nobody else can use it. You don't mess with another raccoons' hairbrush unless you want the claws.
        
        -> END

    * [I'm used to that.]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: ace_chill_normal
        Know what? That doesn't surprise me. Glad you're doing okay with them though. Wouldn't want anything like that to get in the way of the band.
        
        #speaker: Ace #portrait: ace_speaking_normal-alt1 #portrait: harvey_chill_normal
        Boy do I know it. Without cooperation, we wouldn't have a band. And without a band, we would have less money. Without money, we'd lose out on food. 
        
        #speaker: Ace #portrait: ace_speaking_bummed #portrait: harvey_chill_normal-alt
        And once the food goes, life's basically over man.
        
        -> END

