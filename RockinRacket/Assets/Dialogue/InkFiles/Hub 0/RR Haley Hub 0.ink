INCLUDE ../globals.ink

-> H0

=== H0 ===
= Haley
#speaker: Haley #portrait: haley_speaking_normal-alt2 #portrait: harvey_chill_normal
See? I knew you could do it. Consider that practice as a trial run for the big show tomorrow. You think you can handle it?

    * [I think so.]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: haley_chill_huh
        If it's basically gonna be just like this, then I think I can handle it.
        
        -> H_H0_C1
    
    * [Maybe.]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: haley_chill_normal
        Who knows? If anything, it might take me a few gigs and practices to get used to it. 
        
        -> H_H0_C2


= H_H0_C1
#speaker: Haley #portrait: haley_speaking_normal-alt2 #portrait: harvey_chill_normal-alt
That's the spirit. I can't wait for tomorrow!

    * [I bet.]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: haley_chill_concerned
        Oh I bet. You guys have been talking about it like crazy for the past week.
        
        #speaker: Haley #portrait: haley_speaking_happy #portrait: harvey_chill_normal-alt
        We're just excited since this is technically our debut as a band. At least to the public.
        
        -> END
        
    * [Same here.]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: haley_chill_woah
        I'm looking forward to it too. Hopefully all goes well.
        
        #speaker: Haley #portrait: haley_speaking_nervous #portrait: harvey_chill_normal
        It should. It's not like there's gonna be a crazy amount of people anyways.
        
        #speaker: Haley #portrait: haley_speaking_happy #portrait: harvey_chill_normal-alt
        I can't wait to see the reactions of those who do show up though!
        
        -> END

= H_H0_C2
#speaker: Haley #portrait: haley_speaking_happy #portrait: harvey_chill_intrigued
Of course! You need practice just as we do. But it should all come to you pretty quickly, no doubt about it!


    * [Thanks.]
        #speaker: Harvey #portrait: harvey_speaking_normal-alt #portrait: haley_chill_woah
        Thanks for the vote of confidence Haley. It really helps soothe my nerves.
        
        #speaker: Haley #portrait: haley_speaking_normal-alt2 #portrait: harvey_chill_normal-alt
        No problem! That's what I'm here for!
        
        -> END
        
    * [Really?]
        #speaker: Harvey #portrait: harvey_speaking_intrigued #portrait: haley_chill_huh
        You really think it'll be easy for me to pick up?
        
        #speaker: Haley #portrait: haley_speaking_normal-alt1 #portrait: harvey_chill_normal
        Yes I do. We can wait and see after the show tomorrow how you feel about it though.
        
        #speaker: Haley #portrait: haley_speaking_nervous #portrait: harvey_chill_normal-alt
        But for now I think we can all use some rest.
        
        -> END
