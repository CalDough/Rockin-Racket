INCLUDE ../globals.ink

-> H3

=== H3 ===
= Haley
#speaker: Haley #portrait: haley_speaking_happy #portrait: harvey_chill_normal-alt
OH MY GOSH, IT'S HARVEY!!! Hello. Hi. Welcome. What can I do for you?

 * [You seem happy.]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: haley_chill_woah
        Hello... Did you get into the coffee again?
        
        -> H_H3_C1
    
    * [What are you doing?]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: haley_chill_bummed
        What is going on in here? It's more of a mess than usual.
        
        -> H_H3_C2
            
 * [End convo]
        -> END


= H_H3_C1
#speaker: Haley #portrait: haley_speaking_happy #portrait: harvey_chill_intrigued
Nope, nope, nope. I'm just too EXCITED. Ooo, also, I wanted to say thank you.

    * [You're welcome?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: haley_chill_normal
        You're welcome, I guess? Did I do something to deserve thanks?
        
        #speaker: Haley #portrait: haley_speaking_intrigued #portrait: harvey_chill_normal-alt
        What? I can't thank you for no reason? Well see now I don't want to tell you. How do you like them apples?
        
            -> END
    * [What for?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: haley_chill_huh
        Wait, what exactly did I do?
        
        #speaker: Haley #portrait: haley_speaking_normal-alt2 #portrait: harvey_chill_normal-alt
        For helping us ya big goof! I really don't think we would've made it this far without you.
        
        #speaker: Haley #portrait: haley_speaking_normal-alt2 #portrait: harvey_chill_normal-alt
        It means a lot to me that you've been helping us.
        
        -> END


= H_H3_C2
#speaker: Haley #portrait: haley_speaking_nervous #portrait: harvey_chill_normal
Oh, this? Don't worry about it. I'm just cleaning up a bit. Or at least trying to.

    * [Cleaning?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: haley_chill_normal-alt
        Who are you and what have you done with Haley? She'd never be cleaning her room of her own choice.
        
        #speaker: Haley #portrait: haley_speaking_woah #portrait: harvey_chill_normal-alt
        Hey! RUDE. I'll have you know that I'm just doing this to help with all this energy I have. It felt like I was gonna explode if I didn't use it.
        
        -> END
        
    * [Good idea.]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: haley_chill_bummed
        Your room really needed it so I'm glad you decided to clean it. Nice job.
        
        #speaker: Haley #portrait: haley_speaking_bummed #portrait: harvey_chill_normal-alt
        Ouch. Even if it's been a bit messy, I know where everything is. Sometimes.
        
        #speaker: Haley #portrait: haley_speaking_normal-alt2 #portrait: harvey_chill_intrigued
        It's called an organized mess, okay? Just trust the system.
        
        -> END










