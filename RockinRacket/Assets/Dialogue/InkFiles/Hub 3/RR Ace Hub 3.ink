INCLUDE ../globals.ink

-> H3

=== H3 ===
= Ace
#speaker: Ace #portrait: ace_speaking_normal #portrait: harvey_chill_normal-alt
Yoooo. What is up my man?
    * [You good?]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: ace_chill_normal
        Well hey to you too. You seem excited.
        
        -> A_H3_C1
    
    * [How you feeling?]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: ace_chill_adventurous
        How you feeling about making it this far?
        
        -> A_H3_C2
    * [End convo]
        -> END


= A_H3_C1
#speaker: Ace #portrait: ace_speaking_normal-alt2 #portrait: harvey_chill_normal-alt
How could I not dude? WE MADE IT THROUGH!

    * [We're not done yet.]
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_um
        Now wait a minute. We still have that final round to get through before anything's set in stone.
        
        #speaker: Ace #portrait: ace_speaking_challenging #portrait: harvey_chill_intrigued
        Boooo. You should be just as hyped as me right now dude. Just feel that positive energy and embrace it man.
        
            -> END
    * [I KNOW!]
        #speaker: Harvey #portrait: harvey_speaking_ambitious #portrait: ace_chill_okiedokie
        BRO, I KNOW. We are literally so close to taking home the win. I don't even want to think about the prize money.
        
        #speaker: Ace #portrait: ace_speaking_normal-alt1 #portrait: harvey_chill_normal-alt
        OH MY GOSH YOU'RE RIGHT. What am I gonna buy? Food? TV's? A new game console? AHHH, so many things I want to buy.
        
            -> END



= A_H3_C2
#speaker: Ace #portrait: ace_speaking_normal-alt1 #portrait: harvey_chill_intrigued
I'm feeling so many things right now dude, you don't even know. Joy, sadness, fear, anger. Literally everything.

    * [Sadness?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: ace_chill_disappointed
        What's to be sad about this? This is great!
        
        #speaker: Ace #portrait: ace_speaking_bummed #portrait: harvey_chill_normal
        I know, I know. But Summer's gonna end eventually man, and I don't want it to. We'll have to go back to focusing on school instead of playing. It's sad.
        
        -> END
        
    * [I feel ya]
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_um
        I get it, trust me. But this is your first time doing this, it gets better the more you experience it. 
        
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait: ace_chill_um
        So feel those emotions, all of them, and let them push you forward to greatness.
        
        #speaker: Ace #portrait: ace_speaking_doubtful #portrait: harvey_chill_normal-alt
        Since when did you get all philosophical and stuff?
        
        #speaker: Ace #portrait: ace_speaking_normal #portrait: harvey_chill_normal-alt
        But thanks. It means a lot. Now let's go win this thing.
        
        -> END








