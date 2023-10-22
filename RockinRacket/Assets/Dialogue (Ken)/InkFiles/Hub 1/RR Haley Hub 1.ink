INCLUDE ../globals.ink

-> H1

=== H1 ===
= Haley
#speaker: Haley #portrait: Haley_neutral
Need something Harv?
 * [Yeah, I was wondering...] 
        #speaker: Harvey #portrait: Harvey_neutral
        Yeah, I was wondering if I did alright during that performance? I feel like it was bad, but the crowd seemed to like it so I'm confused.
        
        -> H_H1_C1
            
 * [I wanted check in on...]
        #speaker: Harvey #portrait: Harvey_neutral
        I wanted check in on your equipment. Everything in working order?
        
        -> H_H1_C2
 * [End convo]
        -> END

= H_H1_C1
#speaker: Haley #portrait: Haley_neutral
Don't worry about it. It was your first time really managing everything so of course it felt a bit rough.
    * [Thank goodness]
        #speaker: Harvey #portrait: Harvey_neutral
        Thank goodness. I was freaking out over here because I though I did bad.
        
            -> END
    * [When you put it that way...]
        #speaker: Harvey #portrait: Harvey_neutral
        When you put it that way, it makes a lot of sense. I'll probably look back on this and laugh about it in the future
        
            -> END

//Fix this section
= H_H1_C2
#speaker: Haley #portrait: Haley_neutral
That would be really great! Mine has started making some funky noises anyways.
    * [I'll work on it]
        #speaker: Harvey #portrait: Harvey_neutral
        I'll work on it then because that's kinda important.
        
        #speaker: Haley #portrait: Haley_neutral
        Sweet! Thanks so much!
        
            -> END
    * [I can later]
        #speaker: Harvey #portrait: Harvey_neutral
        I can later. Let me check with the others first.
        
        #speaker: Haley #portrait: Haley_neutral
        Of course! I totally understand!
        
            -> END



-> END