INCLUDE ../globals.ink

-> H3

=== H3 ===
= Kurt
#speaker: Kurt #portrait: kurt_speaking_normal-alt #portrait: harvey_chill_normal-alt
Hey Harvey! Did you need something?

    * [How do you feel?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: kurt_chill_normal
        How you feeling after those performances? Must've been nerve-racking, right?
        
        -> K_H3_C1
    
    * [Ready for the finals?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: kurt_chill_happy
        You ready to take on the finals here soon?
        
        -> K_H3_C2
        
    * [End convo]
        -> END


= K_H3_C1
#speaker: Kurt #portrait: kurt_speaking_normal #portrait: harvey_chill_normal-alt
Actually, I'm doing okay. I think I've g-gotten more used to it now. It was terrifying at first, though.

    * [Oh I bet.]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: kurt_chill_normal
        It really was. A lot more people than usual. I thought I was gonna pass out at one point from the pressure too.
        
        #speaker: Kurt #portrait: kurt_speaking_normal-alt #portrait: harvey_chill_normal-alt
        Really? G-good to hear that even you thought it was crazy. I'm happy we both survived it.
        
            -> END
            
    * [I'm glad you're better.]
        #speaker: Harvey #portrait: harvey_speaking_ambitious #portrait: kurt_chill_happy
        I'm happy that you're doing better now. I told you it would, didn't I? You might even be ready to take on the world one day when we get famous enough.
        
        #speaker: Kurt #portrait: kurt_speaking_normal-alt #portrait: harvey_chill_normal-alt
        The world might be a b-bit of a stretch, but I get it. Even though I'm scared, I can't wait to experience it with you guys.
        
            -> END



= K_H3_C2
#speaker: Kurt #portrait: kurt_speaking_happy #portrait: harvey_chill_normal-alt
Totally! It's been crazy so far, but also really fun. I can't wait to finish this t-tournament off!

    * [That's the spirit!]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: kurt_chill_normal
        Now that's what I'm talking about bro! We got this.
        
        #speaker: Kurt #portrait: kurt_speaking_normal #portrait: harvey_chill_ambitious
        Agreed. We're so close I can almost taste it.
        
        -> END
    
    * [What about afterwards?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: kurt_chill_confused
        Have you put any thought as to what you wanna do afterwards? Like what's next?
        
        #speaker: Kurt #portrait: kurt_speaking_confused #portrait: harvey_chill_intrigued
        Oh, uh. I haven't really thought about that yet. Maybe we'll get big enough that we can play other locations?
        
        -> END







