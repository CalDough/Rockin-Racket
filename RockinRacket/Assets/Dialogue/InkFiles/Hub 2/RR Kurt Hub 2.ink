INCLUDE ../globals.ink

-> H2

=== H2 ===
= Kurt
#speaker: Kurt #portrait: kurt_speaking_happy #portrait harvey_chill_normal
Hey there H-harvey. Can I help you?

    * [How are you feeling?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait kurt_chill_happy
        Are you okay? You really looked like you were about to pass out. It got a bit better in the second half, but still. I'm worried about you kid.
        
        -> K_H2_C1
    
    * [I like your figurines]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait kurt_chill_happy
        Those figurines you got there are pretty cool. How long have you been collecting them?
        
        -> K_H2_C2


= K_H2_C1
#speaker: Kurt #portrait: kurt_speaking_normal #portrait harvey_chill_normal-alt
Oh, uh yeah. I'm fine. It was just really overwhelming for me. I just wasn't expecting that many people to show up.

    * [You're getting popular]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait kurt_chill_blank
        Yeah, you guys are getting more popular. Makes sense for more people to show up and support you guys. 
        
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait kurt_chill_normal
        Being in a band has its challenges. Let any of us know if you need a breather, okay?
        
        #speaker: Kurt #portrait: kurt_speaking_happy #portrait harvey_chill_intrigued
        You got it. I really appreciate it, Harvey.
        
        -> END
        
    * [Is there anything I can do?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait kurt_chill_confused
        Is there anything that I can do that would help? I, and I'm sure the rest of the band, don't want you up there suffering if it's a problem for you.
        
        #speaker: Kurt #portrait: kurt_speaking_sad #portrait harvey_chill_normal
        Oh, no. I'll be okay. I'm k-kinda used to it by now. Plus I have some medicine that helps. I just need to remember to take it consistently. 
        
        #speaker: Kurt #portrait: kurt_speaking_normal #portrait harvey_chill_normal-alt
        I appreciate the offer though, it means a lot.
        
        -> END

= K_H2_C2
#speaker: Kurt #portrait: kurt_speaking_happy #portrait harvey_chill_intrigued
Oh! Those? Yeah, they're absolutely great. I started c-collecting them last year once I got into "The Three Mousekateers."

#speaker: Kurt #portrait: kurt_speaking_normal-alt #portrait harvey_chill_intrigued
I wanted them to just decorate my room, but then I started getting more so now I occasionally have them fight each other and p-play with them whenever I'm bored and want something to do.

    * [A year?]
        #speaker: Harvey #portrait: harvey_speaking_normal #portrait kurt_chill_hurt
        You've been collecting them for a year now? I never realized that. I wish I knew, I would have gotten you some new ones for Christmas.
        
        #speaker: Kurt #portrait: kurt_speaking_normal-alt #portrait harvey_chill_intrigued
        Well, you never really came to my room before so it makes sense. I also just like to keep this stuff to myself and within my room. That way nobody can break them.
        
        -> END
        
    * [Do you do anything else?]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait kurt_chill_confused
        You play with them when you're bored? Do you not have any other hobbies or anything that you can do too?
        
        #speaker: Kurt #portrait: kurt_speaking_normal #portrait harvey_chill_normal-alt
        Oh, uh. I make origami sometimes too while listening to music. But my earbuds b-broke a few days ago so now I don't make origami as much.
        
        #speaker: Kurt #portrait: kurt_speaking_sad #portrait harvey_chill_normal-alt
        It feels weird to do it without music playing in my ears.
        
        -> END
